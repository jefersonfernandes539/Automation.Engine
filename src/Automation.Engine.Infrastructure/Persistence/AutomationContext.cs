using Automation.Engine.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Automation.Engine.Infrastructure.Persistence
{
    public class AutomationContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }

        public AutomationContext(DbContextOptions<AutomationContext> options) : base(options) { }
    }

}
