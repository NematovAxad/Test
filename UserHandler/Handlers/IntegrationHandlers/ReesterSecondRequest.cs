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
    public class ReesterSecondRequest : IRequestHandler<SecondRequestQuery, SecondRequestQueryResult>
    {
        IReesterService _reesterService;

        public ReesterSecondRequest(IReesterService reesterService)
        {
            _reesterService = reesterService;
        }

        public Task<SecondRequestQueryResult> Handle(SecondRequestQuery request, CancellationToken cancellationToken)
        {
            return _reesterService.SecondRequest(request);
        }
    }
}
