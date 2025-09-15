using Automation.Engine.Domain.Entities;
using Automation.Engine.Domain.Interfaces;

namespace Automation.Engine.Infrastructure.Persistence
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly AutomationContext _context;

        public QuoteRepository(AutomationContext context)
        {
            _context = context;
        }

        public Guid Save(Quote quote)
        {
            _context.Quotes.Add(quote);
            _context.SaveChanges();
            return quote.Id;
        }

        public Quote? GetLast()
        {
            return _context.Quotes.OrderByDescending(q => q.Id).FirstOrDefault();
        }
    }
}
