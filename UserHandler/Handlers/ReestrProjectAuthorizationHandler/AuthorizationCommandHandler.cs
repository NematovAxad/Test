using Domain.Models;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Results.ReestrProjectClassificationCommandResult;
using UserHandler.Commands.ReestrProjectAuthorizationCommand;
using UserHandler.Results.ReestrProjectAuthorizationResult;
using System.Threading.Tasks;
using System.Threading;
using Domain.States;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain;

namespace UserHandler.Handlers.ReestrProjectAuthorizationHandler
{
    public class AuthorizationCommandHandler : IRequestHandler<AuthorizationCommand, AuthorizationCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ProjectAuthorizations, int> _authorizations;
        private readonly IRepository<ReestrProjectAuthorizations, int> _projectAuthorization;

        public AuthorizationCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ProjectAuthorizations, int> authorizations, IRepository<ReestrProjectAuthorizations, int> projectAuthorization)
        {
            _organization = organization;
            _deadline = deadline;
            _authorizations = authorizations;
            _projectAuthorization = projectAuthorization;
        }

        public async Task<AuthorizationCommandResult> Handle(AuthorizationCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new AuthorizationCommandResult() {Id = id, Success = true };
        }

        public int Add(AuthorizationCommand model)
        {
            int id = 0;

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            var projectAuthorization = _projectAuthorization.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectAuthorization == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _authorizations.Find(p => p.ParentId == model.ParentId && p.AuthorizationUri == model.AuthorizationUri).FirstOrDefault();
            if (identity != null)
                throw ErrorStates.NotAllowed(model.AuthorizationUri.ToString());


            if ((model.UserOrgId == projectAuthorization.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                ProjectAuthorizations addModel = new ProjectAuthorizations();
                addModel.ParentId = model.ParentId;
                addModel.AuthorizationType = model.AuthorizationType;
                addModel.AuthorizationUri = model.AuthorizationUri;
                addModel.FilePath = model.FilePath;

                _authorizations.Add(addModel);
                id = addModel.Id;
            }

            return id;
        }

        public int Update(AuthorizationCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            


            var projectAuthorization = _projectAuthorization.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectAuthorization == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _authorizations.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotAllowed(model.Id.ToString());


            if ((model.UserOrgId == projectAuthorization.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                identity.AuthorizationType = model.AuthorizationType;
                identity.AuthorizationUri = model.AuthorizationUri;
                if (!String.IsNullOrEmpty(model.FilePath))
                    identity.FilePath = model.FilePath;
            }
           



            _authorizations.Update(identity);

            return identity.Id;
        }
        public int Delete(AuthorizationCommand model)
        {
            var identity = _authorizations.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _authorizations.Remove(identity);

            return identity.Id;
        }
    }
}
