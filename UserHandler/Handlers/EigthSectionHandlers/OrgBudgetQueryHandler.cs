using Domain.Models;
using Domain.States;
using Domain;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Domain.Models.FirstSection;
using Domain.Models.EighthSection;
using UserHandler.Queries.EigthSectionQuery;
using UserHandler.Results.EigthSectionResult;

namespace UserHandler.Handlers.EigthSectionHandlers
{
    public class OrgBudgetQueryHandler : IRequestHandler<OrgBudgetQuery, OrgBudgetQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationBudget, int> _orgBudget;

        public OrgBudgetQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationBudget, int> orgBudget)
        {
            _organization = organization;
            _deadline = deadline;
            _orgBudget = orgBudget;
        }
        public async Task<OrgBudgetQueryResult> Handle(OrgBudgetQuery request, CancellationToken cancellationToken)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var orgBudget = _orgBudget.Find(p => p.OrganizationId == request.OrganizationId && p.Year == deadline.Year).FirstOrDefault();

            OrgBudgetQueryResult result = new OrgBudgetQueryResult();
            result.OrganizationBudget = orgBudget;
            return result;
        }
    }
}
