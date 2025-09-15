using Automation.Engine.Domain.Entities;

namespace Automation.Engine.Domain.Interfaces
{
    public interface IQuoteRepository
    {
        Guid Save(Quote quote);
        Quote? GetLast();
    }
}
