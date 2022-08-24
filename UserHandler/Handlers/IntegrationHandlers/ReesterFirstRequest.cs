using Domain.ReesterModels;
using MainInfrastructures.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserHandler.Handlers.IntegrationHandlers
{
    public class ReesterFirstRequest : IRequestHandler<FirstRequestQuery, FirstRequestQueryResult>
    {
        IReesterService _reesterService;

        public ReesterFirstRequest(IReesterService reesterService)
        {
            _reesterService = reesterService;
        }

        public Task<FirstRequestQueryResult> Handle(FirstRequestQuery request, CancellationToken cancellationToken)
        {
            return _reesterService.FirstRequest(request);
        }
    }
}
