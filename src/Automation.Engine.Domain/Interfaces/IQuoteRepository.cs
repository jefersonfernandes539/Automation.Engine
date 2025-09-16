using Automation.Engine.Domain.Entities;

namespace Automation.Engine.Domain.Interfaces
{
    public interface IQuoteRepository
    {
        Task<Guid> SaveAsync(Quote quote);
        Quote? GetLast();
    }
}
