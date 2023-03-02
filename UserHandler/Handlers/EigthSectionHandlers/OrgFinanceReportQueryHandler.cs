using Domain.Models;
using Domain.States;
using Domain;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Domain.Models.FirstSection;
using Domain.Models.EighthSection;
using UserHandler.Queries.EigthSectionQuery;
using UserHandler.Results.EigthSectionResult;

namespace UserHandler.Handlers.EigthSectionHandlers
{
    public class OrgFinanceReportQueryHandler : IRequestHandler<OrgFinanceReportQuery, OrgFinanceReportQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationFinanceReport, int> _orgFinanceReport;

        public OrgFinanceReportQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationFinanceReport, int> orgFinanceReport)
        {
            _organization = organization;
            _deadline = deadline;
            _orgFinanceReport = orgFinanceReport;
        }
        public async Task<OrgFinanceReportQueryResult> Handle(OrgFinanceReportQuery request, CancellationToken cancellationToken)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var orgFinanceReport = _orgFinanceReport.Find(p => p.OrganizationId == request.OrganizationId && p.Year == deadline.Year).FirstOrDefault();

            OrgFinanceReportQueryResult result = new OrgFinanceReportQueryResult();
            result.OrganizationFinanceReport = orgFinanceReport;
            return result;
        }
    }
}
