using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ReesterModels
{
    public class FirstRequestQueryResult
    {
        public long Count { get; set; }
        public long TotalPages { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<FirstRequestResultModel> Items { get; set; }
    }
}
