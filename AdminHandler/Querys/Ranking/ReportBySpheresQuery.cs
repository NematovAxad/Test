using AdminHandler.Results.Ranking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Ranking
{
    public class ReportBySpheresQuery:IRequest<ReportBySpheresResult>
    {
        public int DeadlineId { get; set; }
        public int OrganizationId { get; set; }
    }
}
