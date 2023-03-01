using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ReestrProjectEfficiencyResult;

namespace UserHandler.Queries.ReestrProjectEfficiencyQuery
{
    public class ReestrProjectEfficiencyQuery :IRequest<ReestrProjectEfficiencyQueryResult>
    {
        public int OrgId { get; set; }
        public int ReestrProjectId { get; set; }
    }
}
