using SFL_JPN.Services;


namespace SFL_JPN.Controllers
{
    public class ResultsController
    {
        private WebScraperService _webScraperService;
        public ResultsController()
        {
            _webScraperService = new WebScraperService();

        }

        public async void GetResults()
        {
            var data = await _webScraperService.GetResultsFromWebPage();

            //Get the match results throughout October to December excluding Play offs and Grand finals results
            var results = _webScraperService.GetMatchResults(data);
            var resultsInCsvFormat = results.Select(result => $"{result.ResultId},{result.Player},{result.Character},{result.MatchesWon},{result.Outcome}");
            var csvString = String.Join("\n", resultsInCsvFormat); 
            File.WriteAllText(@"C:\Users\kane-\Documents\Personal Projects\SFL_JPN\SFL_JPN\sfv_jpn_matches.csv", csvString);
        }
    }
}
