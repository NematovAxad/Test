using AdminHandler.Results.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Organization
{
    public class SubOrgStatisticsQuery:IRequest<SubOrgStatisticsQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
