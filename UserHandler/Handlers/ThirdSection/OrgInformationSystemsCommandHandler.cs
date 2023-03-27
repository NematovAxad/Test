using Domain;
using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
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
using UserHandler.Commands.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class OrgInformationSystemsCommandHandler : IRequestHandler<OrgInformationSystemsCommand, OrgInformationSystemsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrgInformationSystems, int> _orgInfoSystem;

        public OrgInformationSystemsCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrgInformationSystems, int> orgInfoSystem)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgInfoSystem = orgInfoSystem;
        }
        public async Task<OrgInformationSystemsCommandResult> Handle(OrgInformationSystemsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgInformationSystemsCommandResult() { IsSuccess = true };
        }
        public void Add(OrgInformationSystemsCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var orgInfoSystems = _orgInfoSystem.Find(h => h.OrganizationId == model.OrganizationId && h.SystemId == model.SystemId).FirstOrDefault();
            if (orgInfoSystems != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            OrgInformationSystems addModel = new OrgInformationSystems()
            {
                OrganizationId = model.OrganizationId,
               
            };
            _orgInfoSystem.Add(addModel);
        }
        public void Update(OrgInformationSystemsCommand model)
        {
            var system = _orgInfoSystem.Find(h => h.Id == model.Id).FirstOrDefault();
            if (system == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == system.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);



            _orgInfoSystem.Update(system);
        }
        public void Delete(OrgInformationSystemsCommand model)
        {
            var service = _orgInfoSystem.Find(h => h.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == service.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);
            _orgInfoSystem.Remove(service);
        }
    }
}
