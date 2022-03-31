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
        public bool Status { get; set; }
        public bool GeoType { get; set; }
        public bool IsWebUrl { get; set; }
        public string Weburl { get; set; }
        public string Link { get; set; }
    }
}
