using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using Domain.Models.SecondSection;
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
                //case Domain.Enums.EventType.Update: Update(request); break;
                //case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgMessengersCommandResult() { IsSuccess = true };
        }
        public void Add(OrgMessengersCommand model)
        {
            var org = _organization.Find(o => o.Id == model.Messenger.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.Messenger.OrganizationId.ToString());
            var field = _field.Find(f => f.Id == model.Messenger.FieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound(model.Messenger.FieldId.ToString());
            var deadline = _deadline.Find(d => d.Id == model.Messenger.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(model.Messenger.DeadlineId.ToString());
            var messenger = _organizationMessengers.Find(s => s.OrganizationId == model.Messenger.OrganizationId && s.DeadlineId == model.Messenger.DeadlineId && s.FieldId == model.Messenger.FieldId && s.MessengerLink == model.Messenger.MessengerLink).FirstOrDefault();
            if (messenger != null)
                throw ErrorStates.NotAllowed(model.Messenger.MessengerLink);

            
        }
    }
}
