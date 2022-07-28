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
    public class OrgSocialSitesCommandHandler : IRequestHandler<OrgSocialSitesCommand, OrgSocialSitesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationSocials, int> _orgSocials;
        private readonly IRepository<OrganizationSocialParameters, int> _orgSocialParameters;

        public OrgSocialSitesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationSocials, int> orgSocials, IRepository<OrganizationSocialParameters, int> orgSocialParameters)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgSocials = orgSocials;
            _orgSocialParameters = orgSocialParameters;
        }
        public async Task<OrgSocialSitesCommandResult> Handle(OrgSocialSitesCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgSocialSitesCommandResult() { IsSuccess = true };
        }
        public void Add(OrgSocialSitesCommand model)
        {
           if(model.Social != null)
            {
                var org = _organization.Find(o => o.Id == model.Social.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.Social.OrganizationId.ToString());
                var socialSite = _orgSocials.Find(s => s.OrganizationId == model.Social.OrganizationId && s.MessengerLink == model.Social.MessengerLink).FirstOrDefault();
                if (socialSite != null)
                    throw ErrorStates.NotAllowed(model.Social.MessengerLink);
                if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                    throw ErrorStates.NotAllowed("permission");
                var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
                if (deadline == null)
                    throw ErrorStates.NotFound("available deadline");
                if (deadline.DeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

                OrganizationSocials addModel = new OrganizationSocials()
                {
                    OrganizationId = model.Social.OrganizationId,
                    MessengerLink = model.Social.MessengerLink
                };
                if(model.Social.Verified != null)
                {
                    addModel.Verified = model.Social.Verified;
                }
                _orgSocials.Add(addModel);
            }
            if (model.SocialParameter != null)
            {
                var org = _organization.Find(o => o.Id == model.SocialParameter.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.SocialParameter.OrganizationId.ToString());
                var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
                if (deadline == null)
                    throw ErrorStates.NotFound("available deadline");
                var socialParameter = _orgSocialParameters.Find(s => s.OrganizationId == model.SocialParameter.OrganizationId && s.DeadlineId == deadline.Id).FirstOrDefault();
                if (socialParameter != null)
                    throw ErrorStates.NotAllowed(model.SocialParameter.Id.ToString());
                if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                    throw ErrorStates.NotAllowed("permission");
                
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

                OrganizationSocialParameters addModel = new OrganizationSocialParameters();
                addModel.DeadlineId = deadline.Id;
                if (model.SocialParameter.OrgFullName != null)
                {
                    addModel.OrgFullName = model.SocialParameter.OrgFullName;
                }
                if (model.SocialParameter.OrgLegalSite != null)
                {
                    addModel.OrgLegalSite = model.SocialParameter.OrgLegalSite;
                }
                if (model.SocialParameter.OrgPhone != null)
                {
                    addModel.OrgPhone = model.SocialParameter.OrgPhone;
                }
                if (model.SocialParameter.OrgLegalAddress != null)
                {
                    addModel.OrgLegalAddress = model.SocialParameter.OrgLegalAddress;
                }
                if (model.SocialParameter.OrgEmail != null)
                {
                    addModel.OrgEmail = model.SocialParameter.OrgEmail;
                }
                if (model.SocialParameter.LinksToOtherSocials != null)
                {
                    addModel.LinksToOtherSocials = model.SocialParameter.LinksToOtherSocials;
                }
                if (model.SocialParameter.SyncronizedPosts != null)
                {
                    addModel.SyncronizedPosts = model.SocialParameter.SyncronizedPosts;
                }
                if (model.SocialParameter.Pool != null)
                {
                    addModel.Pool = model.SocialParameter.Pool;
                }
                _orgSocialParameters.Add(addModel);
            }
        }
        public void Update(OrgSocialSitesCommand model)
        {
            if (model.Social != null)
            {
                var org = _organization.Find(o => o.Id == model.Social.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.Social.OrganizationId.ToString());
                var socialSite = _orgSocials.Find(s => s.Id == model.Social.Id).FirstOrDefault();
                if (socialSite != null)
                    throw ErrorStates.NotAllowed(model.Social.MessengerLink);
                if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                    throw ErrorStates.NotAllowed("permission");
                var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
                if (deadline == null)
                    throw ErrorStates.NotFound("available deadline");
                if (deadline.DeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

                socialSite.MessengerLink = model.Social.MessengerLink;
                if (model.Social.Verified != null)
                {
                    socialSite.Verified = model.Social.Verified;
                }
                _orgSocials.Update(socialSite);
            }
            if (model.SocialParameter != null)
            {
                var org = _organization.Find(o => o.Id == model.SocialParameter.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.Social.OrganizationId.ToString());
                var socialParameter = _orgSocialParameters.Find(s => s.Id == model.SocialParameter.Id).FirstOrDefault();
                if (socialParameter != null)
                    throw ErrorStates.NotAllowed(model.Social.MessengerLink);
                if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                    throw ErrorStates.NotAllowed("permission");
                var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
                if (deadline == null)
                    throw ErrorStates.NotFound("available deadline");
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

                if (model.SocialParameter.OrgFullName != null)
                {
                    socialParameter.OrgFullName = model.SocialParameter.OrgFullName;
                }
                if (model.SocialParameter.OrgLegalSite != null)
                {
                    socialParameter.OrgLegalSite = model.SocialParameter.OrgLegalSite;
                }
                if (model.SocialParameter.OrgPhone != null)
                {
                    socialParameter.OrgPhone = model.SocialParameter.OrgPhone;
                }
                if (model.SocialParameter.OrgLegalAddress != null)
                {
                    socialParameter.OrgLegalAddress = model.SocialParameter.OrgLegalAddress;
                }
                if (model.SocialParameter.OrgEmail != null)
                {
                    socialParameter.OrgEmail = model.SocialParameter.OrgEmail;
                }
                if (model.SocialParameter.LinksToOtherSocials != null)
                {
                    socialParameter.LinksToOtherSocials = model.SocialParameter.LinksToOtherSocials;
                }
                if (model.SocialParameter.SyncronizedPosts != null)
                {
                    socialParameter.SyncronizedPosts = model.SocialParameter.SyncronizedPosts;
                }
                if (model.SocialParameter.Pool != null)
                {
                    socialParameter.Pool = model.SocialParameter.Pool;
                }
                _orgSocialParameters.Update(socialParameter);
            }
        }
        public void Delete(OrgSocialSitesCommand model)
        {
            if(model.Social!=null)
            {
                var social = _orgSocials.Find(s => s.Id == model.Social.Id).FirstOrDefault();
                if (social == null)
                    throw ErrorStates.NotFound(model.Social.Id.ToString());
                var org = _organization.Find(o => o.Id == social.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.Social.OrganizationId.ToString());
                
                if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                    throw ErrorStates.NotAllowed("permission");
                var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
                if (deadline == null)
                    throw ErrorStates.NotFound("available deadline");
                if (deadline.DeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
                _orgSocials.Remove(social);
            }
            if (model.SocialParameter != null)
            {
                var socialParameter = _orgSocialParameters.Find(s => s.Id == model.SocialParameter.Id).FirstOrDefault();
                if (socialParameter == null)
                    throw ErrorStates.NotFound(model.Social.Id.ToString());
                var org = _organization.Find(o => o.Id == socialParameter.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.Social.OrganizationId.ToString());

                if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                    throw ErrorStates.NotAllowed("permission");
                var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
                if (deadline == null)
                    throw ErrorStates.NotFound("available deadline");
                if (deadline.DeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
                _orgSocialParameters.Remove(socialParameter);
            }
        }
    }
}
