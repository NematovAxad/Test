using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Organization
{
    public class BasedDocsCommandHandler : IRequestHandler<BasedDocsCommand, BasedDocsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<BasedDocuments, int> _basedDocs;

        public BasedDocsCommandHandler(IRepository<Organizations, int> organizations, IRepository<BasedDocuments, int> basedDocs)
        {
            _organizations = organizations;
            _basedDocs = basedDocs;
        }

        public async Task<BasedDocsCommandResult> Handle(BasedDocsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new BasedDocsCommandResult() { IsSuccess = true };
        }
        public void Add(BasedDocsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.BasedDoc.OrganizationId).FirstOrDefault();

            if (org == null)
            {
                throw ErrorStates.NotFound(model.BasedDoc.OrganizationId.ToString());
            }

            var doc = _basedDocs.Find(d => d.DocumentNo == model.BasedDoc.DocumentNo).FirstOrDefault();
            if (doc != null)
                throw ErrorStates.NotAllowed(model.BasedDoc.DocumentNo);
            _basedDocs.Add(model.BasedDoc);
        }
        public void Delete(BasedDocsCommand model)
        {
            if (model.BasedDoc.Id == 0)
            {
                throw ErrorStates.NotFound("");
            }
            _basedDocs.Remove(model.BasedDoc.Id);
        }
    }
}
