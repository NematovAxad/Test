using Domain;
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
    public class StageFileCommandHandler : IRequestHandler<StageFileCommand, StageFileCommandResult>
    {
        private readonly IRepository<FileStage, int> _fileStage;
        private readonly IRepository<Stage, int> _stage;

        public StageFileCommandHandler(IRepository<FileStage, int> fileStage, IRepository<Stage, int> stage)
        {
            _fileStage = fileStage;
            _stage = stage;
        }

        public async Task<StageFileCommandResult> Handle(StageFileCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new StageFileCommandResult() { IsSuccess = true };
        }
        public void Add(StageFileCommand model)
        {
            var stage = _stage.Find(p => p.Id == model.StageId).FirstOrDefault();
            if (stage == null)
                throw ErrorStates.NotFound(model.StageId.ToString());
            var path = "";
            if (model.File != null)
                path = FileState.AddFile("apiMonitoring","stageFiles", model.File);
            FileStage addModel = new FileStage()
            {
                Name = model.Name,
                Path = path,
                UserId = model.UserId,
                FileSaveDate = DateTime.Now,
                StageId = stage.Id
            };
            _fileStage.Add(addModel);
        }
        public void Delete(StageFileCommand model)
        {
            var file = _fileStage.Find(f => f.Id == model.Id).FirstOrDefault();
            if (file == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _fileStage.Remove(file);
        }
    }
}
