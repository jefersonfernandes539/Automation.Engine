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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
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
                        Console.WriteLine($"📥 Job recebido: {cr.Message.Value}");

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

                        Console.WriteLine($"📤 Resultado enviado: {resultJson}");

                        consumer.Commit(cr);
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"⚠️ Erro no consumo: {ex.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("🛑 Worker cancelado.");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
