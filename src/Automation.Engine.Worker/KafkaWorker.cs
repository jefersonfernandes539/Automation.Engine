using Automation.Engine.Application.DataTransferObjects;
using Automation.Engine.Domain.Entities;
using Automation.Engine.Domain.Interfaces;
using Confluent.Kafka;
using System.Text.Json;

namespace Automation.Engine.Worker
{
    public class KafkaWorker : BackgroundService
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _consumeTopic = "robot-jobs";
        private readonly string _produceTopic = "robot-results";
        private readonly string _groupId = "automation-worker-group";

        private readonly IServiceProvider _serviceProvider;

        public KafkaWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
            consumer.Subscribe(_consumeTopic);

            using var producer = new ProducerBuilder<Null, string>(
                new ProducerConfig { BootstrapServers = _bootstrapServers }
            ).Build();

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = consumer.Consume(stoppingToken);
                        Console.WriteLine($"Job recebido: {cr.Message.Value}");

                        var job = JsonSerializer.Deserialize<JobRequestDto>(cr.Message.Value);

                        using var scope = _serviceProvider.CreateScope();
                        var crawlerService = scope.ServiceProvider.GetRequiredService<ICrawlerService>();
                        var quoteRepository = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();

                        var quoteFromCrawler = crawlerService.GetQuote();

                        var quoteToSave = new Quote
                        {
                            Id = Guid.NewGuid(),
                            Text = quoteFromCrawler.Text,
                            CreatedAt = DateTime.UtcNow
                        };

                        await quoteRepository.SaveAsync(quoteToSave);
                        var jobResult = new
                        {
                            originalMessage = cr.Message.Value,
                            status = "completed",
                            timestamp = DateTime.UtcNow
                        };

                        var resultJson = JsonSerializer.Serialize(jobResult);

                        await producer.ProduceAsync(
                            _produceTopic,
                            new Message<Null, string> { Value = resultJson },
                            stoppingToken
                        );

                        Console.WriteLine($"Resultado enviado: {resultJson}");

                        consumer.Commit(cr);
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Erro no consumo: {ex.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Worker cancelado.");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
