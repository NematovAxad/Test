using Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.DownloadResult
{
    public class ExportReportResult
    {
        public List<ExportReportResultModelG> ItemGov { get; set; }
        public List<ExportReportResultModelX> ItemXoz { get; set; }
    }
}
