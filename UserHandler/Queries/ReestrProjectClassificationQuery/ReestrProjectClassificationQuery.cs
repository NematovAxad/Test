using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ReestrProjectClassificationResult;

namespace UserHandler.Queries.ReestrProjectClassificationQuery
{
    public class ReestrProjectClassificationQuery:IRequest<ReestrProjectClassificationQueryResult>
    {
        public int OrgId { get; set; }
        public int ReestrProjectId { get; set; }
    }
}
