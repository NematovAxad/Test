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
    public class FutureYearsStrategiesQueryHandler : IRequestHandler<FutureYearsStrategiesQuery, FutureYearsStrategiesQueryResult>
    {
        private readonly IRepository<OrgFutureYearsStrategies, int> _futureStrategies;

        public FutureYearsStrategiesQueryHandler(IRepository<OrgFutureYearsStrategies, int> futureStrategies)
        {
            _futureStrategies = futureStrategies;
        }
        public async Task<FutureYearsStrategiesQueryResult> Handle(FutureYearsStrategiesQuery request, CancellationToken cancellationToken)
        {
            var futureStrategies = _futureStrategies.GetAll();
            if (request.Id != 0)
            {
                futureStrategies = futureStrategies.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                futureStrategies = futureStrategies.Where(s => s.OrganizationId == request.OrganizationId);
            }

            FutureYearsStrategiesQueryResult result = new FutureYearsStrategiesQueryResult();
            result.Count = futureStrategies.Count();
            result.Data = futureStrategies.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
