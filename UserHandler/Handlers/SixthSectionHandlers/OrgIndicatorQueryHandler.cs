using Domain.Models.FirstSection;
using Domain.Models.SixthSection;
using JohaRepository;
using MediatR;
using SB.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrgIndicatorQueryHandler : IRequestHandler<OrgIndicatorQuery, OrgIndicatorQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationIndicators, int> _orgIndicators;

        public OrgIndicatorQueryHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationIndicators, int> orgIndicators)
        {
            _organizations = organizations;
            _orgIndicators = orgIndicators;
        }

        public async Task<OrgIndicatorQueryResult> Handle(OrgIndicatorQuery request, CancellationToken cancellationToken)
        {
            var indicators = _orgIndicators.Find(o => o.OrganizationId == request.OrganizationId).ToList();

            OrgIndicatorQueryResult result = new OrgIndicatorQueryResult();

            result.Count = indicators.Count;
            result.Data = indicators.OrderByDescending(i => i.Id).ToList();

            return result;
        }
    }
}
