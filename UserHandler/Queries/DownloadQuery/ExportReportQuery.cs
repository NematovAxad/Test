using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.DownloadResult;

namespace UserHandler.Queries.DownloadQuery
{
    public class ExportReportQuery:IRequest<ExportReportResult>
    {
        public int OrganizationId { get; set; }
        public OrgCategory Category { get; set; }
        public int DeadlineId { get; set; }
    }
}
