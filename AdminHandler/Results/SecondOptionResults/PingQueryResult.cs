using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Results.SecondOptionResults
{
    public class PingQueryResult
    {
        public int Count { get; set; }
        public List<object> Data { get; set; }
        public List<object> Fails { get; set; }
    }
}
