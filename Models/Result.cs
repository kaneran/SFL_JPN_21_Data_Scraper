﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFL_JPN.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public string Player { get; set; }
        public string Character { get; set; }
        public int MatchesWon { get; set; }
        public string Outcome { get; set; }
    }
}
