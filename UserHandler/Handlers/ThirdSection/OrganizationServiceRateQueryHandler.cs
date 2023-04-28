using Domain.Models.FirstSection;
using Domain.Models.ThirdSection;
using JohaRepository;
using MainInfrastructures.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class OrganizationServiceRateQueryHandler : IRequestHandler<OrganizationServiceRateQuery, OrganizationServiceRateQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationServices, int> _organizationServices;
        private readonly IRepository<OrganizationServicesRate, int> _organizationServicesRate;
        private readonly IOrganizationService _services;

        public OrganizationServiceRateQueryHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationServices, int> organizationServices, IRepository<OrganizationServicesRate, int> organizationServicesRate, IOrganizationService services)
        {
            _organizations = organizations;
            _organizationServices = organizationServices;
            _organizationServicesRate = organizationServicesRate;
            _services = services;
        }

        public async Task<OrganizationServiceRateQueryResult> Handle(OrganizationServiceRateQuery request, CancellationToken cancellationToken)
        {
            var rates = _organizationServicesRate.Find(r=>r.ServiceId == request.ServiceId).ToList();

            if(request.RateId != 0)
            {
                rates = rates.Where(r=>r.Id == request.RateId).ToList();
            }

            OrganizationServiceRateQueryResult result = new OrganizationServiceRateQueryResult();

            result.Rates = rates.OrderBy(r=>r.Id).ToList();

            return result;
        }
    }
}
