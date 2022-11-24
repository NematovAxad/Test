using Domain.Models;
using Domain.Models.SecondSection;
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
    public class ProjectConnectionCommandHandler : IRequestHandler<ProjectConnectionCommand, ProjectConnectionResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectConnection, int> _projectConnection;

        public ProjectConnectionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectConnection, int> projectConnection)
        {
            _organization = organization;
            _deadline = deadline;
            _projectConnection = projectConnection;
        }
        public async Task<ProjectConnectionResult> Handle(ProjectConnectionCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ProjectConnectionResult() { IsSuccess = true };
        }
        public void Add(ProjectConnectionCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            var projectConnection = _projectConnection.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectConnection != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            ReestrProjectConnection addModel = new ReestrProjectConnection();
            addModel.OrganizationId = model.OrganizationId;
            addModel.ReestrProjectId = model.ReestrProjectId;

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if(!String.IsNullOrEmpty(model.OrgComment))
                    addModel.OrgComment = model.OrgComment;
            }
               
            if(!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p==Permissions.OPERATOR_RIGHTS))
            {
                if(!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;
                addModel.ExpertExcept = model.ExpertExcept;
            }

            _projectConnection.Add(addModel);    
        }
        public void Update(ProjectConnectionCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            var projectConnection = _projectConnection.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());
            

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if(!String.IsNullOrEmpty(model.OrgComment))
                    projectConnection.OrgComment = model.OrgComment;
            }
                
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectConnection.ExpertComment = model.ExpertComment;
                projectConnection.ExpertExcept = model.ExpertExcept;
            }
            _projectConnection.Update(projectConnection);
        }

        public void Delete(ProjectConnectionCommand model)
        {
            var projectConnection = _projectConnection.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());
            _projectConnection.Remove(projectConnection);
        }
    }
}
