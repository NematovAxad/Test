using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain;
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
    public class OrganizationDocsCommandHandler : IRequestHandler<OrganizationDocsCommand, OrganizationDocsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationDocuments, int> _orgDocuments;

        public OrganizationDocsCommandHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationDocuments, int> orgDocuments)
        {
            _organizations = organizations;
            _orgDocuments = orgDocuments;
        }

        public async Task<OrganizationDocsCommandResult> Handle(OrganizationDocsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrganizationDocsCommandResult() { IsSuccess = true };
        }

        public void Add(OrganizationDocsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var filePath = FileState.AddFile("orgDocs", model.File);
            OrganizationDocuments addModel = new OrganizationDocuments()
            {
                OrganizationId = model.OrganizationId,
                DocumentNo = model.DocumentNo,
                DocumentDate = model.DocumentDate,
                DocumentType = model.DocumentType,
                DocumentName = model.DocumentName,
                MainPurpose = model.MainPurpose,
                Path = filePath
            };
            _orgDocuments.Add(addModel);
        }

        public void Update(OrganizationDocsCommand model)
        {
            var orgDoc = _orgDocuments.Find(d => d.Id == model.Id).FirstOrDefault();
            if (orgDoc == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var filePath = FileState.AddFile("orgDocs", model.File);
            OrganizationDocuments updateModel = new OrganizationDocuments()
            {
                OrganizationId = orgDoc.OrganizationId,
                DocumentNo = model.DocumentNo,
                DocumentDate = model.DocumentDate,
                DocumentType = model.DocumentType,
                DocumentName = model.DocumentName,
                MainPurpose = model.MainPurpose,
                Path = filePath
            };
            _orgDocuments.Add(updateModel);
        }

        public void Delete(OrganizationDocsCommand model)
        {
            _orgDocuments.Remove(model.Id);
        }
    }
}
