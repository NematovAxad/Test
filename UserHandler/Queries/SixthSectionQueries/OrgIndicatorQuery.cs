using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Queries.SixthSectionQueries
{
    public class OrgIndicatorQuery:IRequest<OrgIndicatorQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
