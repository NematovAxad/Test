using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ReestrProjectIdentityResult;

namespace UserHandler.Queries.ReestrProjectIdentityQuery
{
    public class ReestrProjectIdentityQuery:IRequest<ReestrProjectIdentityQueryResult>
    {
        public int OrgId { get; set; }
        public int ReestrProjectId { get; set; }
    }
}
