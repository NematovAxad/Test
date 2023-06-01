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
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Results.SixthSectionResults;
using Domain.States;
using Domain;
using MainInfrastructures.Migrations;
using System.Linq;
using Domain.Permission;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrganizationDigitalEconomyProjectsReportCommandHandler : IRequestHandler<OrganizationDigitalEconomyProjectsReportCommand, OrganizationDigitalEconomyProjectsReportCommandResult>
    {

        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationDigitalEconomyProjectsReport, int> _orgDigitalEconomyProjectsReport;

        public OrganizationDigitalEconomyProjectsReportCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationDigitalEconomyProjectsReport, int> orgDigitalEconomyProjectsReport)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDigitalEconomyProjectsReport = orgDigitalEconomyProjectsReport;
        }

        public async Task<OrganizationDigitalEconomyProjectsReportCommandResult> Handle(OrganizationDigitalEconomyProjectsReportCommand request, CancellationToken cancellationToken)
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
            return new OrganizationDigitalEconomyProjectsReportCommandResult() { Id = id, IsSuccess = true };
        }

        public int Add(OrganizationDigitalEconomyProjectsReportCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);



            var economyProjectReport = _orgDigitalEconomyProjectsReport.Find(p => p.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (economyProjectReport != null)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);

            OrganizationDigitalEconomyProjectsReport addModel = new OrganizationDigitalEconomyProjectsReport();

            addModel.OrganizationId = model.OrganizationId;
            addModel.ProjectsCount = model.ProjectsCount;
            addModel.CompletedProjects = model.CompletedProjects;
            addModel.OngoingProjects = model.OngoingProjects;
            addModel.NotFinishedProjects = model.NotFinishedProjects;

            _orgDigitalEconomyProjectsReport.Add(addModel);

            return addModel.Id;
        }

        public int Update(OrganizationDigitalEconomyProjectsReportCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            var economyProjectReport = _orgDigitalEconomyProjectsReport.Find(p => p.Id == model.Id).FirstOrDefault();
            if (economyProjectReport == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            economyProjectReport.ProjectsCount = model.ProjectsCount;
            economyProjectReport.CompletedProjects = model.CompletedProjects;
            economyProjectReport.OngoingProjects = model.OngoingProjects;
            economyProjectReport.NotFinishedProjects = model.NotFinishedProjects; 

            _orgDigitalEconomyProjectsReport.Update(economyProjectReport);

            return economyProjectReport.Id;
        }

        public int Delete(OrganizationDigitalEconomyProjectsReportCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.SixthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            var economyProject = _orgDigitalEconomyProjectsReport.Find(p => p.Id == model.Id).FirstOrDefault();
            if (economyProject == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            _orgDigitalEconomyProjectsReport.Remove(economyProject);

            return economyProject.Id;
        }
    }
}
