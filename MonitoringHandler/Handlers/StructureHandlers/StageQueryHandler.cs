using Domain.MonitoringModels.Models;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MonitoringHandler.Commands.StructureCommands;
using MonitoringHandler.Querys.StructureQuerys;
using MonitoringHandler.Results.StructureResults.CommandResults;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringHandler.Handlers.StructureHandlers
{
    public class StageQueryHandler : IRequestHandler<StageQuery, StageQueryResult>
    {
        private readonly IRepository<Stage, int> _stage;
        private readonly IRepository<Project, int> _project;

        public StageQueryHandler(IRepository<Stage, int> stage, IRepository<Project, int> project)
        {
            _stage = stage;
            _project = project;
        }
        public async Task<StageQueryResult> Handle(StageQuery request, CancellationToken cancellationToken)
        {
            var stage = _stage.GetAll().Include(mbox => mbox.Comments);
            if (request.Id != 0)
            {
                stage = stage.Where(n => n.Id == request.Id).Include(mbox => mbox.Comments);
            }
            if (request.ProjectId != 0)
            {
                stage = stage.Where(n => n.ProjectId == request.ProjectId).Include(mbox => mbox.Comments);
            }
            StageQueryResult result = new StageQueryResult();
            result.Count = stage.Count();
            result.Data = stage.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
