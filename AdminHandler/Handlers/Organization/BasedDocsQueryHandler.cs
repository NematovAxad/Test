using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
using Domain;
using Domain.Models.FirstSection;
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
    public class BasedDocsQueryHandler : IRequestHandler<BasedDocsQuery, BasedDocsQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<BasedDocuments, int> _basedDocuments;

        public BasedDocsQueryHandler(IRepository<Organizations, int> organizations, IRepository<BasedDocuments, int> basedDocuments)
        {
            _organizations = organizations;
            _basedDocuments = basedDocuments;
        }

        public async Task<BasedDocsQueryResult> Handle(BasedDocsQuery request, CancellationToken cancellationToken)
        {
            var org = _organizations.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            var docs = _basedDocuments.GetAll();
            if (request.Id != 0)
            {
                docs = docs.Where(o => o.Id == request.Id);
            }

            if (request.OrganizationId != 0)
            {
                docs = docs.Where(o => o.OrganizationId == request.OrganizationId);
            }

            BasedDocsQueryResult result = new BasedDocsQueryResult();
            result.Count = docs.Count();
            result.Data = docs.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
