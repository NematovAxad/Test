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
    public class FinancierQueryHandler : IRequestHandler<FinancierQuery, FinancierQueryResult>
    {
        private readonly IRepository<Financier, int> _financier;

        public FinancierQueryHandler(IRepository<Financier, int> financier)
        {
            _financier = financier;
        }

        public async Task<FinancierQueryResult> Handle(FinancierQuery request, CancellationToken cancellationToken)
        {
            var financier = _financier.GetAll();
            if (request.Id != 0)
            {
                financier = financier.Where(n => n.Id == request.Id);
            }
            FinancierQueryResult result = new FinancierQueryResult();
            result.Count = financier.Count();
            result.Data = financier.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
