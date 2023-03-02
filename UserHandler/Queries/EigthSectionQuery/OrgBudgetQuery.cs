using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.EigthSectionResult;

namespace UserHandler.Queries.EigthSectionQuery
{
    public class OrgBudgetQuery : IRequest<OrgBudgetQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
