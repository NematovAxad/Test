using Domain.Models.FirstSection;
using Domain.Models;
using Domain.Models.SixthSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;
using MainInfrastructures.Migrations;
using System.Linq;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrganizationDigitalEconomyProjectsReportQueryHandler : IRequestHandler<OrganizationDigitalEconomyProjectsReportQuery, OrganizationDigitalEconomyProjectsReportQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationDigitalEconomyProjectsReport, int> _orgDigitalEconomyProjectsReport;

        public OrganizationDigitalEconomyProjectsReportQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationDigitalEconomyProjectsReport, int> orgDigitalEconomyProjectsReport)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDigitalEconomyProjectsReport = orgDigitalEconomyProjectsReport;
        }

        public async Task<OrganizationDigitalEconomyProjectsReportQueryResult> Handle(OrganizationDigitalEconomyProjectsReportQuery request, CancellationToken cancellationToken)
        {
            var digitalEconomyProjectsReport = _orgDigitalEconomyProjectsReport.Find(p => p.OrganizationId == request.OrganizationId).FirstOrDefault();

            OrganizationDigitalEconomyProjectsReportQueryResult result = new OrganizationDigitalEconomyProjectsReportQueryResult();

            result.OrganizationDigitalEconomyProjectsReport = digitalEconomyProjectsReport;

            return result;
        }
    }
}
