using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ReestrProjectAutomatedServicesResult;

namespace UserHandler.Queries.ReestrProjectAutomatedServicesQuery
{
    public class ReestrProjectServicesQuery:IRequest<ReestrProjectServicesQueryResult>
    {
        public int OrgId { get; set; }
        public int ReestrProjectId { get; set; }
    }
}
