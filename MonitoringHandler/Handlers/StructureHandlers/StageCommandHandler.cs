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
    public class StageCommandHandler : IRequestHandler<StageCommand, StageCommandResult>
    {
        private readonly IRepository<Stage, int> _stage;
        private readonly IRepository<Project, int> _project;

        public StageCommandHandler(IRepository<Stage, int> stage, IRepository<Project, int> project)
        {
            _stage = stage;
            _project = project;
        }

        public async Task<StageCommandResult> Handle(StageCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new StageCommandResult() { IsSuccess = true };
        }
        public void Add(StageCommand model)
        {
            var stage = _stage.Find(s => s.NameRu == model.NameRu && s.ProjectId == model.ProjectId).FirstOrDefault();
            if (stage != null)
                throw ErrorStates.NotAllowed(model.NameRu);
            Stage addModel = new Stage()
            {
                NameRu = model.NameRu,
                NameUz = model.NameUz,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StageStatus = model.StageStatus,
                ProjectId = model.ProjectId,
                CreationUserId = model.UserId,
                CreationUserName = model.UserId.ToString(),
                CreationDate = DateTime.Now
            };
            _stage.Add(addModel);
        }
        public void Update(StageCommand model)
        {
            var stage = _stage.Find(s => s.Id == model.Id).FirstOrDefault();
            if (stage == null)
                throw ErrorStates.NotAllowed(model.NameRu);
            stage.NameRu = model.NameRu;
            stage.NameUz = model.NameUz;
            stage.StartDate = model.StartDate;
            stage.EndDate = model.EndDate;
            stage.StageStatus = model.StageStatus;

            _stage.Update(stage);
        }
        public void Delete(StageCommand model)
        {
            var stage = _stage.Find(s => s.Id == model.Id).FirstOrDefault();
            if (stage == null)
                throw ErrorStates.NotAllowed(model.NameRu);
            _stage.Remove(stage);
        }
    }
}
