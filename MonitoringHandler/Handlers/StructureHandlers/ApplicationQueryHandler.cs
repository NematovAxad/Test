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
            var projects = _project.GetAll();
            var application = _application.GetAll().Select(a=> new {application = a,   Done = projects.Where(p=>p.Status == Domain.MonitoringProjectStatus.Done && p.ApplicationId == a.Id).Count(),
                                                                                        InProgress = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.InProgress && p.ApplicationId == a.Id).Count(),
                                                                                        NotDone = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.NotDone && p.ApplicationId == a.Id).Count(),
                                                                                        ForApproval = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.ForApproval && p.ApplicationId == a.Id).Count(),
                                                                                        ExecutedWithDelay = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.ExecutedWithDelay && p.ApplicationId == a.Id).Count(),
                                                                                        Canceled = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.Canceled && p.ApplicationId == a.Id).Count(),
                                                                                        FinalStage = projects.Where(p => p.Status == Domain.MonitoringProjectStatus.FinalStage && p.ApplicationId == a.Id).Count(),
                                                                                        ProjectsCount = projects.Where(p=>p.ApplicationId == a.Id).Count(),
                                                                                        }).ToList();
            
            if (request.Id != 0)
            {
                application = application.Where(n => n.application.Id == request.Id).ToList();
            }
            if (request.NormativeId != 0)
            {
                application = application.Where(n => n.application.NormativeLegalDocumentId == request.NormativeId).ToList();
            }
            
            ApplicationQueryResult result = new ApplicationQueryResult() { Statistics = new Statistics() };
            result.Count = application.Count();
            result.Data = application.OrderBy(u => u.application.Id).ToList<object>();
            result.Statistics.Done = application.Select(a => a.Done).Sum();
            result.Statistics.InProgress = application.Select(a => a.InProgress).Sum();
            result.Statistics.NotDone = application.Select(a => a.NotDone).Sum();
            result.Statistics.ForApproval = application.Select(a => a.ForApproval).Sum();
            result.Statistics.ExecutedWithDelay = application.Select(a => a.ExecutedWithDelay).Sum();
            result.Statistics.Canceled = application.Select(a => a.Canceled).Sum();
            result.Statistics.FinalStage = application.Select(a => a.FinalStage).Sum();
            result.Statistics.ProjectsCount = application.Select(a => a.ProjectsCount).Sum();
            return result;
        }
    }
}
