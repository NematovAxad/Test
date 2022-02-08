﻿using Domain.MonitoringModels.Models;
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
    public class ProjectCommandHandler : IRequestHandler<ProjectCommand, ProjectCommandResult>
    {
        private readonly IRepository<Project, int> _project;
        private readonly IRepository<Application, int> _application;
        public ProjectCommandHandler(IRepository<Project, int> project, IRepository<Application, int> application)
        {
            _project = project;
            _application = application;
        }

        public async Task<ProjectCommandResult> Handle(ProjectCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ProjectCommandResult() { IsSuccess = true };
        }
        public void Add(ProjectCommand model)
        {
            var project = _project.Find(p => p.NameRu == model.NameRu || p.NameUz == model.NameUz).FirstOrDefault();
            if (project != null)
                throw ErrorStates.NotAllowed(model.NameUz);
            Project addModel = new Project()
            {
                NameUz = model.NameUz,
                NameRu = model.NameRu,
                Status = model.Status,
                ProjectPurpose = model.ProjectPurpose,
                CostEffective = model.CostEffective,
                Problem = model.Problem,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                VolumeForecastFunds = model.VolumeForecastFunds,
                RaisedFunds = model.RaisedFunds,
                Payouts = model.Payouts,
                PerformencerId = model.PerformencerId,
                ApplicationId = model.ApplicationId
            };
            _project.Add(addModel);
        }
        public void Update(ProjectCommand model)
        {
            var project = _project.Find(p => p.Id == model.Id).FirstOrDefault();
            if (project == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            project.NameUz = model.NameUz;
            project.NameRu = model.NameRu;
            project.Status = model.Status;
            project.ProjectPurpose = model.ProjectPurpose;
            project.CostEffective = model.CostEffective;
            project.Problem = model.Problem;
            project.StartDate = model.StartDate;
            project.EndDate = model.EndDate;
            project.VolumeForecastFunds = model.VolumeForecastFunds;
            project.RaisedFunds = model.RaisedFunds;
            project.Payouts = model.Payouts;
            project.PerformencerId = model.PerformencerId;

            _project.Update(project);
        }
        public void Delete(ProjectCommand model)
        {
            var project = _project.Find(p => p.Id == model.Id).FirstOrDefault();
            if (project == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _project.Remove(project);
        }
    }
}