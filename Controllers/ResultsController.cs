using SFL_JPN.Services;


namespace SFL_JPN.Controllers
{
    public class ResultsController
    {
        private WebScraperService _webScraperService;
        private CsvService _csvService;
        public ResultsController()
        {
            _webScraperService = new WebScraperService();
            _csvService = new CsvService();

        }

        public async void WriteResults()
        {
            var data = await _webScraperService.GetResultsFromWebPage();
            var results = _webScraperService.GetMatchResults(data);
            _csvService.WriteResultsToCsv(results);
        }
    }
}
