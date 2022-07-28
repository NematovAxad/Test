using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using Domain.Models.Ranking;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrgSocialCommandHandler : IRequestHandler<OrgSocialCommand, OrgSocialCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationSocials, int> _orgSocials;
        private readonly IRepository<OrganizationSocialParameters, int> _orgSocialParameters;

        public OrgSocialCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationSocials, int> orgSocials, IRepository<OrganizationSocialParameters, int> orgSocialParameters)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgSocials = orgSocials;
            _orgSocialParameters = orgSocialParameters;
        }
        public async Task<OrgSocialCommandResult> Handle(OrgSocialCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgSocialCommandResult() { IsSuccess = true };
        }
        public void Add(OrgSocialCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var socialSite = _orgSocials.Find(s => s.OrganizationId == model.OrganizationId && s.MessengerLink == model.MessengerLink).FirstOrDefault();
            if (socialSite != null)
                throw ErrorStates.NotAllowed(model.MessengerLink);
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            OrganizationSocials addModel = new OrganizationSocials()
            {
                OrganizationId = model.OrganizationId,
                MessengerLink = model.MessengerLink
            };
            if(model.Verified != null)
            {
                addModel.Verified = model.Verified;
            }
            _orgSocials.Add(addModel);
        }
        public void Update(OrgSocialCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var socialSite = _orgSocials.Find(s => s.Id == model.Id).FirstOrDefault();
            if (socialSite == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            socialSite.MessengerLink = model.MessengerLink;
            if (model.Verified != null)
            {
                socialSite.Verified = model.Verified;
            }
            _orgSocials.Update(socialSite);
        }
        public void Delete(OrgSocialCommand model)
        {
            var social = _orgSocials.Find(s => s.Id == model.Id).FirstOrDefault();
            if (social == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organization.Find(o => o.Id == social.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
                
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            _orgSocials.Remove(social);
        }
    }
}
