using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
using Domain.Models;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Organization
{
    public class OrgQueryHandler : IRequestHandler<OrgQuery, OrgQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;

        public OrgQueryHandler(IRepository<Organizations, int> organizations)
        {
            _organizations = organizations;
        }
        public async Task<OrgQueryResult> Handle(OrgQuery request, CancellationToken cancellationToken)
        {
            var organization = _organizations.GetAll().Include(m => m.BasedDocuments).Include(mbox=>mbox.SubOrganizations);

            if (request.OrgId != 0)
            {
                organization = organization.Where(o=>o.Id == request.OrgId).Include(m => m.BasedDocuments).Include(mbox => mbox.SubOrganizations);
            }

            OrgQueryResult result = new OrgQueryResult();
            result.Count = organization.Count();
            result.Data = organization.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
