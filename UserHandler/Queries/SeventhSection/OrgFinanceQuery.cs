using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Queries.SeventhSection
{
    public class OrgFinanceQuery:IRequest<OrgFinanceQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
