using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Queries.SixthSectionQueries
{
    public class OrgDataAvailabilityQuery:IRequest<OrgDataAvailabilityQueryResult>
    {
        public int OrgId { get; set; }
        public string Section { get; set; }
    }
}
