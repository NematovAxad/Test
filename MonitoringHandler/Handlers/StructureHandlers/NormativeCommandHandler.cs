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
    public class NormativeCommandHandler : IRequestHandler<NormativeCommand, NormativeCommandResult>
    {
        private readonly IRepository<NormativeLegalDocument, int> _normative;

        public NormativeCommandHandler(IRepository<NormativeLegalDocument, int> normative)
        {
            _normative = normative;
        }

        public async Task<NormativeCommandResult> Handle(NormativeCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new NormativeCommandResult() { IsSuccess = true };
        }
        public void Add(NormativeCommand model)
        {
            var normativ = _normative.Find(n => n.Number == model.Number).FirstOrDefault();
            if (normativ != null)
                throw ErrorStates.NotAllowed(model.Number.ToString());
            NormativeLegalDocument addModel = new NormativeLegalDocument
            {
                Number = model.Number,
                NameRu = model.NameRu,
                NameUz = model.NameUz,
                ApprovedDate = model.ApprovedDate,
                NormativType = model.NormativType
            };
            _normative.Add(addModel);
        }
        public void Update(NormativeCommand model)
        {
            var normativ = _normative.Find(n => n.Id == model.Id).FirstOrDefault();
            if (normativ == null)
                throw ErrorStates.NotFound(model.Number.ToString());
            normativ.NameRu = model.NameRu;
            normativ.NameUz = model.NameUz;
            normativ.Number = model.Number;
            normativ.NormativType = model.NormativType;
            normativ.ApprovedDate = model.ApprovedDate;

            _normative.Update(normativ);
        }
        public void Delete(NormativeCommand model)
        {
            var normativ = _normative.Find(n => n.Id == model.Id).FirstOrDefault();
            if (normativ == null)
                throw ErrorStates.NotFound(model.Number.ToString());
            _normative.Remove(normativ);
        }
    }
}
