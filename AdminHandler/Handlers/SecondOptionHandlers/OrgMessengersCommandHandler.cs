using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using Domain.Models.SecondSection;
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
    public class OrgMessengersCommandHandler : IRequestHandler<OrgMessengersCommand, OrgMessengersCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationMessengers, int> _organizationMessengers;

        public OrgMessengersCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationMessengers, int> organizationMessengers)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _organizationMessengers = organizationMessengers;
        }

        public async Task<OrgMessengersCommandResult> Handle(OrgMessengersCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgMessengersCommandResult() { IsSuccess = true };
        }
        public void Add(OrgMessengersCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var messenger = _organizationMessengers.Find(s => s.OrganizationId == model.OrganizationId && s.MessengerLink == model.MessengerLink).FirstOrDefault();
            if (messenger != null)
                throw ErrorStates.NotAllowed(model.MessengerLink);

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.Id) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            OrganizationMessengers addModel = new OrganizationMessengers()
            {
                OrganizationId = model.OrganizationId,
                MessengerLink = model.MessengerLink,
                ReasonNotFilling = model.ReasonNotFilling
            };

            _organizationMessengers.Add(addModel);
        }
        public void Update(OrgMessengersCommand model)
        {
            var messenger = _organizationMessengers.Find(m => m.Id == model.Id).FirstOrDefault();
            if (messenger == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            messenger.MessengerLink = model.MessengerLink;
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == messenger.OrganizationId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            _organizationMessengers.Update(messenger);
        }
        public void Delete(OrgMessengersCommand model)
        {
            var messenger = _organizationMessengers.Find(m => m.Id == model.Id).FirstOrDefault();
            if (messenger == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == messenger.OrganizationId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            _organizationMessengers.Remove(messenger);
        }
    }
}
