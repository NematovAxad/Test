using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Queries.SixthSectionQueries
{
    public class OrgIndicatorRateQuery:IRequest<OrgIndicatorRateQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
