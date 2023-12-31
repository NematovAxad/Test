﻿using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain;
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
    public class OrgHeadCommandHandler : IRequestHandler<OrgHeadCommand, OrgHeadCommandResult>
    {
        private readonly IRepository<ReplacerOrgHead, int> _replacerHead;
        private readonly IRepository<Organizations, int> _organizations;

        public OrgHeadCommandHandler(IRepository<ReplacerOrgHead, int> replacerHead, IRepository<Organizations, int> organizations)
        {
            _replacerHead = replacerHead;
            _organizations = organizations;
        }

        public async Task<OrgHeadCommandResult> Handle(OrgHeadCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgHeadCommandResult() { IsSuccess = true };
        }

        public void Add(OrgHeadCommand model)
        {
            var head = _replacerHead.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (head != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            
            ReplacerOrgHead addModel = new ReplacerOrgHead()
            {
                OrganizationId = model.OrganizationId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MidName = model.MidName,
                Position = model.Position,
                Phone = model.Phone,
                Email = model.Email,
                Fax = model.Fax,
                FilePath = model.FilePath,
                PhotoPath = model.PhotoPath
            };

            if (!String.IsNullOrEmpty(model.UserPinfl))
                addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _replacerHead.Add(addModel);
        }
        public void Update(OrgHeadCommand model)
        {
            var head = _replacerHead.Find(h => h.Id == model.Id).FirstOrDefault();
            if (head == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organizations.Find(o => o.Id == head.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
           
            head.FilePath = model.FilePath;
            head.FirstName = model.FirstName;
            head.LastName = model.LastName;
            head.MidName = model.MidName;
            head.Position = model.Position;
            head.Phone = model.Phone;
            head.Email = model.Email;
            head.Fax = model.Fax;
            head.PhotoPath = model.PhotoPath;

            if (!String.IsNullOrEmpty(model.UserPinfl))
                head.UserPinfl = model.UserPinfl;
            head.LastUpdate = DateTime.Now;

            _replacerHead.Update(head);
        }
        public void Delete(OrgHeadCommand model)
        {
            var head = _replacerHead.Find(h => h.Id == model.Id).FirstOrDefault();
            if (head == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organizations.Find(o => o.Id == head.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _replacerHead.Remove(model.Id);
        }
    }
}
