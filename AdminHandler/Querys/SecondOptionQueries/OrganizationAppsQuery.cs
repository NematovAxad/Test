using AdminHandler.Results.SecondOptionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.SecondOptionQueries
{
    public class OrganizationAppsQuery:IRequest<OrganizationAppsQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
