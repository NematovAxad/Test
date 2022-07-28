using AdminHandler.Results.SecondOptionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.SecondOptionQueries
{
    public class OrgSocialQuery:IRequest<OrgSocialQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
