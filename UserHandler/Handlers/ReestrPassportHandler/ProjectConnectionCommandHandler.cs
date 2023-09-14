using Domain;
using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
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
using MainInfrastructures.Interfaces;
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
        private readonly IReesterService _reesterService;

        public ProjectConnectionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectConnection, int> projectConnection, IRepository<ProjectConnections, int> connections, IReesterService reesterService)
        {
            _organization = organization;
            _deadline = deadline;
            _projectConnection = projectConnection;
            _connections = connections;
            _reesterService = reesterService;
        }
        public async Task<ProjectConnectionResult> Handle(ProjectConnectionCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ProjectConnectionResult() {Id = id,  IsSuccess = true };
        }
        public int Add(ProjectConnectionCommand model)
        {
            if (model.AllItems < model.ExceptedItems)
                throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);
            int id = 0;
            
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");


            var projectConnection = _projectConnection.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectConnection != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                ReestrProjectConnection addModel = new ReestrProjectConnection();
                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                if (!String.IsNullOrEmpty(model.OrgComment))
                    addModel.OrgComment = model.OrgComment;
                addModel.Exist = model.Exist;
                addModel.UserPinfl = model.UserPinfl;
                addModel.LastUpdate = DateTime.Now;

                _projectConnection.Add(addModel);

                id = addModel.Id;
            }
               
            if(model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p==Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                ReestrProjectConnection addModel = new ReestrProjectConnection();
                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                addModel.Exist = model.Exist;

                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    addModel.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    addModel.ExceptedItems = model.ExceptedItems;

                addModel.UserPinfl = model.UserPinfl;
                addModel.LastUpdate = DateTime.Now;
                
                _projectConnection.Add(addModel);

                id = addModel.Id;
            }

            _reesterService.RecordUpdateTime(model.ReestrProjectId);

            return id;
        }
        public int Update(ProjectConnectionCommand model)
        {
            if (model.AllItems < model.ExceptedItems)
                throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);
            
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            
            

            var projectConnection = _projectConnection.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).Include(mbox => mbox.Connections).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());
            

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.OrgComment))
                    projectConnection.OrgComment = model.OrgComment;
                projectConnection.Exist = model.Exist;

                if(model.Exist == false)
                {
                    _connections.RemoveRange(projectConnection.Connections);
                }
            }
                
            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectConnection.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    projectConnection.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    projectConnection.ExceptedItems = model.ExceptedItems;
            }
            
            projectConnection.UserPinfl = model.UserPinfl;
            projectConnection.LastUpdate = DateTime.Now;
            _projectConnection.Update(projectConnection);

            _reesterService.RecordUpdateTime(projectConnection.ReestrProjectId);
            
            return projectConnection.Id;
        }

        public int Delete(ProjectConnectionCommand model)
        {
            var projectConnection = _projectConnection.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());
            _projectConnection.Remove(projectConnection);

            return projectConnection.Id;
        }
    }
}
