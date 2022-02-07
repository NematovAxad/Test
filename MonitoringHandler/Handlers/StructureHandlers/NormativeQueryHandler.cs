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
    public class NormativeQueryHandler:IRequestHandler<NormativeQuery, NormativeQueryResult>
    {
        private readonly IRepository<NormativeLegalDocument, int> _normative;

        public NormativeQueryHandler(IRepository<NormativeLegalDocument, int> normative)
        {
            _normative = normative;
        }

        public async Task<NormativeQueryResult> Handle(NormativeQuery request, CancellationToken cancellationToken)
        {
            var normative = _normative.GetAll();
            if(request.Id != 0)
            {
                normative = normative.Where(n => n.Id == request.Id);
            }
            NormativeQueryResult result = new NormativeQueryResult();
            result.Count = normative.Count();
            result.Data = normative.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
