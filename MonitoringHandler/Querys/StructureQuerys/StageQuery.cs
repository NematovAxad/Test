using Domain;
using MediatR;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringHandler.Querys.StructureQuerys
{
    public class StageQuery:IRequest<StageQueryResult>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public StageStatus Status { get; set; }
    }
}
