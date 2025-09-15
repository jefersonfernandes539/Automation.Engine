using Automation.Engine.Application.UseCases;
using Quartz;

namespace Automation.Engine.Worker.Jobs
{
    public class QuoteJob : IJob
    {
        private readonly ProcessQuoteUseCase _useCase;

        public QuoteJob(ProcessQuoteUseCase useCase) => _useCase = useCase;

        public Task Execute(IJobExecutionContext context)
        {
            _useCase.Execute();
            return Task.CompletedTask;
        }
    }
}
