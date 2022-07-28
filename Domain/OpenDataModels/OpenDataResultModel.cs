using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OpenDataModels
{
    public class OpenDataResultModel
    {
        public ResultClass Result { get; set; }
    }
    public class ResultClass
    {
        public int Count { get; set; }
        public List<Data> Data { get; set; }
    }
    public class Data
    {
        public string DataName { get; set; }
        public string OrgName { get; set; }
        public string Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public string Link { get; set; }
    }
}
