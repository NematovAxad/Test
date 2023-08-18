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

                AllComputers = (model.CentralAllComputers + model.TerritorialAllComputers + model.SubordinateAllComputers + model.DevicionsAllComputers),
            CentralAllComputers = model.CentralAllComputers,
            TerritorialAllComputers = model.TerritorialAllComputers,
            SubordinateAllComputers = model.SubordinateAllComputers,
            DevicionsAllComputers = model.DevicionsAllComputers,

            WorkingComputers = (model.CentralWorkingComputers + model.TerritorialWorkingComputers + model.SubordinateWorkingComputers + model.DevicionsWorkingComputers),
            CentralWorkingComputers = model.CentralWorkingComputers,
            TerritorialWorkingComputers = model.TerritorialWorkingComputers,
            SubordinateWorkingComputers = model.SubordinateWorkingComputers,
            DevicionsWorkingComputers = model.DevicionsWorkingComputers,

            ConnectedLocalSet = (model.CentralConnectedLocalSet + model.TerritorialConnectedLocalSet + model.SubordinateConnectedLocalSet + model.DevicionsConnectedLocalSet),
            CentralConnectedLocalSet = model.CentralConnectedLocalSet,
            TerritorialConnectedLocalSet = model.TerritorialConnectedLocalSet,
            SubordinateConnectedLocalSet = model.SubordinateConnectedLocalSet,
            DevicionsConnectedLocalSet = model.DevicionsConnectedLocalSet,

            ConnectedNetwork = (model.CentralConnectedNetwork + model.TerritorialConnectedNetwork + model.SubordinateConnectedNetwork + model.DevicionsConnectedNetwork),
            CentralConnectedNetwork = model.CentralConnectedNetwork,
            TerritorialConnectedNetwork = model.TerritorialConnectedNetwork,
            SubordinateConnectedNetwork = model.SubordinateConnectedNetwork,
            DevicionsConnectedNetwork = model.DevicionsConnectedNetwork,

            ConnectedCorporateNetwork = (model.CentralConnectedCorporateNetwork + model.TerritorialConnectedCorporateNetwork + model.SubordinateConnectedCorporateNetwork + model.DevicionsConnectedCorporateNetwork),
            CentralConnectedCorporateNetwork = model.CentralConnectedCorporateNetwork,
            TerritorialConnectedCorporateNetwork = model.TerritorialConnectedCorporateNetwork,
            SubordinateConnectedCorporateNetwork = model.SubordinateConnectedCorporateNetwork,
            DevicionsConnectedCorporateNetwork = model.DevicionsConnectedCorporateNetwork,

            ConnectedExat = (model.CentralConnectedExat + model.TerritorialConnectedExat + model.SubordinateConnectedExat + model.DevicionsConnectedExat),
            CentralConnectedExat = model.CentralConnectedExat,
            TerritorialConnectedExat = model.TerritorialConnectedExat,
            SubordinateConnectedExat = model.SubordinateConnectedExat,
            DevicionsConnectedExat = model.DevicionsConnectedExat,

            ConnectedEijro = (model.CentralConnectedEijro + model.TerritorialConnectedEijro + model.SubordinateConnectedEijro + model.DevicionsConnectedEijro),
            CentralConnectedEijro = model.CentralConnectedEijro,
            TerritorialConnectedEijro = model.TerritorialConnectedEijro,
            SubordinateConnectedEijro = model.SubordinateConnectedEijro,
            DevicionsConnectedEijro = model.DevicionsConnectedEijro,

            ConnectedProjectGov = (model.CentralConnectedProjectGov + model.TerritorialConnectedProjectGov + model.SubordinateConnectedProjectGov + model.DevicionsConnectedProjectGov),
            CentralConnectedProjectGov = model.CentralConnectedProjectGov,
            TerritorialConnectedProjectGov = model.TerritorialConnectedProjectGov,
            SubordinateConnectedProjectGov = model.SubordinateConnectedProjectGov,
            DevicionsConnectedProjectGov = model.DevicionsConnectedProjectGov,

            ConnectedProjectAppeal = (model.CentralConnectedProjectAppeal + model.TerritorialConnectedProjectAppeal + model.SubordinateConnectedProjectAppeal + model.DevicionsConnectedProjectAppeal),
            CentralConnectedProjectAppeal = model.CentralConnectedProjectAppeal,
            TerritorialConnectedProjectAppeal = model.TerritorialConnectedProjectAppeal,
            SubordinateConnectedProjectAppeal = model.SubordinateConnectedProjectAppeal,
            DevicionsConnectedProjectAppeal = model.DevicionsConnectedProjectAppeal,

            ConnectedProjectResolution = (model.CentralConnectedProjectResolution + model.TerritorialConnectedProjectResolution + model.SubordinateConnectedProjectResolution + model.DevicionsConnectedProjectResolution),
            CentralConnectedProjectResolution = model.CentralConnectedProjectResolution,
            TerritorialConnectedProjectResolution = model.TerritorialConnectedProjectResolution,
            SubordinateConnectedProjectResolution = model.SubordinateConnectedProjectResolution,
            DevicionsConnectedProjectResolution = model.DevicionsConnectedProjectResolution,

            ConnectedProjectMyWork = (model.CentralConnectedProjectMyWork + model.TerritorialConnectedProjectMyWork + model.SubordinateConnectedProjectMyWork + model.DevicionsConnectedProjectMyWork),
            CentralConnectedProjectMyWork = model.CentralConnectedProjectMyWork,
            TerritorialConnectedProjectMyWork = model.TerritorialConnectedProjectMyWork,
            SubordinateConnectedProjectMyWork = model.SubordinateConnectedProjectMyWork,
            DevicionsConnectedProjectMyWork = model.DevicionsConnectedProjectMyWork,

            UserPinfl = model.UserPinfl,
            LastUpdate = DateTime.Now
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

            orgComputers.AllComputers = (model.CentralAllComputers + model.TerritorialAllComputers + model.SubordinateAllComputers + model.DevicionsAllComputers);
            orgComputers.CentralAllComputers = model.CentralAllComputers;
            orgComputers.TerritorialAllComputers = model.TerritorialAllComputers;
            orgComputers.SubordinateAllComputers = model.SubordinateAllComputers;
            orgComputers.DevicionsAllComputers = model.DevicionsAllComputers;

            orgComputers.WorkingComputers = (model.CentralWorkingComputers + model.TerritorialWorkingComputers + model.SubordinateWorkingComputers + model.DevicionsWorkingComputers);
            orgComputers.CentralWorkingComputers = model.CentralWorkingComputers;
            orgComputers.TerritorialWorkingComputers = model.TerritorialWorkingComputers;
            orgComputers.SubordinateWorkingComputers = model.SubordinateWorkingComputers;
            orgComputers.DevicionsWorkingComputers = model.DevicionsWorkingComputers;

            orgComputers.ConnectedLocalSet = (model.CentralConnectedLocalSet + model.TerritorialConnectedLocalSet + model.SubordinateConnectedLocalSet + model.DevicionsConnectedLocalSet);
            orgComputers.CentralConnectedLocalSet = model.CentralConnectedLocalSet;
            orgComputers.TerritorialConnectedLocalSet = model.TerritorialConnectedLocalSet;
            orgComputers.SubordinateConnectedLocalSet = model.SubordinateConnectedLocalSet;
            orgComputers.DevicionsConnectedLocalSet = model.DevicionsConnectedLocalSet;

            orgComputers.ConnectedNetwork = (model.CentralConnectedNetwork + model.TerritorialConnectedNetwork + model.SubordinateConnectedNetwork + model.DevicionsConnectedNetwork);
            orgComputers.CentralConnectedNetwork = model.CentralConnectedNetwork;
            orgComputers.TerritorialConnectedNetwork = model.TerritorialConnectedNetwork;
            orgComputers.SubordinateConnectedNetwork = model.SubordinateConnectedNetwork;
            orgComputers.DevicionsConnectedNetwork = model.DevicionsConnectedNetwork;

            orgComputers.ConnectedCorporateNetwork = (model.CentralConnectedCorporateNetwork + model.TerritorialConnectedCorporateNetwork + model.SubordinateConnectedCorporateNetwork + model.DevicionsConnectedCorporateNetwork);
            orgComputers.CentralConnectedCorporateNetwork = model.CentralConnectedCorporateNetwork;
            orgComputers.TerritorialConnectedCorporateNetwork = model.TerritorialConnectedCorporateNetwork;
            orgComputers.SubordinateConnectedCorporateNetwork = model.SubordinateConnectedCorporateNetwork;
            orgComputers.DevicionsConnectedCorporateNetwork = model.DevicionsConnectedCorporateNetwork;

            orgComputers.ConnectedExat = (model.CentralConnectedExat + model.TerritorialConnectedExat + model.SubordinateConnectedExat + model.DevicionsConnectedExat);
            orgComputers.CentralConnectedExat = model.CentralConnectedExat;
            orgComputers.TerritorialConnectedExat = model.TerritorialConnectedExat;
            orgComputers.SubordinateConnectedExat = model.SubordinateConnectedExat;
            orgComputers.DevicionsConnectedExat = model.DevicionsConnectedExat;

            orgComputers.ConnectedEijro = (model.CentralConnectedEijro + model.TerritorialConnectedEijro + model.SubordinateConnectedEijro + model.DevicionsConnectedEijro);
            orgComputers.CentralConnectedEijro = model.CentralConnectedEijro;
            orgComputers.TerritorialConnectedEijro = model.TerritorialConnectedEijro;
            orgComputers.SubordinateConnectedEijro = model.SubordinateConnectedEijro;
            orgComputers.DevicionsConnectedEijro = model.DevicionsConnectedEijro;

            orgComputers.ConnectedProjectGov = (model.CentralConnectedProjectGov + model.TerritorialConnectedProjectGov + model.SubordinateConnectedProjectGov + model.DevicionsConnectedProjectGov);
            orgComputers.CentralConnectedProjectGov = model.CentralConnectedProjectGov;
            orgComputers.TerritorialConnectedProjectGov = model.TerritorialConnectedProjectGov;
            orgComputers.SubordinateConnectedProjectGov = model.SubordinateConnectedProjectGov;
            orgComputers.DevicionsConnectedProjectGov = model.DevicionsConnectedProjectGov;

            orgComputers.ConnectedProjectAppeal = (model.CentralConnectedProjectAppeal + model.TerritorialConnectedProjectAppeal + model.SubordinateConnectedProjectAppeal + model.DevicionsConnectedProjectAppeal);
            orgComputers.CentralConnectedProjectAppeal = model.CentralConnectedProjectAppeal;
            orgComputers.TerritorialConnectedProjectAppeal = model.TerritorialConnectedProjectAppeal;
            orgComputers.SubordinateConnectedProjectAppeal = model.SubordinateConnectedProjectAppeal;
            orgComputers.DevicionsConnectedProjectAppeal = model.DevicionsConnectedProjectAppeal;

            orgComputers.ConnectedProjectResolution = (model.CentralConnectedProjectResolution + model.TerritorialConnectedProjectResolution + model.SubordinateConnectedProjectResolution + model.DevicionsConnectedProjectResolution);
            orgComputers.CentralConnectedProjectResolution = model.CentralConnectedProjectResolution;
            orgComputers.TerritorialConnectedProjectResolution = model.TerritorialConnectedProjectResolution;
            orgComputers.SubordinateConnectedProjectResolution = model.SubordinateConnectedProjectResolution;
            orgComputers.DevicionsConnectedProjectResolution = model.DevicionsConnectedProjectResolution;

            orgComputers.ConnectedProjectMyWork = (model.CentralConnectedProjectMyWork + model.TerritorialConnectedProjectMyWork + model.SubordinateConnectedProjectMyWork + model.DevicionsConnectedProjectMyWork);
            orgComputers.CentralConnectedProjectMyWork = model.CentralConnectedProjectMyWork;
            orgComputers.TerritorialConnectedProjectMyWork = model.TerritorialConnectedProjectMyWork;
            orgComputers.SubordinateConnectedProjectMyWork = model.SubordinateConnectedProjectMyWork;
            orgComputers.DevicionsConnectedProjectMyWork = model.DevicionsConnectedProjectMyWork;

            orgComputers.UserPinfl = model.UserPinfl;
            orgComputers.LastUpdate = DateTime.Now;

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
