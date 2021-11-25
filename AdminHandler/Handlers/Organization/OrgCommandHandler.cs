using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Organization
{
    public class OrgCommandHandler : IRequestHandler<OrgCommand, OrgCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;

        public OrgCommandHandler(IRepository<Organizations, int> organization)
        {
            _organization = organization;
        }

        public async Task<OrgCommandResult> Handle(OrgCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: AddOrg(request); break;
                case Domain.Enums.EventType.Update: UpdateOrg(request); break;
                case Domain.Enums.EventType.Delete: DeleteOrg(request); break;
            }
            return new OrgCommandResult() { IsSuccess = true };
        }
        public void AddOrg(OrgCommand model)
        {
            var org = _organization.Find(o => o.ShortName == model.Organization.ShortName).FirstOrDefault();

            if (org != null)
            {
                throw ErrorStates.NotAllowed(model.Organization.ShortName);
            }
            
            _organization.Add(model.Organization);
        }
        public void UpdateOrg(OrgCommand model)
        {
            if (model.Organization.Id==0)
            {
                throw ErrorStates.NotFound("");
            }
            var org = _organization.Find(r => r.Id == model.Organization.Id).FirstOrDefault();
            if (org == null)
            {
                throw ErrorStates.NotFound("");
            }
            _organization.Update(model.Organization);

        }
        public void DeleteOrg(OrgCommand model)
        {
            if (model.Organization.Id==0)
            {
                throw ErrorStates.NotFound("");
            }
            _organization.Remove(model.Organization.Id);
        }
    }
}
