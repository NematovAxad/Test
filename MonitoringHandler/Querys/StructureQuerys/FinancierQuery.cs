using MediatR;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringHandler.Querys.StructureQuerys
{
    public class FinancierQuery:IRequest<FinancierQueryResult>
    {
        public int Id { get; set; }
    }
}
