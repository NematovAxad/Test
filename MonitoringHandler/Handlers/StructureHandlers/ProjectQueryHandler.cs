using Domain.MonitoringModels.Models;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var project = _project.GetAll().Include(mbox=>mbox.ProjectComments).Include(mbox => mbox.ProjectFiles).Include(mbox => mbox.ProjectFinanciers).ThenInclude(mbox => mbox.Financier).Include(mbox => mbox.Cooworkers).ThenInclude(mbox => mbox.Performencer);
            if (request.Id != 0)
            {
                project = project.Where(n => n.Id == request.Id).Include(mbox => mbox.ProjectComments).Include(mbox => mbox.ProjectFiles).Include(mbox => mbox.ProjectFinanciers).ThenInclude(mbox => mbox.Financier).Include(mbox => mbox.Cooworkers).ThenInclude(mbox => mbox.Performencer);
            }
            if (request.PerformencerId != 0)
            {
                project = project.Where(n => n.PerformencerId == request.PerformencerId).Include(mbox => mbox.ProjectComments).Include(mbox => mbox.ProjectFiles).Include(mbox => mbox.ProjectFinanciers).ThenInclude(mbox => mbox.Financier).Include(mbox => mbox.Cooworkers).ThenInclude(mbox => mbox.Performencer);
            }
            if (request.Status != 0)
            {
                project = project.Where(n => n.Status == request.Status).Include(mbox => mbox.ProjectComments).Include(mbox => mbox.ProjectFiles).Include(mbox => mbox.ProjectFinanciers).ThenInclude(mbox => mbox.Financier).Include(mbox => mbox.Cooworkers).ThenInclude(mbox => mbox.Performencer);
            }
            if (request.ApplicationId != 0)
            {
                project = project.Where(n => n.ApplicationId == request.ApplicationId).Include(mbox => mbox.ProjectComments).Include(mbox => mbox.ProjectFiles).Include(mbox => mbox.ProjectFinanciers).ThenInclude(mbox => mbox.Financier).Include(mbox => mbox.Cooworkers).ThenInclude(mbox => mbox.Performencer);
            }
            ProjectQueryResult result = new ProjectQueryResult();
            result.Count = project.Count();
            result.Data = project.OrderBy(u => u.Id).ToList<object>();
            result.Done = project.Where(p => p.Status == Domain.MonitoringProjectStatus.Done).Count();
            result.InProgress = project.Where(p => p.Status == Domain.MonitoringProjectStatus.InProgress).Count();
            result.NotDone = project.Where(p => p.Status == Domain.MonitoringProjectStatus.NotDone).Count();
            result.ForApproval = project.Where(p => p.Status == Domain.MonitoringProjectStatus.ForApproval).Count();
            result.ExecutedWithDelay = project.Where(p => p.Status == Domain.MonitoringProjectStatus.ExecutedWithDelay).Count();
            result.Canceled = project.Where(p => p.Status == Domain.MonitoringProjectStatus.Canceled).Count();
            result.FinalStage = project.Where(p => p.Status == Domain.MonitoringProjectStatus.FinalStage).Count();
            result.ProjectsCount = project.Count();
            return result;
        }
    }
}
