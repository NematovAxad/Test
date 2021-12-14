using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrganizationAppsCommandHandler : IRequestHandler<OrganizationAppCommand, OrganizationAppsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationApps, int> _organizationApps;

        public OrganizationAppsCommandHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationApps, int> organizationApps)
        {
            _organizationApps = organizationApps;
            _organizations = organizations;
        }

        public async Task<OrganizationAppsCommandResult> Handle(OrganizationAppCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                //case Domain.Enums.EventType.Update: Update(request); break;
                //case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrganizationAppsCommandResult() { IsSuccess = true };
        }
        public void Add(OrganizationAppCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
        }
    }
}
