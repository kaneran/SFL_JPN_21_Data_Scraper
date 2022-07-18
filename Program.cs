// See https://aka.ms/new-console-template for more information
using Microsoft.Playwright;
using System.Text.RegularExpressions;

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

//Get the match results throughout October to December excluding Play offs and Grand finals results
var matchResultsData = data.Where(data => data.Contains("December") || data.Contains("November") || data.Contains("October")).ToList();

matchResultsData.ForEach((matchResult) =>
{
    var matchResults = matchResult.Split("\n").Where(data => data.Contains("Away:") || data.Contains("Home:"));
});
