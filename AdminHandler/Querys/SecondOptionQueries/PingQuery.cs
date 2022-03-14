using AdminHandler.Results.SecondOptionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.SecondOptionQueries
{
    public class PingQuery:IRequest<PingQueryResult>
    {
        public int OrganizationId { get; set; }
        public int DeadlineId { get; set; }
    }
}
