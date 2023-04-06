using Domain.Models.SixthSection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Queries.SixthSectionQueries
{
    public class OrganizationDigitalEconomyProjectsReportQuery:IRequest<OrganizationDigitalEconomyProjectsReportQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
