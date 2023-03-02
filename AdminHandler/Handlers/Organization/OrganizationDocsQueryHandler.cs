using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
using Domain.Models.FirstSection;
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
    public class OrganizationDocsQueryHandler : IRequestHandler<OrganizationDocsQuery, OrganizationDocsQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationDocuments, int> _orgDocuments;

        public OrganizationDocsQueryHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationDocuments, int> orgDocuments)
        {
            _organizations = organizations;
            _orgDocuments = orgDocuments;
        }

        public async Task<OrganizationDocsQueryResult> Handle(OrganizationDocsQuery request, CancellationToken cancellationToken)
        {
            var docs = _orgDocuments.GetAll();

            if (request.Id != 0)
            {
                docs = docs.Where(o => o.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                docs = docs.Where(o => o.OrganizationId == request.OrganizationId);
            }

            OrganizationDocsQueryResult result = new OrganizationDocsQueryResult();
            result.Count = docs.Count();
            result.Data = docs.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
