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
    public class SubOrgQueryHandler : IRequestHandler<SubOrgQuery, SubOrgQueryResult>
    {
        private readonly IRepository<SubOrganizations, int> _subOrganizations;
        public SubOrgQueryHandler(IRepository<SubOrganizations, int> subOrganizations)
        {
            _subOrganizations = subOrganizations;
        }

        public async Task<SubOrgQueryResult> Handle(SubOrgQuery request, CancellationToken cancellationToken)
        {
            var subOrg = _subOrganizations.GetAll();

            if (request.ParentId != 0)
            {
                subOrg = subOrg.Where(o => o.OrganizationId == request.ParentId);
            }
            if (request.Id != 0)
            {
                subOrg = subOrg.Where(o => o.Id == request.Id);
            }

            SubOrgQueryResult result = new SubOrgQueryResult();
            result.Count = subOrg.Count();
            result.Data = subOrg.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
