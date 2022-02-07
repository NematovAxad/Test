using MediatR;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringHandler.Querys.StructureQuerys
{
    public class NormativeQuery:IRequest<NormativeQueryResult>
    {
        public int Id { get; set; }
    }
}
