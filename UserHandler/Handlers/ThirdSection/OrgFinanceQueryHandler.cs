using Domain.Models.FifthSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class OrgFinanceQueryHandler : IRequestHandler<OrgFinanceQuery, OrgFinanceQueryResult>
    {
        private readonly IRepository<OrgFinance, int> _orgFinance;

        public OrgFinanceQueryHandler(IRepository<OrgFinance, int> orgFinance)
        {
            _orgFinance = orgFinance;
        }

        public async Task<OrgFinanceQueryResult> Handle(OrgFinanceQuery request, CancellationToken cancellationToken)
        {
            var orgFinance = _orgFinance.GetAll();
            if (request.Id != 0)
            {
                orgFinance = orgFinance.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                orgFinance = orgFinance.Where(s => s.OrganizationId == request.OrganizationId);
            }

            OrgFinanceQueryResult result = new OrgFinanceQueryResult();
            result.Count = orgFinance.Count();
            result.Data = orgFinance.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
