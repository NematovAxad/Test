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
    public class ProjectQueryHandler : IRequestHandler<ProjectQuery, ProjectQueryResult>
    {
        private readonly IRepository<Project, int> _project;

        public ProjectQueryHandler(IRepository<Project, int> project)
        {
            _project = project;
        }
        public async Task<ProjectQueryResult> Handle(ProjectQuery request, CancellationToken cancellationToken)
        {
            var project = _project.GetAll();
            if (request.Id != 0)
            {
                project = project.Where(n => n.Id == request.Id);
            }
            ProjectQueryResult result = new ProjectQueryResult();
            result.Count = project.Count();
            result.Data = project.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
