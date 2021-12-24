using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Queries.ThirdSection
{
    public class OrgPublicServicesQuery:IRequest<OrgPublicServicesQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
