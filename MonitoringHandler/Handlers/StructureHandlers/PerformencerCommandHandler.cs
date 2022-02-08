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
    public class PerformencerCommandHandler : IRequestHandler<PerformencerCommand, PerformencerCommandResult>
    {
        private readonly IRepository<Performencer, int> _performencer;

        public PerformencerCommandHandler(IRepository<Performencer, int> performencer)
        {
            _performencer = performencer;
        }

        public async Task<PerformencerCommandResult> Handle(PerformencerCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new PerformencerCommandResult() { IsSuccess = true };
        }
        public void Add(PerformencerCommand model)
        {
            var p = _performencer.Find(p => p.Name == model.Name).FirstOrDefault();
            if (p != null)
                throw ErrorStates.NotAllowed(model.Name);
            Performencer addModel = new Performencer()
            {
                Name = model.Name
            };
            _performencer.Add(addModel);
        }
        public void Update(PerformencerCommand model)
        {
            var p = _performencer.Find(p => p.Id == model.Id).FirstOrDefault();
            if (p == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            p.Name = model.Name;
            _performencer.Update(p);
        }
        public void Delete(PerformencerCommand model)
        {
            var p = _performencer.Find(p => p.Id == model.Id).FirstOrDefault();
            if (p == null)
                throw ErrorStates.NotFound(model.Id.ToString());
        }
    }
}
