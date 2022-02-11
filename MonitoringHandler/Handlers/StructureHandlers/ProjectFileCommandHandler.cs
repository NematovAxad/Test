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
    public class ProjectFileCommandHandler : IRequestHandler<ProjectFileCommand, ProjectFileCommandResult>
    {
        private readonly IRepository<FileProject, int> _fileProject;
        private readonly IRepository<Project, int> _project;

        public ProjectFileCommandHandler(IRepository<FileProject, int> fileProject, IRepository<Project, int> project)
        {
            _fileProject = fileProject;
            _project = project;
        }

        public async Task<ProjectFileCommandResult> Handle(ProjectFileCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ProjectFileCommandResult() { IsSuccess = true };
        }
        public void Add(ProjectFileCommand model)
        {
            var project = _project.Find(p => p.Id == model.ProjectId).FirstOrDefault();
            if (project == null)
                throw ErrorStates.NotFound(model.ProjectId.ToString());
            var path = "";
            if(model.File !=null)
                path = FileState.AddFile("projectFiles", model.File);
            FileProject addModel = new FileProject()
            {
                Name = model.Name,
                Path = path,
                UserId = model.UserId,
                FileSaveDate = DateTime.Now,
                ProjectId = project.Id
            };
            _fileProject.Add(addModel);
        }
        public void Delete(ProjectFileCommand model)
        {
            var file = _fileProject.Find(f => f.Id == model.Id).FirstOrDefault();
            if (file == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _fileProject.Remove(file);
        }
    }
}
