using AdminHandler.Results.SecondOptionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.SecondOptionQueries
{
    public class OrgSocialSitesQuery:IRequest<OrgSocialSitesQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int DeadlineId { get; set; }
    }
}
