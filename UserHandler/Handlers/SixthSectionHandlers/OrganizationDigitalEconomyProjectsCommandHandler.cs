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
    public class OrganizationDigitalEconomyProjectsCommandHandler : IRequestHandler<OrganizationDigitalEconomyProjectsCommand, OrganizationDigitalEconomyProjectsCommandResult>
    {

        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationDigitalEconomyProjects, int> _orgDigitalEconomyProjects;

        public OrganizationDigitalEconomyProjectsCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationDigitalEconomyProjects, int> orgDigitalEconomyProjects)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDigitalEconomyProjects = orgDigitalEconomyProjects;
        }

        public async Task<OrganizationDigitalEconomyProjectsCommandResult> Handle(OrganizationDigitalEconomyProjectsCommand request, CancellationToken cancellationToken)
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
            return new OrganizationDigitalEconomyProjectsCommandResult() { Id = id, IsSuccess = true };
        }

        public int Add(OrganizationDigitalEconomyProjectsCommand model)
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



            var economyProject = _orgDigitalEconomyProjects.Find(p => p.OrganizationId == model.OrganizationId && p.ProjectName == model.ProjectName).FirstOrDefault();
            if (economyProject != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            OrganizationDigitalEconomyProjects addModel = new OrganizationDigitalEconomyProjects();
            
            addModel.OrganizationId = model.OrganizationId;
            addModel.ProjectName = model.ProjectName;
            addModel.BasisFilePath = model.BasisFilePath;
            addModel.Comment = model.Comment;
            addModel.Date = DateTime.Now;
            addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;


            _orgDigitalEconomyProjects.Add(addModel);

            return addModel.Id;
        }

        public int Update(OrganizationDigitalEconomyProjectsCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            var economyProject = _orgDigitalEconomyProjects.Find(p => p.Id == model.Id).FirstOrDefault();
            if (economyProject == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            economyProject.ProjectName = model.ProjectName;
            economyProject.BasisFilePath = model.BasisFilePath;
            economyProject.Comment = model.Comment;
            economyProject.UserPinfl = model.UserPinfl;
            economyProject.LastUpdate = DateTime.Now;

            _orgDigitalEconomyProjects.Update(economyProject);

            return economyProject.Id;
        }

        public int Delete(OrganizationDigitalEconomyProjectsCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            var economyProject = _orgDigitalEconomyProjects.Find(p => p.Id == model.Id).FirstOrDefault();
            if (economyProject == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            _orgDigitalEconomyProjects.Remove(economyProject);

            return economyProject.Id;
        }
    }
}
