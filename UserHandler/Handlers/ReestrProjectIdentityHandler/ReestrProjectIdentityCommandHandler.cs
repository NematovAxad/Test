using Domain;
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
using UserHandler.Commands.ReestrProjectIdentityCommand;
using UserHandler.Results.ReestrProjectIdentityResult;

namespace UserHandler.Handlers.ReestrProjectIdentityHandler
{
    public class ReestrProjectIdentityCommandHandler : IRequestHandler<ReestrProjectIdentityCommand, ReestrProjectIdentityCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectIdentities, int> _projectIdentities;

        public ReestrProjectIdentityCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectIdentities, int> projectIdentities)
        {
            _organization = organization;
            _deadline = deadline;
            _projectIdentities = projectIdentities;
        }
        public async Task<ReestrProjectIdentityCommandResult> Handle(ReestrProjectIdentityCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ReestrProjectIdentityCommandResult() { IsSuccess = true };
        }
        public void Add(ReestrProjectIdentityCommand model)
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

            var projectIdentities = _projectIdentities.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectIdentities != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            ReestrProjectIdentities addModel = new ReestrProjectIdentities
            {
                OrganizationId = model.OrganizationId,
                ReestrProjectId = model.ReestrProjectId,
                ExpertComment = model.ExpertComment,
                ExpertExcept = model.ExpertExcept
            };
           
            _projectIdentities.Add(addModel);
        }
        public void Update(ReestrProjectIdentityCommand model)
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

            var projectIdentities = _projectIdentities.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectIdentities == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            
            projectIdentities.ExpertComment = model.ExpertComment;
            projectIdentities.ExpertExcept = model.ExpertExcept;

            _projectIdentities.Update(projectIdentities);
        }
        public void Delete(ReestrProjectIdentityCommand model)
        {
            var projectIdentities = _projectIdentities.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectIdentities == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            _projectIdentities.Remove(projectIdentities);
        }
    }
}
