using SFL_JPN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFL_JPN.Services
{
    public class CsvService
    {
        public void WriteResultsToCsv(List<Result> results)
        {
            var resultsInCsvFormat = results.Select(result => $"{result.ResultId},{result.Player},{result.Character},{result.MatchesWon},{result.Outcome}");
            var csvString = String.Join("\n", resultsInCsvFormat);
            File.WriteAllText("sfv_jpn_matches.csv", csvString);
        }
    }
}
