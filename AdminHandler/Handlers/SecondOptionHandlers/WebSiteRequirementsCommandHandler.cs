using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class WebSiteRequirementsCommandHandler : IRequestHandler<WebSiteRequirementsCommand, WebSiteRequirementsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<WebSiteRequirements, int> _websiteRequirements;
        
        public WebSiteRequirementsCommandHandler(IRepository<Organizations, int> organizations, IRepository<WebSiteRequirements, int> websiteRequirements)
        {
            _organizations = organizations;
            _websiteRequirements = websiteRequirements;
        }

        public async Task<WebSiteRequirementsCommandResult> Handle(WebSiteRequirementsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new WebSiteRequirementsCommandResult() { IsSuccess = true };
        }
        public void Add(WebSiteRequirementsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var requirement = _websiteRequirements.Find(m => m.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (requirement != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            WebSiteRequirements addModel = new WebSiteRequirements()
            {
                OrganizationId = org.Id,
                Requirement1 = model.Requirement1,
                Requirement2 = model.Requirement2,
                Requirement3 = model.Requirement3,
                Requirement4 = model.Requirement4,
                Requirement5 = model.Requirement5,
                Requirement6 = model.Requirement6,
                Requirement7 = model.Requirement7,
                Requirement8 = model.Requirement8,
                Requirement9 = model.Requirement9,
                Requirement10 = model.Requirement10,
                Requirement11 = model.Requirement11,
                Requirement12 = model.Requirement12,
                Requirement13 = model.Requirement13,
                Requirement14 = model.Requirement14,
                Requirement15 = model.Requirement15,
                Requirement16 = model.Requirement16,
                Requirement17 = model.Requirement17,
                Requirement18 = model.Requirement18,
                Requirement19 = model.Requirement19,
                Requirement20 = model.Requirement20,
                Requirement21 = model.Requirement21,
                Requirement22 = model.Requirement22,
                Requirement23 = model.Requirement23,
                Requirement24 = model.Requirement24,
                Requirement25 = model.Requirement25,
                Requirement26 = model.Requirement26,
                Requirement27 = model.Requirement27,
                Requirement28 = model.Requirement28,
                Requirement29 = model.Requirement29,
                Requirement30 = model.Requirement30,
                Requirement31 = model.Requirement31,
                Requirement32 = model.Requirement32,
                Requirement33 = model.Requirement33,
                Requirement34 = model.Requirement34,
                Requirement35 = model.Requirement35,
                Requirement36 = model.Requirement36,
                Requirement37 = model.Requirement37,
                Requirement38 = model.Requirement38,
                Requirement39 = model.Requirement39,
                Requirement40 = model.Requirement40,
                Requirement41 = model.Requirement41,
                Requirement42 = model.Requirement42,
                Requirement43 = model.Requirement43,
                Requirement44 = model.Requirement44,
                Requirement45 = model.Requirement45,
            };
            _websiteRequirements.Add(addModel); 
        }
        public void Update(WebSiteRequirementsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var requirement = _websiteRequirements.Find(m => m.Id == model.Id).FirstOrDefault();
            if (requirement != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            requirement.Requirement1 = model.Requirement1;
            requirement.Requirement2 = model.Requirement2;
            requirement.Requirement3 = model.Requirement3;
            requirement.Requirement4 = model.Requirement4;
            requirement.Requirement5 = model.Requirement5;
            requirement.Requirement6 = model.Requirement6;
            requirement.Requirement7 = model.Requirement7;
            requirement.Requirement8 = model.Requirement8;
            requirement.Requirement9 = model.Requirement9;
            requirement.Requirement10 = model.Requirement10;
            requirement.Requirement11 = model.Requirement11;
            requirement.Requirement12 = model.Requirement12;
            requirement.Requirement13 = model.Requirement13;
            requirement.Requirement14 = model.Requirement14;
            requirement.Requirement15 = model.Requirement15;
            requirement.Requirement16 = model.Requirement16;
            requirement.Requirement17 = model.Requirement17;
            requirement.Requirement18 = model.Requirement18;
            requirement.Requirement19 = model.Requirement19;
            requirement.Requirement20 = model.Requirement20;
            requirement.Requirement21 = model.Requirement21;
            requirement.Requirement22 = model.Requirement22;
            requirement.Requirement23 = model.Requirement23;
            requirement.Requirement24 = model.Requirement24;
            requirement.Requirement25 = model.Requirement25;
            requirement.Requirement26 = model.Requirement26;
            requirement.Requirement27 = model.Requirement27;
            requirement.Requirement28 = model.Requirement28;
            requirement.Requirement29 = model.Requirement29;
            requirement.Requirement30 = model.Requirement30;
            requirement.Requirement31 = model.Requirement31;
            requirement.Requirement32 = model.Requirement32;
            requirement.Requirement33 = model.Requirement33;
            requirement.Requirement34 = model.Requirement34;
            requirement.Requirement35 = model.Requirement35;
            requirement.Requirement36 = model.Requirement36;
            requirement.Requirement37 = model.Requirement37;
            requirement.Requirement38 = model.Requirement38;
            requirement.Requirement39 = model.Requirement39;
            requirement.Requirement40 = model.Requirement40;
            requirement.Requirement41 = model.Requirement41;
            requirement.Requirement42 = model.Requirement42;
            requirement.Requirement43 = model.Requirement43;
            requirement.Requirement44 = model.Requirement44;
            requirement.Requirement45 = model.Requirement45;
            _websiteRequirements.Update(requirement);
        }
        public void Delete(WebSiteRequirementsCommand model)
        {
            var requirement = _websiteRequirements.Find(m => m.Id == model.Id).FirstOrDefault();
            if (requirement != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == requirement.Id) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _websiteRequirements.Remove(requirement);
        }
    }
}
