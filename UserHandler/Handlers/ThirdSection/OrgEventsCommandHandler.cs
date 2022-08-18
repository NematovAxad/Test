using Domain.Models;
using Domain.Models.FifthSection;
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
    public class OrgEventsCommandHandler : IRequestHandler<OrgEventsCommand, OrgEventsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationEvents, int> _orgEvents;

        public OrgEventsCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationEvents, int> orgEvents)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgEvents = orgEvents;
        }
        public async Task<OrgEventsCommandResult> Handle(OrgEventsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgEventsCommandResult() { IsSuccess = true };
        }
        public void Add(OrgEventsCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            OrganizationEvents addModel = new OrganizationEvents()
            {
                OrganizationId = model.OrganizationId,
                EventName = model.EventName,
                EventDate = model.EventDate
            };
            _orgEvents.Add(addModel);
        }
        public void Update(OrgEventsCommand model)
        {
            var orgEvents = _orgEvents.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgEvents == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgEvents.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            orgEvents.EventName = model.EventName;
            orgEvents.EventDate = model.EventDate;

            _orgEvents.Update(orgEvents);
        }
        public void Delete(OrgEventsCommand model)
        {
            var orgEvents = _orgEvents.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgEvents == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgEvents.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _orgEvents.Remove(orgEvents);
        }
    }
}
