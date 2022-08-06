using Microsoft.Playwright;
using SFL_JPN.Models;

namespace SFL_JPN.Services
{
    public class WebScraperService
    {
        public async Task<IReadOnlyList<string>> GetResultsFromWebPage()
        {
            using var playright = await Playwright.CreateAsync();
            await using var browser = await playright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.eventhubs.com/news/2021/oct/04/sfl-pro-jp-2021-results/");
            await page.Locator("button:has-text(\"AGREE\") >> nth=-1").ClickAsync();
            var matchResults = page.Locator(".results1");
            var data = await matchResults.AllTextContentsAsync();
            return data;
        }

        public List<Result> GetMatchResults(IReadOnlyList<string> data)
        {
            var filteredData = data.Where(data => data.Contains("December") || data.Contains("November") || data.Contains("October")).ToList();
            var results = new List<Result>();
            var resultId = 0;
            filteredData.ForEach((matchResult) =>
            {
                var matchResults = matchResult.Split("\n").Where(data => data.Contains("Away:") || data.Contains("Home:")).ToList();

                matchResults.ForEach(result =>
                {
                    var matchesWonByPlayers = result.Substring(result.Length - 4).Replace(".", "").Split("-");

                    //Player name, Character used, matches won, match result (Won, Lost) , result id
                    var playerDataStrings = result.Replace("•", "").Split(new string[] { "beat" }, StringSplitOptions.None);
                    for (var i = 0; i < playerDataStrings.Length; i++)
                    {
                        var playerData = playerDataStrings[i].Split(" ").Where(data => !string.IsNullOrEmpty(data)).ToList();
                        var isSecondPlayer = i == 1;
                        var characterString = String.Join(" ", playerData.GetRange(2, playerData.Count - (isSecondPlayer ? 3 : 2)).ToArray());
                        var character = playerData.Count > 2 ? characterString.Replace("(", "").Replace(")", "") : "N/A";
                        var outcome = isSecondPlayer ? "Lost" : "Won";
                        var matchResult = new Result() { ResultId = resultId, Player = playerData[1], Character = character, MatchesWon = Int32.Parse(matchesWonByPlayers[i]), Outcome = outcome };
                        results.Add(matchResult);

                    }
                    resultId++;
                });
            });
            return results;
        }
    }
}
