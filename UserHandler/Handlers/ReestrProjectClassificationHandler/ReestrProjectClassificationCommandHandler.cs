using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Results.ReestrProjectClassificationResult;
using UserHandler.Results.ReestrProjectIdentityResult;
using Domain.States;
using UserHandler.Commands.ReestrProjectIdentityCommand;
using System.Linq;
using Domain.Permission;
using Microsoft.EntityFrameworkCore;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain;

namespace UserHandler.Handlers.ReestrProjectClassificationHandler
{
    public class ReestrProjectClassificationCommandHandler : IRequestHandler<ReestrProjectClassificationCommand, ReestrProjectClassificationCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectClassifications, int> _projectClassifications;
        private readonly IRepository<ProjectClassifications, int> _classifications;

        public ReestrProjectClassificationCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectClassifications, int> projectClassifications, IRepository<ProjectClassifications, int> classifications)
        {
            _organization = organization;
            _deadline = deadline;
            _projectClassifications = projectClassifications;
            _classifications = classifications;
        }


        public async Task<ReestrProjectClassificationCommandResult> Handle(ReestrProjectClassificationCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ReestrProjectClassificationCommandResult() {Id = id, Success = true };
        }

        public int Add(ReestrProjectClassificationCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            var projectClassificator = _projectClassifications.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectClassificator != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            ReestrProjectClassifications addModel = new ReestrProjectClassifications();

            addModel.OrganizationId = model.OrganizationId;
            addModel.ReestrProjectId = model.ReestrProjectId;

            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if (!String.IsNullOrEmpty(model.OrgComment))
                    addModel.OrgComment = model.OrgComment;
                addModel.Exist = model.Exist;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;
                
                if (model.AllItems >= 0)
                    addModel.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    addModel.ExceptedItems = model.ExceptedItems;
            }


            _projectClassifications.Add(addModel);

            return addModel.Id;
        }
        public int Update(ReestrProjectClassificationCommand model)
        {
            var projectClassificator = _projectClassifications.Find(p => p.Id == model.Id).Include(mbox => mbox.Classifications).FirstOrDefault();
            if (projectClassificator == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            var org = _organization.Find(o => o.Id == projectClassificator.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);



            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if (!String.IsNullOrEmpty(model.OrgComment))
                    projectClassificator.OrgComment = model.OrgComment;
                projectClassificator.Exist = model.Exist;

                if (model.Exist == false)
                {
                    _classifications.RemoveRange(projectClassificator.Classifications);
                }
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectClassificator.ExpertComment = model.ExpertComment;
                
                if (model.AllItems >= 0)
                    projectClassificator.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    projectClassificator.ExceptedItems = model.ExceptedItems;
            }

            _projectClassifications.Update(projectClassificator);

            return projectClassificator.Id;
        }
        public int Delete(ReestrProjectClassificationCommand model)
        {
            var projectIdentities = _projectClassifications.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectIdentities == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            _projectClassifications.Remove(projectIdentities);

            return projectIdentities.Id;
        }
    }
}
