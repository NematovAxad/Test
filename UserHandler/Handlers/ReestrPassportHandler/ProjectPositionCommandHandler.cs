using DocumentFormat.OpenXml.Office2013.Excel;
using Domain;
using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Results.ReestrPassportResult;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ProjectPositionCommandHandler : IRequestHandler<ProjectPositionCommand, ProjectPositionCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectPosition, int> _projectPosition;

        public ProjectPositionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectPosition, int> projectPosition)
        {
            _organization = organization;
            _deadline = deadline;
            _projectPosition = projectPosition;
        }

        public async Task<ProjectPositionCommandResult> Handle(ProjectPositionCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ProjectPositionCommandResult() { Id = id, IsSuccess = true };
        }
        public int Add(ProjectPositionCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS) && !model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            

            var projectPosition = _projectPosition.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectPosition != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            ReestrProjectPosition addModel = new ReestrProjectPosition();
            addModel.OrganizationId = model.OrganizationId;
            addModel.ReestrProjectId = model.ReestrProjectId;

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                addModel.ProjectStatus = model.ProjectStatus;
                addModel.ExpertExcept = model.ExpertExcept;
                if(!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;
            }
            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                addModel.ProjectStatus = model.ProjectStatus;
                if (!String.IsNullOrEmpty(model.FilePath))
                    addModel.FilePath = model.FilePath;
            }
                

                _projectPosition.Add(addModel);

            return addModel.Id;
        }
        public int Update(ProjectPositionCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            

            var projectPosition = _projectPosition.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectPosition == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                projectPosition.ProjectStatus = model.ProjectStatus;
                projectPosition.ExpertExcept = model.ExpertExcept;
                if(!String.IsNullOrEmpty(model.ExpertComment))
                    projectPosition.ExpertComment = model.ExpertComment;

            }
            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                projectPosition.ProjectStatus = model.ProjectStatus;
                if (!String.IsNullOrEmpty(model.FilePath))
                    projectPosition.FilePath = model.FilePath;
            }
               
            _projectPosition.Update(projectPosition);

            return projectPosition.Id;
        }
        public int Delete(ProjectPositionCommand model)
        {
            var projectPosition = _projectPosition.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectPosition == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            _projectPosition.Remove(projectPosition);

            return projectPosition.Id;
        }
    }
}
