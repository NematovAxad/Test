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
    public class ApplicationCommandHandler : IRequestHandler<ApplicationCommand, ApplicationCommandResult>
    {
        private readonly IRepository<Application, int> _application;
        private readonly IRepository<NormativeLegalDocument, int> _normative;
        public ApplicationCommandHandler(IRepository<Application, int> application, IRepository<NormativeLegalDocument, int> normative)
        {
            _application = application;
            _normative = normative;
        }

        public async Task<ApplicationCommandResult> Handle(ApplicationCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ApplicationCommandResult() { IsSuccess = true };
        }
        public void Add(ApplicationCommand model)
        {
            var application = _application.Find(a => a.ShortName == model.ShortName && a.NormativeLegalDocumentId == model.NormativeLegalDocumentId).FirstOrDefault();
            if (application != null)
                throw ErrorStates.NotAllowed(model.ShortName);
            var normative = _normative.Find(n => n.Id == model.NormativeLegalDocumentId).FirstOrDefault();
            if (normative == null)
                throw ErrorStates.NotFound(model.NormativeLegalDocumentId.ToString());

            Application addModel = new Application()
            {
                NameUz = model.NameUz,
                NameRu = model.NameRu,
                ShortName = model.ShortName,
                PerformanceYearStart = model.PerformanceYearStart,
                PerformanceYearEnd = model.PerformanceYearEnd,
                NormativeLegalDocumentId = model.NormativeLegalDocumentId
            };
            _application.Add(addModel);
        }
        public void Update(ApplicationCommand model)
        {
            var application = _application.Find(a => a.Id == model.Id).FirstOrDefault();
            if (application == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            application.NameRu = model.NameRu;
            application.NameUz = model.NameUz;
            application.ShortName = model.ShortName;
            application.PerformanceYearStart = model.PerformanceYearStart;
            application.PerformanceYearEnd = model.PerformanceYearEnd;

            _application.Update(application);
        }
        public void Delete(ApplicationCommand model)
        {
            var application = _application.Find(a => a.Id == model.Id).FirstOrDefault();
            if (application == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _application.Remove(application);
        }
    }
}
