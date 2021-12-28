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
    public class OrgEventsQueryHandler : IRequestHandler<OrgEventsQuery, OrgEventsQueryResult>
    {
        private readonly IRepository<OrganizationEvents, int> _orgEvents;

        public OrgEventsQueryHandler(IRepository<OrganizationEvents, int> orgEvents)
        {
            _orgEvents = orgEvents;
        }

        public async Task<OrgEventsQueryResult> Handle(OrgEventsQuery request, CancellationToken cancellationToken)
        {
            var orgEvents = _orgEvents.GetAll();
            if (request.Id != 0)
            {
                orgEvents = orgEvents.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                orgEvents = orgEvents.Where(s => s.OrganizationId == request.OrganizationId);
            }

            OrgEventsQueryResult result = new OrgEventsQueryResult();
            result.Count = orgEvents.Count();
            result.Data = orgEvents.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
