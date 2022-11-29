﻿using Domain.Models.SecondSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrProjectIdentityCommand;
using UserHandler.Results.ReestrProjectIdentityResult;
using UserHandler.Results.ReestrPassportResult;
using Domain.States;
using MainInfrastructures.Migrations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;
using UserHandler.Commands.ReestrPassportCommands;

namespace UserHandler.Handlers.ReestrProjectIdentityHandler
{
    public class IdentityCommandHandler : IRequestHandler<IdentityCommand, IdentitiyCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ProjectIdentities, int> _identities;
        private readonly IRepository<ReestrProjectIdentities, int> _projectidentity;

        public IdentityCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ProjectIdentities, int> identities, IRepository<ReestrProjectIdentities, int> projectidentity)
        {
            _organization = organization;
            _deadline = deadline;
            _identities = identities;
            _projectidentity = projectidentity;
        }

        public async Task<IdentitiyCommandResult> Handle(IdentityCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new IdentitiyCommandResult() { Success = true };
        }

        public void Add(IdentityCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());



            var projectIdentity = _projectidentity.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectIdentity == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _identities.Find(p => p.ParentId == model.ParentId && p.IdentityUrl == model.IdentityUrl).FirstOrDefault();
            if (identity != null)
                throw ErrorStates.NotAllowed(model.IdentityUrl.ToString());


            ProjectIdentities addModel = new ProjectIdentities();


            if (((model.UserOrgId == projectIdentity.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                addModel.ParentId = model.ParentId;
                addModel.IdentitiyType = model.IdentitiyType;
                addModel.IdentityUrl = model.IdentityUrl;
                addModel.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _identities.Add(addModel);
        }

        public void Update(IdentityCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            var projectIdentity = _projectidentity.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectIdentity == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _identities.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotAllowed(model.IdentityUrl.ToString());


            if (((model.UserOrgId == projectIdentity.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                identity.IdentitiyType = model.IdentitiyType;
                identity.IdentityUrl = model.IdentityUrl;
                if (!String.IsNullOrEmpty(model.FilePath))
                    identity.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _identities.Update(identity);
        }
        public void Delete(IdentityCommand model)
        {
            var identity = _identities.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _identities.Remove(identity);
        }
    }
}