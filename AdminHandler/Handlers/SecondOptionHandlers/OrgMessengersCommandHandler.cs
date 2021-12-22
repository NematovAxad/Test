using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using Domain.Models.SecondSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
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

        public Task<OrgMessengersCommandResult> Handle(OrgMessengersCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
