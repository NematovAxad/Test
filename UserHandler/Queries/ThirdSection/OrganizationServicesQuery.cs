using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Queries.ThirdSection
{
    public class OrganizationServicesQuery:IRequest<OrganizationServicesQueryResult>
    {
        public int OrganizationId { get; set; }

        public int ServiceId { get; set; }
    }
}
