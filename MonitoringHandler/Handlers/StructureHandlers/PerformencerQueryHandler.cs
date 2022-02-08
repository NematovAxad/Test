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
    public class PerformencerQueryHandler : IRequestHandler<PerformencerQuery, PerformencerQueryResult>
    {
        private readonly IRepository<Performencer, int> _performencer;
        public PerformencerQueryHandler(IRepository<Performencer, int> performencer)
        {
            _performencer = performencer;
        }

        public async Task<PerformencerQueryResult> Handle(PerformencerQuery request, CancellationToken cancellationToken)
        {
            var p = _performencer.GetAll();
            if (request.Id != 0)
            {
                p = p.Where(n => n.Id == request.Id);
            }
            
            PerformencerQueryResult result = new PerformencerQueryResult();
            result.Count = p.Count();
            result.Data = p.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
