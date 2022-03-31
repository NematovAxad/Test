using Domain.OpenDataModels;
using MainInfrastructures.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserHandler.Handlers.IntegrationHandlers
{
    public class OpenDataQueryHandler : IRequestHandler<OpenDataQuery, OpenDataQueryResult>
    {
        IOpenDataService _openDataService;

        public OpenDataQueryHandler(IOpenDataService openDataService)
        {
            _openDataService = openDataService;
        }

        public Task<OpenDataQueryResult> Handle(OpenDataQuery request, CancellationToken cancellationToken)
        {
            return _openDataService.OpenDataApi(request);
        }
    }
}
