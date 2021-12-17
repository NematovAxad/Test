using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrganizationAppsQueryHandler : IRequestHandler<OrganizationAppsQuery, OrganizationAppsQueryResult>
    {
        private readonly IRepository<OrganizationApps, int> _organizationApps;
        
        public OrganizationAppsQueryHandler(IRepository<OrganizationApps, int> organizationApps)
        {
            _organizationApps = organizationApps;
        }

        public async Task<OrganizationAppsQueryResult> Handle(OrganizationAppsQuery request, CancellationToken cancellationToken)
        {
            var orgApps = _organizationApps.GetAll();
            if (request.Id != 0)
                orgApps = orgApps.Where(m => m.Id == request.Id);
            if (request.OrganizationId != 0)
                orgApps = orgApps.Where(m => m.OrganizationId == request.OrganizationId);

            OrganizationAppsQueryResult result = new OrganizationAppsQueryResult();
            result.Count = orgApps.Count();
            result.Data = orgApps.OrderBy(u => u.Id).ToList<object>();
            return result;

        }
    }
}
