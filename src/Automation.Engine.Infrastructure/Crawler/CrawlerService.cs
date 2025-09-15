using Automation.Engine.Domain.Entities;
using Automation.Engine.Domain.Interfaces;
using HtmlAgilityPack;

namespace Automation.Engine.Infrastructure.Crawler
{
    public class CrawlerService : ICrawlerService
    {
        public Quote GetQuote()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://quotes.toscrape.com/");

            var quote = doc.DocumentNode.SelectNodes("//span[@class='text']")
                ?.FirstOrDefault()?.InnerText ?? "Sem dados";

            return new Quote { Text = quote };
        }
    }
}
