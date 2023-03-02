using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.EigthSectionResult;

namespace UserHandler.Queries.EigthSectionQuery
{
    public class OrgFinanceQuery : IRequest<OrgFinanceQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
