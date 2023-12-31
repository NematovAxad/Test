﻿using MediatR;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringHandler.Querys.StructureQuerys
{
    public class ApplicationQuery:IRequest<ApplicationQueryResult>
    {
        public int Id { get; set; }
        public int NormativeId { get; set; }
    }
}
