﻿using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain;
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
            if (model.OrgFullName != null)
            {
                addModel.OrgFullName = model.OrgFullName;
            }
            if (model.OrgLegalSite != null)
            {
                addModel.OrgLegalSite = model.OrgLegalSite;
            }
            if (model.OrgPhone != null)
            {
                addModel.OrgPhone = model.OrgPhone;
            }
            if (model.OrgLegalAddress != null)
            {
                addModel.OrgLegalAddress = model.OrgLegalAddress;
            }
            if (model.OrgEmail != null)
            {
                addModel.OrgEmail = model.OrgEmail;
            }
            if (model.LinksToOtherSocials != null)
            {
                addModel.LinksToOtherSocials = model.LinksToOtherSocials;
            }
            if (model.SyncronizedPosts != null)
            {
                addModel.SyncronizedPosts = model.SyncronizedPosts;
            }
            if (model.Pool != null)
            {
                addModel.Pool = model.Pool;
            }
            if (model.Verified != null)
            {
                addModel.Verified = model.Verified;
            }
            addModel.IsMain = model.IsMain;
            addModel.Post1 = model.Post1;
            addModel.Post2 = model.Post2;
            addModel.Post3 = model.Post3;
            addModel.Post4 = model.Post4;
            addModel.Post5 = model.Post5;
            if (!String.IsNullOrEmpty(model.Post1Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post1Screenshot);
                addModel.Post1Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post2Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post2Screenshot);
                addModel.Post2Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post3Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post3Screenshot);
                addModel.Post3Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post4Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post4Screenshot);
                addModel.Post4Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post5Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post5Screenshot);
                addModel.Post5Link = filePath;
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
            if (model.Pool != null)
            {
                socialSite.Pool = model.Pool;
            }
            socialSite.IsMain = model.IsMain;
            socialSite.Post1 = model.Post1;
            socialSite.Post2 = model.Post2;
            socialSite.Post3 = model.Post3;
            socialSite.Post4 = model.Post4;
            socialSite.Post5 = model.Post5;
            if (!String.IsNullOrEmpty(model.Post1Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post1Screenshot);
                socialSite.Post1Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post2Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post2Screenshot);
                socialSite.Post2Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post3Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post3Screenshot);
                socialSite.Post3Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post4Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post4Screenshot);
                socialSite.Post4Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Post5Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Post5Screenshot);
                socialSite.Post5Link = filePath;
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
