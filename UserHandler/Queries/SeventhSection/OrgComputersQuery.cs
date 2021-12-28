using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Queries.SeventhSection
{
    public class OrgComputersQuery:IRequest<OrgComputersQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
