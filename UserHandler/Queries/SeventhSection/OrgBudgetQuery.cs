using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Queries.SeventhSection
{
    public class OrgBudgetQuery:IRequest<OrgBudgetQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
