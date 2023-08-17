using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace AdminHandler.Results.SecondOptionResults
{
    public class PingQueryResult
    {
        public int Count { get; set; }
        public Deadline Deadline { get; set; }
        public List<object> Data { get; set; }
        public List<object> Fails { get; set; }
    }
}
