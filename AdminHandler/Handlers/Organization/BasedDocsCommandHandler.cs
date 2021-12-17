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
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new BasedDocsCommandResult() { IsSuccess = true };
        }
        public void Add(BasedDocsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();

            if (org == null)
            {
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            }

            var doc = _basedDocs.Find(d => d.DocumentNo == model.DocumentNo).FirstOrDefault();
            if (doc != null)
                throw ErrorStates.NotAllowed(model.DocumentNo);

            var filePath = FileState.AddFile("basedDocs", model.File);

            BasedDocuments addModel = new BasedDocuments()
            {
                OrganizationId = model.OrganizationId,
                DocumentNo = model.DocumentNo,
                DocumentDate = DateTime.Now,
                DocumentType = model.DocumentType,
                AcceptedOrg = model.AcceptedOrg,
                DocumentName = model.DocumentName,
                Path = filePath
            };
            _basedDocs.Add(addModel);
        }
        public void Update(BasedDocsCommand model)
        {
            var doc = _basedDocs.Find(d => d.Id == model.Id).FirstOrDefault();
            if (doc == null)
                throw ErrorStates.NotAllowed(model.DocumentNo);

            doc.DocumentNo = model.DocumentNo;
            doc.DocumentDate = DateTime.Now;
            doc.DocumentType = model.DocumentType;
            doc.AcceptedOrg = model.AcceptedOrg;
            doc.DocumentName = model.DocumentName;


            if(model.File!=null)
            {
                var filePath = FileState.AddFile("basedDocs", model.File);
                doc.Path = filePath;
            }
            _basedDocs.Update(doc);
        }
        public void Delete(BasedDocsCommand model)
        {
            var doc = _basedDocs.Find(d => d.Id == model.Id).FirstOrDefault();
            if (doc is null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (model.Id == 0)
            {
                throw ErrorStates.NotFound("");
            }
            _basedDocs.Remove(model.Id);
        }
    }
}
