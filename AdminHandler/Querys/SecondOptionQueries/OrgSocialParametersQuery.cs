using AdminHandler.Results.SecondOptionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.SecondOptionQueries
{
    public class OrgSocialParametersQuery : IRequest<OrgSocialParametersQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
