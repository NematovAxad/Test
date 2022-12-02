using Domain.Models;
using Domain.Models.SecondSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<ProjectConnections, int> _connections;

        public ProjectConnectionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectConnection, int> projectConnection, IRepository<ProjectConnections, int> connections)
        {
            _organization = organization;
            _deadline = deadline;
            _projectConnection = projectConnection;
            _connections = connections;
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

            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if(!String.IsNullOrEmpty(model.OrgComment))
                    addModel.OrgComment = model.OrgComment;
                addModel.Exist = model.Exist;
            }
               
            if(model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p==Permissions.OPERATOR_RIGHTS))
            {
                if(!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;

                if (model.AllItems > 0)
                    addModel.AllItems = model.AllItems;

                if (model.ExceptedItems > 0)
                    addModel.Exceptedtems = model.ExceptedItems;
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

            var projectConnection = _projectConnection.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).Include(mbox => mbox.Connections).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());
            

            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if(!String.IsNullOrEmpty(model.OrgComment))
                    projectConnection.OrgComment = model.OrgComment;
                projectConnection.Exist = model.Exist;

                if(model.Exist == false)
                {
                    _connections.RemoveRange(projectConnection.Connections);
                }
            }
                
            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectConnection.ExpertComment = model.ExpertComment;

                if (model.AllItems > 0)
                    projectConnection.AllItems = model.AllItems;

                if (model.ExceptedItems > 0)
                    projectConnection.Exceptedtems = model.ExceptedItems;
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
