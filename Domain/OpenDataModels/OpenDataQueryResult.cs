using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OpenDataModels
{
    public class OpenDataQueryResult
    {
        public int Count { get; set; }
        public List<Data> Data { get; set; }
    }
}
