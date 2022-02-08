using MediatR;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringHandler.Querys.StructureQuerys
{
    public class PerformencerQuery:IRequest<PerformencerQueryResult>
    {
        public int Id { get; set; }
    }
}
