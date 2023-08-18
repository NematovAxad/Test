using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SeventhSection;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Domain.States;
using Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;
using Domain.Models.FirstSection;
using Domain.Models.EighthSection;
using UserHandler.Results.EigthSectionResult;
using UserHandler.Commands.EigthSectionCommand;

namespace UserHandler.Handlers.EigthSectionHandlers
{
    public class OrgFinanceReportCommandHandler : IRequestHandler<OrgFinanceReportCommand, OrgFinanceReportCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationFinanceReport, int> _orgFinanceReport;

        public OrgFinanceReportCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationFinanceReport, int> orgFinanceReport)
        {
            _organization = organization;
            _deadline = deadline;
            _orgFinanceReport = orgFinanceReport;
        }

        public async Task<OrgFinanceReportCommandResult> Handle(OrgFinanceReportCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add:
                    id = Add(request);
                    break;
                case Domain.Enums.EventType.Update:
                    id = Update(request);
                    break;
                case Domain.Enums.EventType.Delete:
                    id = Delete(request);
                    break;
            }
            return new OrgFinanceReportCommandResult() { Id = id, IsSuccess = true };
        }

        public int Add(OrgFinanceReportCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            var orgFinanceReport = _orgFinanceReport.Find(p => p.OrganizationId == model.OrganizationId && p.Year == deadline.Year).Include(mbox => mbox.Organization).FirstOrDefault();
            if (orgFinanceReport != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            OrganizationFinanceReport addModel = new OrganizationFinanceReport();


            if (model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE) || model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                addModel.OrganizationId = model.OrganizationId;
                addModel.Year = deadline.Year;

                addModel.FullYearBudget = model.FullYearBudget;
                addModel.FullYearDigitalizationBudgetPercent = model.FullYearDigitalizationBudgetPercent;
                addModel.FullYearSpentBudgetPercent = model.FullYearSpentBudgetPercent;
                addModel.UserPinfl = model.UserPinfl;
                addModel.LastUpdate = DateTime.Now;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _orgFinanceReport.Add(addModel);

            return addModel.Id;
        }
        public int Update(OrgFinanceReportCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var orgFinanceReport = _orgFinanceReport.Find(p => p.Id == model.Id).Include(mbox => mbox.Organization).FirstOrDefault();
            if (orgFinanceReport == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());




            if (model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE) || model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {

                orgFinanceReport.FullYearBudget = model.FullYearBudget;
                orgFinanceReport.FullYearDigitalizationBudgetPercent = model.FullYearDigitalizationBudgetPercent;
                orgFinanceReport.FullYearSpentBudgetPercent = model.FullYearSpentBudgetPercent;
                orgFinanceReport.UserPinfl = model.UserPinfl;
                orgFinanceReport.LastUpdate = DateTime.Now;

            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _orgFinanceReport.Update(orgFinanceReport);

            return orgFinanceReport.Id;
        }

        public int Delete(OrgFinanceReportCommand model)
        {
            var orgFinanceReport = _orgFinanceReport.Find(p => p.Id == model.Id).FirstOrDefault();
            if (orgFinanceReport == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _orgFinanceReport.Remove(orgFinanceReport);
            return orgFinanceReport.Id;
        }
    }
}
