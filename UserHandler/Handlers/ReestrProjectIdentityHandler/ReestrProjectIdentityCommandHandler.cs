using Domain;
using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MainInfrastructures.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrProjectIdentityCommand;
using UserHandler.Results.ReestrProjectIdentityResult;

namespace UserHandler.Handlers.ReestrProjectIdentityHandler
{
    public class ReestrProjectIdentityCommandHandler : IRequestHandler<ReestrProjectIdentityCommand, ReestrProjectIdentityCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectIdentities, int> _projectIdentities;
        private readonly IRepository<ProjectIdentities, int> _identities;

        public ReestrProjectIdentityCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectIdentities, int> projectIdentities, IRepository<ProjectIdentities, int> identities)
        {
            _organization = organization;
            _deadline = deadline;
            _projectIdentities = projectIdentities;
            _identities = identities;
        }
        public async Task<ReestrProjectIdentityCommandResult> Handle(ReestrProjectIdentityCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ReestrProjectIdentityCommandResult() {Id = id, IsSuccess = true };
        }
        public int Add(ReestrProjectIdentityCommand model)
        {
            int id = 0;
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
           
           

            var projectIdentities = _projectIdentities.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectIdentities != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);
                
                ReestrProjectIdentities addModel = new ReestrProjectIdentities();

                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                if (!String.IsNullOrEmpty(model.OrgComment))
                    addModel.OrgComment = model.OrgComment;
                addModel.Exist = model.Exist;

                _projectIdentities.Add(addModel);
                id = addModel.Id;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);


                ReestrProjectIdentities addModel = new ReestrProjectIdentities();

                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;
                if(model.Exist == true)
                {
                    if (model.AllItems >= 0)
                        addModel.AllItems = model.AllItems;

                    if (model.ExceptedItems >= 0)
                        addModel.ExceptedItems = model.ExceptedItems;
                }

                _projectIdentities.Add(addModel);
                id = addModel.Id;
            }


           

            return id;
        }
        public int Update(ReestrProjectIdentityCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

           

            var projectIdentities = _projectIdentities.Find(p => p.Id == model.Id).Include(mbox=>mbox.Identities).FirstOrDefault();
            if (projectIdentities == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.OrgComment))
                    projectIdentities.OrgComment = model.OrgComment;
                projectIdentities.Exist = model.Exist;

                if (model.Exist == false)
                {
                    _identities.RemoveRange(projectIdentities.Identities);
                }
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectIdentities.ExpertComment = model.ExpertComment;
                
                if (model.AllItems >= 0)
                    projectIdentities.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    projectIdentities.ExceptedItems = model.ExceptedItems;
            }

           
            

            _projectIdentities.Update(projectIdentities);

            return projectIdentities.Id;
        }
        public int Delete(ReestrProjectIdentityCommand model)
        {
            var projectIdentities = _projectIdentities.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectIdentities == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            _projectIdentities.Remove(projectIdentities);

            return projectIdentities.Id;
        }
    }
}
