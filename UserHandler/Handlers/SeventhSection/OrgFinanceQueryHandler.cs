using Domain.Models.SeventhSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SeventhSection;
using UserHandler.Results.SeventhSection;
using Domain.States;
using Domain;
using System.Linq;

namespace UserHandler.Handlers.SeventhSection
{
    public class OrgFinanceQueryHandler : IRequestHandler<OrgFinanceQuery, OrgFinanceQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationFinance, int> _orgFinance;

        public OrgFinanceQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationFinance, int> orgFinance)
        {
            _organization = organization;
            _deadline = deadline;
            _orgFinance = orgFinance;
        }
        public async Task<OrgFinanceQueryResult> Handle(OrgFinanceQuery request, CancellationToken cancellationToken)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var orgFinance = _orgFinance.Find(p => p.OrganizationId == request.OrganizationId && p.Year == deadline.Year).FirstOrDefault();

            OrgFinanceQueryResult result = new OrgFinanceQueryResult();
            result.OrgFinance = orgFinance;
            return result;
        }
    }
}
