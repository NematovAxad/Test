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
    public class ApplicationQueryHandler : IRequestHandler<ApplicationQuery, ApplicationQueryResult>
    {
        private readonly IRepository<Application, int> _application;
        private readonly IRepository<Project, int> _project;

        public ApplicationQueryHandler(IRepository<Application, int> application, IRepository<Project, int> project)
        {
            _application = application;
            _project = project;
        }

        public async Task<ApplicationQueryResult> Handle(ApplicationQuery request, CancellationToken cancellationToken)
        {
            var application = _application.GetAll().Include(mbox=>mbox.Projects);
            var projects = _project.GetAll();
            if (request.Id != 0)
            {
                application = application.Where(n => n.Id == request.Id).Include(mbox => mbox.Projects);
            }
            if (request.NormativeId != 0)
            {
                application = application.Where(n => n.NormativeLegalDocumentId == request.NormativeId).Include(mbox => mbox.Projects);
            }
            projects = projects.Where(p => application.Any(a => a.Id == p.ApplicationId));
            ApplicationQueryResult result = new ApplicationQueryResult();
            result.Count = application.Count();
            result.Data = application.OrderBy(u => u.Id).ToList<object>();
            result.Done = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.Done).Count();
            result.InProgress = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.InProgress).Count();
            result.NotDone = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.NotDone).Count();
            result.ForApproval = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.ForApproval).Count();
            result.ExecutedWithDelay = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.ExecutedWithDelay).Count();
            result.Canceled = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.Canceled).Count();
            result.FinalStage = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.FinalStage).Count();
            result.ProjectsCount = projects.Count();
            return result;
        }
    }
}
