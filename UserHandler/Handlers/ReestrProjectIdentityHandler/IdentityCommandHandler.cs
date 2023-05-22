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
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain;

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
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new IdentitiyCommandResult() { Id = id, Success = true };
        }

        public int Add(IdentityCommand model)
        {
            int id = 0;

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            



            var projectIdentity = _projectidentity.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectIdentity == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _identities.Find(p => p.ParentId == model.ParentId && p.IdentitiyType == model.IdentitiyType).FirstOrDefault();
            if (identity != null)
                throw ErrorStates.NotAllowed(model.IdentityUrl.ToString());


           


            if ((model.UserOrgId == projectIdentity.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                ProjectIdentities addModel = new ProjectIdentities();
                addModel.ParentId = model.ParentId;
                addModel.IdentitiyType = model.IdentitiyType;
                addModel.IdentityUrl = model.IdentityUrl;
                addModel.FilePath = model.FilePath;

                _identities.Add(addModel);
                id = addModel.Id;
            }
           



            

            return id;
        }

        public int Update(IdentityCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            


            var projectIdentity = _projectidentity.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectIdentity == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _identities.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotAllowed(model.IdentityUrl.ToString());


            if ((model.UserOrgId == projectIdentity.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                identity.IdentitiyType = model.IdentitiyType;
                identity.IdentityUrl = model.IdentityUrl;
                if (!String.IsNullOrEmpty(model.FilePath))
                    identity.FilePath = model.FilePath;
            }
           
            _identities.Update(identity);

            return identity.Id;
        }
        public int Delete(IdentityCommand model)
        {
            var identity = _identities.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _identities.Remove(identity);

            return identity.Id;
        }
    }
}
