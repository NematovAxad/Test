using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ReestrProjectAuthorizationResult;

namespace UserHandler.Queries.ReestrProjectAuthorizationQuery
{
    public class ReestrProjectAuthorizationQuery:IRequest<ReestrProjectAuthorizationQueryResult>
    {
        public int OrgId { get; set; }
        public int ReestrProjectId { get; set; }
    }
}
