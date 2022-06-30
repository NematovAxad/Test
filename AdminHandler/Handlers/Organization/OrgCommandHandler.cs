using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain.Models;
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
    public class OrgCommandHandler : IRequestHandler<OrgCommand, OrgCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;

        public OrgCommandHandler(IRepository<Organizations, int> organization)
        {
            _organization = organization;
        }

        public async Task<OrgCommandResult> Handle(OrgCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: AddOrg(request); break;
                case Domain.Enums.EventType.Update: UpdateOrg(request); break;
                case Domain.Enums.EventType.Delete: DeleteOrg(request); break;
            }
            return new OrgCommandResult() { IsSuccess = true };
        }
        public void AddOrg(OrgCommand model)
        {
            var org = _organization.Find(o => o.ShortName == model.ShortName).FirstOrDefault();

            if (org != null)
            {
                throw ErrorStates.NotAllowed(model.ShortName);
            }
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER))
                throw ErrorStates.NotAllowed("permission");
            Organizations addModel = new Organizations();

            if (model.UserServiceId != 0)
                addModel.UserServiceId = model.UserServiceId;

            if (!String.IsNullOrEmpty(model.FullName))
                addModel.FullName = model.FullName;
            
            if (!String.IsNullOrEmpty(model.ShortName))
                addModel.ShortName = model.ShortName;

            if (!String.IsNullOrEmpty(model.DirectorFirstName))
                addModel.DirectorFirstName = model.DirectorFirstName;

            if (!String.IsNullOrEmpty(model.DirectorLastName))
                addModel.DirectorLastName = model.DirectorLastName;

            if (!String.IsNullOrEmpty(model.DirectorMidName))
                addModel.DirectorMidName = model.DirectorMidName;

            if (!String.IsNullOrEmpty(model.DirectorPosition))
                addModel.DirectorPosition = model.DirectorPosition;

            if (!String.IsNullOrEmpty(model.PhoneNumber))
                addModel.PhoneNumber = model.PhoneNumber;

            if (!String.IsNullOrEmpty(model.AddressHomeNo))
                addModel.AddressHomeNo = model.AddressHomeNo;

            if (!String.IsNullOrEmpty(model.AddressStreet))
                addModel.AddressStreet = model.AddressStreet;

            if (!String.IsNullOrEmpty(model.AddressProvince))
                addModel.AddressProvince = model.AddressProvince;

            if (!String.IsNullOrEmpty(model.AddressDistrict))
                addModel.AddressDistrict = model.AddressDistrict;

            if (!String.IsNullOrEmpty(model.PostIndex))
                addModel.PostIndex = model.PostIndex;

            if (!String.IsNullOrEmpty(model.Department))
                addModel.Department = model.Department;

            if (!String.IsNullOrEmpty(model.DirectorMail))
                addModel.DirectorMail = model.DirectorMail;

            if (!String.IsNullOrEmpty(model.OrgMail))
                addModel.OrgMail = model.OrgMail;

            if (!String.IsNullOrEmpty(model.WebSite))
                addModel.WebSite = model.WebSite;

            if (model.OrgType == 0)
                addModel.OrgType = model.OrgType;

            if (!String.IsNullOrEmpty(model.Fax))
                addModel.Fax = model.Fax;

            if (model.OrgCategory == 0)
                addModel.OrgCategory = model.OrgCategory;

            addModel.IsActive = model.IsActive;
            addModel.IsIct = model.IsIct;
            addModel.IsMonitoring = model.IsMonitoring;
            
            _organization.Add(addModel);
        }
        public void UpdateOrg(OrgCommand model)
        {
            if (model.Id==0)
            {
                throw ErrorStates.NotFound("");
            }
            var org = _organization.Find(r => r.Id == model.Id).FirstOrDefault();
            if (org == null)
            {
                throw ErrorStates.NotFound("");
            }
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            org.FullName = model.FullName;
            org.ShortName = model.ShortName;
            org.DirectorFirstName = model.DirectorFirstName;
            org.DirectorLastName = model.DirectorLastName;
            org.DirectorMidName = model.DirectorMidName;
            org.DirectorPosition = model.DirectorPosition;
            org.PhoneNumber = model.PhoneNumber;
            org.AddressHomeNo = model.AddressHomeNo;
            org.AddressStreet = model.AddressStreet;
            org.AddressProvince = model.AddressProvince;
            org.AddressDistrict = model.AddressDistrict;
            org.PostIndex = model.PostIndex;
            org.Department = model.Department;
            org.DirectorMail = model.DirectorMail;
            org.OrgMail = model.OrgMail;
            org.WebSite = model.WebSite;
            org.OrgType = model.OrgType;
            org.Fax = model.Fax;
            org.OrgCategory = model.OrgCategory;
            org.IsActive = model.IsActive;
            org.IsIct = model.IsIct;
            org.IsMonitoring = model.IsMonitoring;

            _organization.Update(org);

        }
        public void DeleteOrg(OrgCommand model)
        {
            var org = _organization.Find(r => r.Id == model.Id).FirstOrDefault();
            if (org == null)
            {
                throw ErrorStates.NotFound("");
            }
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER))
                throw ErrorStates.NotAllowed("permission");
            _organization.Remove(model.Id);
        }
    }
}
