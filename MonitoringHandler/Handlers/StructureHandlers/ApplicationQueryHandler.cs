using Domain.MonitoringModels.Models;
using JohaRepository;
using MediatR;
using MonitoringHandler.Querys.StructureQuerys;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringHandler.Handlers.StructureHandlers
{
    public class ApplicationQueryHandler : IRequestHandler<ApplicationQuery, ApplicationQueryResult>
    {
        private readonly IRepository<Application, int> _application;

        public ApplicationQueryHandler(IRepository<Application, int> application)
        {
            _application = application;
        }

        public async Task<ApplicationQueryResult> Handle(ApplicationQuery request, CancellationToken cancellationToken)
        {
            var application = _application.GetAll();
            if (request.Id != 0)
            {
                application = application.Where(n => n.Id == request.Id);
            }
            ApplicationQueryResult result = new ApplicationQueryResult();
            result.Count = application.Count();
            result.Data = application.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
