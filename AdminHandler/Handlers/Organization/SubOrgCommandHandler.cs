using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
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

namespace AdminHandler.Handlers.Organization
{
    public class SubOrgCommandHandler : IRequestHandler<SubOrgCommand, SubOrgCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<SubOrganizations, int> _subOrganizations;

        public SubOrgCommandHandler(IRepository<Organizations, int> organizations, IRepository<SubOrganizations, int> subOrganizations)
        {
            _organizations = organizations;
            _subOrganizations = subOrganizations;
        }

        public async Task<SubOrgCommandResult> Handle(SubOrgCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new SubOrgCommandResult() { IsSuccess = true };
        }
        public void Add(SubOrgCommand model)
        {
            var subOrg = _subOrganizations.Find(o => o.Name == model.Name).FirstOrDefault();
            if (subOrg != null)
                throw ErrorStates.NotAllowed(model.Name);
            var org = _organizations.Find(o => o.Id == model.ParentId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            SubOrganizations addModel = new SubOrganizations()
            {
                OrganizationId = model.ParentId,
                Name = model.Name,
                DirectorFirstName = model.DirectorFirstName,
                DirectorLastName = model.DirectorLastName,
                DirectorMidName = model.DirectorMidName,
                OwnerType = model.OwnerType,
                OfficialSite = model.OfficialSite,
                Contacts = model.Contacts
            };

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) )
                if(!String.IsNullOrEmpty(model.Inn))
                    addModel.Inn = model.Inn;
                
            _subOrganizations.Add(addModel);
        }
        public void Update(SubOrgCommand model)
        {
            var subOrg = _subOrganizations.Find(o => o.Id == model.Id).FirstOrDefault();
            if (subOrg == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organizations.Find(o => o.Id == subOrg.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            subOrg.Name = model.Name;
            subOrg.DirectorFirstName = model.DirectorFirstName;
            subOrg.DirectorLastName = model.DirectorLastName;
            subOrg.DirectorMidName = model.DirectorMidName;
            subOrg.OwnerType = model.OwnerType;
            subOrg.OfficialSite = model.OfficialSite;
            subOrg.Contacts = model.Contacts;

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER))
                if (!String.IsNullOrEmpty(model.Inn))
                    subOrg.Inn = model.Inn;

            _subOrganizations.Update(subOrg);
        }
        public void Delete(SubOrgCommand model)
        {
            var subOrg = _subOrganizations.Find(o => o.Id == model.Id).FirstOrDefault();
            if (subOrg == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organizations.Find(o => o.Id == subOrg.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _subOrganizations.Remove(model.Id);
        }
    }
}
