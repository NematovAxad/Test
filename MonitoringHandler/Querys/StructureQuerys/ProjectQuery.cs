using Domain;
using Domain.Enums;
using MediatR;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringHandler.Querys.StructureQuerys
{
    public class ProjectQuery:IRequest<ProjectQueryResult>
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int PerformencerId { get; set; }
        public MonitoringProjectStatus Status { get; set; }
    }
}
