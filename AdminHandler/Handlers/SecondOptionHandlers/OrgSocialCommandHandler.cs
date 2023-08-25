using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrgSocialCommandHandler : IRequestHandler<OrgSocialCommand, OrgSocialCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;

        private readonly IRepository<OrganizationSocials, int> _orgSocials;


        public OrgSocialCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationSocials, int> orgSocials)
        {
            _organization = organization;
            _deadline = deadline;
            _orgSocials = orgSocials;

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
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
           
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);
            
            if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.SecondSectionDeadlineDate.ToString());
            
            if (!(model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            OrganizationSocials addModel = new OrganizationSocials()
            {
                OrganizationId = model.OrganizationId,
                MessengerLink = model.MessengerLink
            };
            if (model.Pool != null)
            {
                addModel.Pool = model.Pool;
            }
            addModel.PoolScreenshotLink = model.PoolScreenshot;
            addModel.PoolLink = model.PoolLink;
            addModel.PoolComment = model.PoolComment;
            addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _orgSocials.Add(addModel);
        }
        public void Update(OrgSocialCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var socialSite = _orgSocials.Find(s => s.Id == model.Id).Include(mbox=>mbox.Organizations).FirstOrDefault();
            if (socialSite == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.OperatorDeadlineDate.ToString());

            if (((model.UserOrgId == socialSite.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
            {
                socialSite.MessengerLink = model.MessengerLink;

                if (model.Pool != null)
                {
                    socialSite.Pool = model.Pool;
                }

                socialSite.PoolScreenshotLink = model.PoolScreenshot;
                socialSite.PoolLink = model.PoolLink;
                socialSite.PoolComment = model.PoolComment;
            }
            if(model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (model.Verified != null)
                {
                    socialSite.Verified = model.Verified;
                }
                if (model.OrgFullName != null)
                {
                    socialSite.OrgFullName = model.OrgFullName;
                }
                if (model.OrgLegalSite != null)
                {
                    socialSite.OrgLegalSite = model.OrgLegalSite;
                }
                if (model.OrgPhone != null)
                {
                    socialSite.OrgPhone = model.OrgPhone;
                }
                if (model.OrgLegalAddress != null)
                {
                    socialSite.OrgLegalAddress = model.OrgLegalAddress;
                }
                if (model.OrgEmail != null)
                {
                    socialSite.OrgEmail = model.OrgEmail;
                }
                if (model.LinksToOtherSocials != null)
                {
                    socialSite.LinksToOtherSocials = model.LinksToOtherSocials;
                }
                if (model.SyncronizedPosts != null)
                {
                    socialSite.SyncronizedPosts = model.SyncronizedPosts;
                }
                

                socialSite.IsMain = model.IsMain;
       
                socialSite.Link1 = model.Link1;
                socialSite.Link2 = model.Link2;
                socialSite.Link3 = model.Link3;
                socialSite.Link4 = model.Link4;
                socialSite.Link5 = model.Link5;

                socialSite.Post1Link = model.Post1Link;
                socialSite.Post2Link = model.Post2Link;
                socialSite.Post3Link = model.Post3Link;
                socialSite.Post4Link = model.Post4Link;
                socialSite.Post5Link = model.Post5Link;

                socialSite.Post1 = model.Post1;
                socialSite.Post2 = model.Post2;
                socialSite.Post3 = model.Post3;
                socialSite.Post4 = model.Post4;
                socialSite.Post5 = model.Post5;


                socialSite.CommentToSocialSite = model.CommentToSocialSite;

                if(model.PoolExceptExpert!=null)
                {
                    socialSite.PoolExceptExpert = model.PoolExceptExpert;
                }
                socialSite.PoolCommentExpert = model.PoolCommentExpert;
                
            }
            socialSite.UserPinfl = model.UserPinfl;
            socialSite.LastUpdate = DateTime.Now;
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
            if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.SecondSectionDeadlineDate.ToString());
            _orgSocials.Remove(social);
        }
    }
}
