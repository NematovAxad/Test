using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Queries.ThirdSection
{
    public class OrgInformationSystemsQuery: IRequest<OrgInformationSystemsQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
