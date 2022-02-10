using Domain.MonitoringModels.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using MonitoringHandler.Commands.StructureCommands;
using MonitoringHandler.Results.StructureResults.CommandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringHandler.Handlers.StructureHandlers
{
    public class ProjectFinanciersCommandHandler : IRequestHandler<ProjectFinanciersCommand, ProjectFinanciersCommandResult>
    {
        private readonly IRepository<ProjectFinanciers, int> _pFinanciers;
        private readonly IRepository<Project, int> _project;
        private readonly IRepository<Financier, int> _financier;

        public ProjectFinanciersCommandHandler(IRepository<ProjectFinanciers, int> pFinanciers, IRepository<Project, int> project, IRepository<Financier, int> financier)
        {
            _pFinanciers = pFinanciers;
            _project = project;
            _financier = financier;
        }

        public async Task<ProjectFinanciersCommandResult> Handle(ProjectFinanciersCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ProjectFinanciersCommandResult() { IsSuccess = true };
        }
        public void Add(ProjectFinanciersCommand model)
        {
            var project = _project.Find(p => p.Id == model.ProjectId).FirstOrDefault();
            if (project == null)
                throw ErrorStates.NotFound(model.ProjectId.ToString());
            var financier = _financier.Find(f => f.Id == model.FinancierId).FirstOrDefault();
            if (financier == null)
                throw ErrorStates.NotFound(model.FinancierId.ToString());

            var projectFinancier = _pFinanciers.Find(p => p.ProjectId == model.ProjectId && p.FinancierId == model.FinancierId).FirstOrDefault();
            if (projectFinancier != null)
                throw ErrorStates.NotAllowed(model.ProjectId.ToString());
            ProjectFinanciers addModel = new ProjectFinanciers()
            {
                ProjectId = model.ProjectId,
                FinancierId = model.FinancierId
            };
            _pFinanciers.Add(addModel);
        }
        public void Delete(ProjectFinanciersCommand model)
        {
            var projectFinancier = _pFinanciers.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectFinancier == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _pFinanciers.Remove(projectFinancier);
        }
    }
}
