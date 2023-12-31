﻿using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain;
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
    public class ContentManagerCommandHandler : IRequestHandler<ContentManagerCommand, ContentManagerCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<ContentManager, int> _contentManager;

        public ContentManagerCommandHandler(IRepository<Organizations, int> organizations, IRepository<ContentManager, int> contentManager)
        {
            _organizations = organizations;
            _contentManager = contentManager;
        }

        public async Task<ContentManagerCommandResult> Handle(ContentManagerCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ContentManagerCommandResult() { IsSuccess = true };
        }
        public void Add(ContentManagerCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var manager = _contentManager.Find(m => m.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (manager != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            

            ContentManager addModel = new ContentManager()
            {
                OrganizationId = model.OrganizationId,
                FullName = model.FullName,
                Position = model.Position,
                Phone = model.Phone,
                FilePath = model.FilePath
            };
             
            if(!String.IsNullOrEmpty(model.UserPinfl))
                addModel.UserPinfl = model.UserPinfl;

            addModel.LastUpdate = DateTime.Now;
            
            _contentManager.Add(addModel);
        }
        public void Update(ContentManagerCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var manager = _contentManager.Find(m => m.Id == model.Id).FirstOrDefault();
            if (manager == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                throw ErrorStates.NotAllowed("permission");

            manager.FilePath = model.FilePath;
            manager.FullName = model.FullName;
            manager.Position = model.Position;
            manager.Phone = model.Phone;

            if (!String.IsNullOrEmpty(model.UserPinfl))
                manager.UserPinfl = model.UserPinfl;

            manager.LastUpdate = DateTime.Now;

            _contentManager.Update(manager);
        }
        public void Delete(ContentManagerCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var manager = _contentManager.Find(m => m.Id == model.Id).FirstOrDefault();
            if (manager == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                throw ErrorStates.NotAllowed("permission");
            _contentManager.Remove(model.Id);
        }
    }
}
