using Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Results.Ranking
{
    public class ReportBySpheresResult
    {
        public int Count { get; set; }
        public List<ReportBySpheresModel> Data { get; set; }
    }
}
