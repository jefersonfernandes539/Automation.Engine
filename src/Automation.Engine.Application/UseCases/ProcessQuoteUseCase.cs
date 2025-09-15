using Automation.Engine.Domain.Interfaces;

namespace Automation.Engine.Application.UseCases
{
    public class ProcessQuoteUseCase
    {
        private readonly ICrawlerService _crawler;
        private readonly IQuoteRepository _repo;
        private readonly IRpaService _rpa;

        public ProcessQuoteUseCase(ICrawlerService crawler, IQuoteRepository repo, IRpaService rpa)
        {
            _crawler = crawler;
            _repo = repo;
            _rpa = rpa;
        }

        public void Execute()
        {
            var quote = _crawler.GetQuote();
            Console.WriteLine($"[Crawler] {quote.Text}");

            int id = _repo.Save(quote);
            Console.WriteLine($"[DB] ID {id} salvo");

            var last = _repo.GetLast();
            if (last != null)
            {
                _rpa.FillForm(last);
                Console.WriteLine("[RPA] Formulário preenchido!");
            }
        }
    }
}
