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
    public class FinancierCommandHandler : IRequestHandler<FinancierCommand, FinancierCommandResult>
    {
        private readonly IRepository<Financier, int> _financier;

        public FinancierCommandHandler(IRepository<Financier, int> financier)
        {
            _financier = financier;
        }

        public async Task<FinancierCommandResult> Handle(FinancierCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new FinancierCommandResult() { IsSuccess = true };
        }
        public void Add(FinancierCommand model)
        {
            var f = _financier.Find(f => f.NameRu == model.NameRu).FirstOrDefault();
            if (f != null)
                throw ErrorStates.NotAllowed(model.NameRu);
            Financier addModel = new Financier()
            {
                NameRu = model.NameRu,
                NameUz = model.NameUz
            };

            _financier.Add(addModel);
        }
        public void Update(FinancierCommand model)
        {
            var f = _financier.Find(f => f.Id == model.Id).FirstOrDefault();
            if (f == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            f.NameRu = model.NameRu;
            f.NameUz = model.NameUz;

            _financier.Update(f);
        }
        public void Delete(FinancierCommand model)
        {
            var f = _financier.Find(f => f.Id == model.Id).FirstOrDefault();
            if (f == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _financier.Remove(f);
        }
    }
}
