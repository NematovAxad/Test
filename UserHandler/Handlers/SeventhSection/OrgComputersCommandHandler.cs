using Domain.Models;
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
    public class OrgComputersCommandHandler : IRequestHandler<OrgComputersCommand, OrgComputersCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<OrganizationComputers, int> _orgComputers;

        public OrgComputersCommandHandler(IRepository<Organizations, int> organization, IRepository<OrganizationComputers, int> orgComputers)
        {
            _organization = organization;
            _orgComputers = orgComputers;
        }
        public async Task<OrgComputersCommandResult> Handle(OrgComputersCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgComputersCommandResult() { IsSuccess = true };
        }
        public void Add(OrgComputersCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var orgComputers = _orgComputers.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (orgComputers != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            


            OrganizationComputers addModel = new OrganizationComputers()
            {
                OrganizationId = model.OrganizationId,
                AllComputers = model.AllComputers,
                CentralAllComputers = model.CentralAllComputers,
                TerritorialAllComputers = model.TerritorialAllComputers,
                SubordinateAllComputers = model.SubordinateAllComputers,
                WorkingComputers = model.WorkingComputers,
                CentralWorkingComputers = model.CentralWorkingComputers,
                TerritorialWorkingComputers = model.TerritorialWorkingComputers,
                SubordinateWorkingComputers = model.SubordinateWorkingComputers,
                ConnectedLocalSet = model.ConnectedLocalSet,
                CentralConnectedLocalSet = model.CentralConnectedLocalSet,
                TerritorialConnectedLocalSet = model.TerritorialConnectedLocalSet,
                SubordinateConnectedLocalSet = model.SubordinateConnectedLocalSet,
                ConnectedNetwork = model.ConnectedNetwork,
                CentralConnectedNetwork = model.CentralConnectedNetwork,
                TerritorialConnectedNetwork = model.TerritorialConnectedNetwork,
                SubordinateConnectedNetwork = model.SubordinateConnectedNetwork,
                ConnectedCorporateNetwork = model.ConnectedCorporateNetwork,
                CentralConnectedCorporateNetwork = model.CentralConnectedCorporateNetwork,
                TerritorialConnectedCorporateNetwork = model.TerritorialConnectedCorporateNetwork,
                SubordinateConnectedCorporateNetwork = model.SubordinateConnectedCorporateNetwork,
                ConnectedExat = model.ConnectedExat,
                CentralConnectedExat = model.CentralConnectedExat,
                TerritorialConnectedExat = model.TerritorialConnectedExat,
                SubordinateConnectedExat = model.SubordinateConnectedExat,
                ConnectedEijro = model.ConnectedEijro,
                CentralConnectedEijro = model.CentralConnectedEijro,
                TerritorialConnectedEijro = model.TerritorialConnectedEijro,
                SubordinateConnectedEijro = model.SubordinateConnectedEijro,
                ConnectedProjectGov = model.ConnectedProjectGov,
                CentralConnectedProjectGov = model.CentralConnectedProjectGov,
                TerritorialConnectedProjectGov = model.TerritorialConnectedProjectGov,
                SubordinateConnectedProjectGov = model.SubordinateConnectedProjectGov,
                ConnectedProjectMyWork = model.ConnectedProjectMyWork,
                CentralConnectedProjectMyWork = model.CentralConnectedProjectMyWork,
                TerritorialConnectedProjectMyWork = model.TerritorialConnectedProjectMyWork,
                SubordinateConnectedProjectMyWork = model.SubordinateConnectedProjectMyWork
            };
            _orgComputers.Add(addModel);
        }
        public void Update(OrgComputersCommand model)
        {
            
            var orgComputers = _orgComputers.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgComputers == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            var org = _organization.Find(o => o.Id == orgComputers.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            orgComputers.AllComputers = model.AllComputers;
            orgComputers.CentralAllComputers = model.CentralAllComputers;
            orgComputers.TerritorialAllComputers = model.TerritorialAllComputers;
            orgComputers.SubordinateAllComputers = model.SubordinateAllComputers;
            orgComputers.WorkingComputers = model.WorkingComputers;
            orgComputers.CentralWorkingComputers = model.CentralWorkingComputers;
            orgComputers.TerritorialWorkingComputers = model.TerritorialWorkingComputers;
            orgComputers.SubordinateWorkingComputers = model.SubordinateWorkingComputers;
            orgComputers.ConnectedLocalSet = model.ConnectedLocalSet;
            orgComputers.CentralConnectedLocalSet = model.CentralConnectedLocalSet;
            orgComputers.TerritorialConnectedLocalSet = model.TerritorialConnectedLocalSet;
            orgComputers.SubordinateConnectedLocalSet = model.SubordinateConnectedLocalSet;
            orgComputers.ConnectedNetwork = model.ConnectedNetwork;
            orgComputers.CentralConnectedNetwork = model.CentralConnectedNetwork;
            orgComputers.TerritorialConnectedNetwork = model.TerritorialConnectedNetwork;
            orgComputers.SubordinateConnectedNetwork = model.SubordinateConnectedNetwork;
            orgComputers.ConnectedCorporateNetwork = model.ConnectedCorporateNetwork;
            orgComputers.CentralConnectedCorporateNetwork = model.CentralConnectedCorporateNetwork;
            orgComputers.TerritorialConnectedCorporateNetwork = model.TerritorialConnectedCorporateNetwork;
            orgComputers.SubordinateConnectedCorporateNetwork = model.SubordinateConnectedCorporateNetwork;
            orgComputers.ConnectedExat = model.ConnectedExat;
            orgComputers.CentralConnectedExat = model.CentralConnectedExat;
            orgComputers.TerritorialConnectedExat = model.TerritorialConnectedExat;
            orgComputers.SubordinateConnectedExat = model.SubordinateConnectedExat;
            orgComputers.ConnectedEijro = model.ConnectedEijro;
            orgComputers.CentralConnectedEijro = model.CentralConnectedEijro;
            orgComputers.TerritorialConnectedEijro = model.TerritorialConnectedEijro;
            orgComputers.SubordinateConnectedEijro = model.SubordinateConnectedEijro;
            orgComputers.ConnectedProjectGov = model.ConnectedProjectGov;
            orgComputers.CentralConnectedProjectGov = model.CentralConnectedProjectGov;
            orgComputers.TerritorialConnectedProjectGov = model.TerritorialConnectedProjectGov;
            orgComputers.SubordinateConnectedProjectGov = model.SubordinateConnectedProjectGov;
            orgComputers.ConnectedProjectMyWork = model.ConnectedProjectMyWork;
            orgComputers.CentralConnectedProjectMyWork = model.CentralConnectedProjectMyWork;
            orgComputers.TerritorialConnectedProjectMyWork = model.TerritorialConnectedProjectMyWork;
            orgComputers.SubordinateConnectedProjectMyWork = model.SubordinateConnectedProjectMyWork;

            _orgComputers.Update(orgComputers);
        }
        public void Delete(OrgComputersCommand model)
        {

            var orgComputers = _orgComputers.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgComputers == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgComputers.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            _orgComputers.Remove(orgComputers);
        }
    }
}
