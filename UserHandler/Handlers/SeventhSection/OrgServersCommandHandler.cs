using Domain.Models.FirstSection;
using Domain.Models.SeventhSection;
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
using UserHandler.Commands.SeventhSection;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Handlers.SeventhSection
{
    public class OrgServersCommandHandler : IRequestHandler<OrgServersCommand, OrgServersCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<OrganizationServers, int> _orgServers;

        public OrgServersCommandHandler(IRepository<Organizations, int> organization, IRepository<OrganizationServers, int> orgServers)
        {
            _organization = organization;
            _orgServers = orgServers;
        }
        public async Task<OrgServersCommandResult> Handle(OrgServersCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgServersCommandResult() { IsSuccess = true };
        }
        public void Add(OrgServersCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            OrganizationServers addModel = new OrganizationServers()
            {
                OrganizationId = model.OrganizationId,
                ServerLocation = model.ServerLocation,
                ServerType = model.ServerType,
                ServerBrand = model.ServerBrand,
                ServerConfig = model.ServerConfig,
                ServerAutomaticTasks = model.ServerAutomaticTasks,
                NumberOfServers = model.NumberOfServers,
                LastUpdate = DateTime.Now,
                userPinfl = model.UserPinfl
            };
            _orgServers.Add(addModel);
        }
        public void Update(OrgServersCommand model)
        {
            var orgServers = _orgServers.Find(s => s.Id == model.Id).FirstOrDefault();
            if (orgServers == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organization.Find(o => o.Id == orgServers.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            orgServers.OrganizationId = model.OrganizationId;
            orgServers.ServerLocation = model.ServerLocation;
            orgServers.ServerType = model.ServerType;
            orgServers.ServerBrand = model.ServerBrand;
            orgServers.ServerConfig = model.ServerConfig;
            orgServers.ServerAutomaticTasks = model.ServerAutomaticTasks;
            orgServers.NumberOfServers = model.NumberOfServers;
            orgServers.userPinfl = model.UserPinfl;
            orgServers.LastUpdate = DateTime.Now;

            _orgServers.Update(orgServers);
        }
        public void Delete(OrgServersCommand model)
        {
            var orgServers = _orgServers.Find(s => s.Id == model.Id).FirstOrDefault();
            if (orgServers == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            _orgServers.Remove(orgServers);
        }
    }
}
