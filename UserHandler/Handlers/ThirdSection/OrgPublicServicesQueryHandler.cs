using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.Models.ThirdSection;
using Domain.States;
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
    public class OrgPublicServicesQueryHandler : IRequestHandler<OrgPublicServicesQuery, OrgPublicServicesQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationPublicServices, int> _orgPublicServices;

        public OrgPublicServicesQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationPublicServices, int> orgPublicServices)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgPublicServices = orgPublicServices;
        }

        public async Task<OrgPublicServicesQueryResult> Handle(OrgPublicServicesQuery request, CancellationToken cancellationToken)
        {
            var services = _orgPublicServices.GetAll();

            if (request.OrganizationId != 0)
            {
                services = services.Where(s => s.OrganizationId == request.OrganizationId);
            }

            OrgPublicServicesQueryResult result = new OrgPublicServicesQueryResult();
            result.Count = services.Count();
            result.Data = services.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
