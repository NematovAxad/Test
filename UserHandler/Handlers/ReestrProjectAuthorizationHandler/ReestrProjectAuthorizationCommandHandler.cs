using Domain.Models;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Results.ReestrProjectClassificationResult;
using UserHandler.Commands.ReestrProjectAuthorizationCommand;
using UserHandler.Results.ReestrProjectAuthorizationResult;
using System.Threading.Tasks;
using System.Threading;
using Domain.States;
using System.Linq;
using Domain.Permission;
using Microsoft.EntityFrameworkCore;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain;

namespace UserHandler.Handlers.ReestrProjectAuthorizationHandler
{
    public class ReestrProjectAuthorizationCommandHandler : IRequestHandler<ReestrProjectAuthorizationCommand, ReestrProjectAuthorizationCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ProjectAuthorizations, int> _authorizations;
        private readonly IRepository<ReestrProjectAuthorizations, int> _projectAuthorization;

        public ReestrProjectAuthorizationCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ProjectAuthorizations, int> authorizations, IRepository<ReestrProjectAuthorizations, int> projectAuthorization)
        {
            _organization = organization;
            _deadline = deadline;
            _authorizations = authorizations;
            _projectAuthorization = projectAuthorization;
        }

        public async Task<ReestrProjectAuthorizationCommandResult> Handle(ReestrProjectAuthorizationCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ReestrProjectAuthorizationCommandResult() {Id = id, Success = true };
        }

        public int Add(ReestrProjectAuthorizationCommand model)
        {
            int id = 0;

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            

            var projectAuthorization = _projectAuthorization.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectAuthorization != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
           

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);
                
                ReestrProjectAuthorizations addModel = new ReestrProjectAuthorizations();

                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                if (!String.IsNullOrEmpty(model.OrgComment))
                    addModel.OrgComment = model.OrgComment;
                addModel.Exist = model.Exist;

                _projectAuthorization.Add(addModel);

                id = addModel.Id;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);


                ReestrProjectAuthorizations addModel = new ReestrProjectAuthorizations();

                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    addModel.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    addModel.ExceptedItems = model.ExceptedItems;

                _projectAuthorization.Add(addModel);

                id = addModel.Id;
            }


            

            return id;
        }
        public int Update(ReestrProjectAuthorizationCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            

            var projectAuthorization = _projectAuthorization.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).Include(mbox => mbox.Authorizations).FirstOrDefault();
            if (projectAuthorization == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.OrgComment))
                    projectAuthorization.OrgComment = model.OrgComment;
                projectAuthorization.Exist = model.Exist;

                if (model.Exist == false)
                {
                    _authorizations.RemoveRange(projectAuthorization.Authorizations);
                }
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectAuthorization.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    projectAuthorization.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    projectAuthorization.ExceptedItems = model.ExceptedItems;
            }

            _projectAuthorization.Update(projectAuthorization);

            return projectAuthorization.Id;
        }
        public int Delete(ReestrProjectAuthorizationCommand model)
        {
            var projectIdentities = _projectAuthorization.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectIdentities == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            _projectAuthorization.Remove(projectIdentities);

            return projectIdentities.Id;
        }
    }
}
