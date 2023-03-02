using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
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
    public class SubOrgStatisticsQueryHandler : IRequestHandler<SubOrgStatisticsQuery, SubOrgStatisticsQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<SubOrgStatistics, int> _subOrgStatistics;

        public SubOrgStatisticsQueryHandler(IRepository<Organizations, int> organizations, IRepository<SubOrgStatistics, int> subOrgStatistics)
        {
            _organizations = organizations;
            _subOrgStatistics = subOrgStatistics;
        }
        public async Task<SubOrgStatisticsQueryResult> Handle(SubOrgStatisticsQuery request, CancellationToken cancellationToken)
        {
            var org = _organizations.Find(o=>o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());
            var stat = _subOrgStatistics.GetAll();

            if (request.OrganizationId != 0)
            {
                stat = stat.Where(o => o.OrganizationId == request.OrganizationId);
            }

            SubOrgStatisticsQueryResult result = new SubOrgStatisticsQueryResult();
            result.Count = stat.Count();
            result.Data = stat.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
