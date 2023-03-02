using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using Domain.Models.FirstSection;
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
    public class OrganizationAppsCommandHandler : IRequestHandler<OrganizationAppCommand, OrganizationAppsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationApps, int> _organizationApps;

        public OrganizationAppsCommandHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationApps, int> organizationApps)
        {
            _organizationApps = organizationApps;
            _organizations = organizations;
        }

        public async Task<OrganizationAppsCommandResult> Handle(OrganizationAppCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrganizationAppsCommandResult() { IsSuccess = true };
        }
        public void Add(OrganizationAppCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var apps = _organizationApps.Find(a => a.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (apps != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            OrganizationApps addModel = new OrganizationApps()
            {
                OrganizationId = model.OrganizationId,
                HasAndroidApp = model.HasAndroidApp,
                AndroidAppLink = model.AndroidAppLink,
                HasIosApp = model.HasIosApp,
                IosAppLink = model.IosAppLink,
                HasOtherApps = model.HasOtherApps,
                OtherAppLink = model.OtherAppLink,
                HasResponsiveWebsite = model.HasResponsiveWebsite
            };
            _organizationApps.Add(addModel);
        }
        public void Update(OrganizationAppCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var apps = _organizationApps.Find(a => a.Id == model.Id).FirstOrDefault();
            if (apps == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            apps.HasAndroidApp = model.HasAndroidApp;
            apps.AndroidAppLink = model.AndroidAppLink;
            apps.HasIosApp = model.HasIosApp;
            apps.IosAppLink = model.IosAppLink;
            apps.HasOtherApps = model.HasOtherApps;
            apps.OtherAppLink = model.OtherAppLink;
            apps.HasResponsiveWebsite = model.HasResponsiveWebsite;
            
            _organizationApps.Update(apps);
        }
        public void Delete(OrganizationAppCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var apps = _organizationApps.Find(a => a.Id == model.Id).FirstOrDefault();
            if (apps == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _organizationApps.Remove(apps);
        }
    }
}
