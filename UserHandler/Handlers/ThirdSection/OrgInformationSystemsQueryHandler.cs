using Domain.Models;
using Domain.Models.FifthSection;
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
    public class OrgInformationSystemsQueryHandler : IRequestHandler<OrgInformationSystemsQuery, OrgInformationSystemsQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrgInformationSystems, int> _orgInfoSystem;

        public OrgInformationSystemsQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrgInformationSystems, int> orgInfoSystem)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgInfoSystem = orgInfoSystem;
        }

        public async Task<OrgInformationSystemsQueryResult> Handle(OrgInformationSystemsQuery request, CancellationToken cancellationToken)
        {
            var services = _orgInfoSystem.GetAll();

            if (request.OrganizationId != 0)
            {
                services = services.Where(s => s.OrganizationId == request.OrganizationId);
            }

            OrgInformationSystemsQueryResult result = new OrgInformationSystemsQueryResult();
            result.Count = services.Count();
            result.Data = services.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
