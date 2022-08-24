using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.DownloadResult;

namespace UserHandler.Queries.DownloadQuery
{
    public class SiteReportQuery : IRequest<SiteReportResult>
    {
        public int OrgId { get; set; }
    }
}
