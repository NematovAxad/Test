using Domain.Models.FirstSection;
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
    public class CooworkersCommandHandler : IRequestHandler<CooworkersCommand, CooworkersCommandResult>
    {
        private readonly IRepository<Cooworkers, int> _cooworkers;
        private readonly IRepository<Project, int> _project;
        private readonly IRepository<Organizations, int> _organizations;

        public CooworkersCommandHandler(IRepository<Cooworkers, int> cooworkers, IRepository<Project, int> project, IRepository<Organizations, int> organizations)
        {
            _cooworkers = cooworkers;
            _project = project;
            _organizations = organizations;
        }

        public async Task<CooworkersCommandResult> Handle(CooworkersCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new CooworkersCommandResult() { IsSuccess = true };
        }
        public void Add(CooworkersCommand model)
        {
            var project = _project.Find(p => p.Id == model.ProjectId).FirstOrDefault();
            if (project == null)
                throw ErrorStates.NotFound(model.ProjectId.ToString());
            var financier = _organizations.Find(f => f.Id == model.OrganizationId).FirstOrDefault();
            if (financier == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var cooworkers = _cooworkers.Find(p => p.ProjectId == model.ProjectId && p.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (cooworkers != null)
                throw ErrorStates.NotAllowed(model.ProjectId.ToString());
            Cooworkers addModel = new Cooworkers()
            {
                ProjectId = model.ProjectId,
                OrganizationId = model.OrganizationId
            };
            _cooworkers.Add(addModel);
        }
        public void Delete(CooworkersCommand model)
        {
            var projectFinancier = _cooworkers.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectFinancier == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _cooworkers.Remove(projectFinancier);
        }
    }
}
