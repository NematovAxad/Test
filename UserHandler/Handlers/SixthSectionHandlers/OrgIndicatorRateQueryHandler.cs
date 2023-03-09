using Domain.Models.SixthSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrgIndicatorRateQueryHandler : IRequestHandler<OrgIndicatorRateQuery, OrgIndicatorRateQueryResult>
    {

        private readonly IRepository<IndicatorRating, int> _indicatorRating;


        public OrgIndicatorRateQueryHandler(IRepository<IndicatorRating, int> indicatorRating)
        {
            _indicatorRating = indicatorRating;
        }

        public async Task<OrgIndicatorRateQueryResult> Handle(OrgIndicatorRateQuery request, CancellationToken cancellationToken)
        {
            var indicatorRate = _indicatorRating.Find(i => i.OrganizationId == request.OrganizationId).FirstOrDefault();
            
            OrgIndicatorRateQueryResult result = new OrgIndicatorRateQueryResult();

            result.IndicatorRating = indicatorRate;

            return result;
        }
    }
}
