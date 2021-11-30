﻿using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain.Models;
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
            Organizations addModel = new Organizations()
            {
                FullName = model.FullName,
                ShortName = model.ShortName,
                DirectorFirstName = model.DirectorFirstName,
                DirectorLastName = model.DirectorLastName,
                DirectorMidName = model.DirectorMidName,
                DirectorPosition = model.DirectorPosition,
                PhoneNumber = model.PhoneNumber,
                AddressHomeNo = model.AddressHomeNo,
                AddressStreet = model.AddressStreet,
                AddressProvince = model.AddressProvince,
                AddressDistrict = model.AddressDistrict,
                PostIndex = model.PostIndex,
                Department = model.Department,
                DirectorMail = model.DirectorMail,
                OrgMail = model.OrgMail,
                WebSite = model.WebSite,
                OrgType = model.OrgType,
                Fax = model.Fax,
                OrgCategory = model.OrgCategory
            };
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
            Organizations updateModel = new Organizations()
            {
                Id = model.Id,
                FullName = model.FullName,
                ShortName = model.ShortName,
                DirectorFirstName = model.DirectorFirstName,
                DirectorLastName = model.DirectorLastName,
                DirectorMidName = model.DirectorMidName,
                DirectorPosition = model.DirectorPosition,
                PhoneNumber = model.PhoneNumber,
                AddressHomeNo = model.AddressHomeNo,
                AddressStreet = model.AddressStreet,
                AddressProvince = model.AddressProvince,
                AddressDistrict = model.AddressDistrict,
                PostIndex = model.PostIndex,
                Department = model.Department,
                DirectorMail = model.DirectorMail,
                OrgMail = model.OrgMail,
                WebSite = model.WebSite,
                OrgType = model.OrgType,
                Fax = model.Fax,
                OrgCategory = model.OrgCategory
            };
            _organization.Update(updateModel);

        }
        public void DeleteOrg(OrgCommand model)
        {
            if (model.Id==0)
            {
                throw ErrorStates.NotFound("");
            }
            _organization.Remove(model.Id);
        }
    }
}
