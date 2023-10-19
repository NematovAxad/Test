using System;
using System.Collections.Generic;

namespace Domain.ReesterModels
{
    public class FirstRequestQueryResultNew
    {
        public long Count { get; set; }
        public long TotalPages { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<FirstRequestResultModelNew> Items { get; set; }
    }
}