using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Queries.ThirdSection
{
    public class OrgEventsQuery:IRequest<OrgEventsQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
