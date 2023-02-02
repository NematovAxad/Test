using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrProjectAutomatedServicesCommand;
using UserHandler.Results.ReestrProjectAutomatedServicesResult;

namespace UserHandler.Handlers.ReestrProjectAutomatedServicesHandler
{
    public class AutomaticServicesCommandHandler : IRequestHandler<AutomaticServicesCommand, AutomaticServicesCommandResult>
    {
        public Task<AutomaticServicesCommandResult> Handle(AutomaticServicesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
