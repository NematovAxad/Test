using Domain.Models.FirstSection;
using Domain.Models.SixthSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;
using System.Linq;
using Domain.States;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrgDataAvailabilityQueryHandler : IRequestHandler<OrgDataAvailabilityQuery, OrgDataAvailabilityQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationDataAvailability, int> _orgDataAvailability;


        public OrgDataAvailabilityQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationDataAvailability, int> orgDataAvailability)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDataAvailability = orgDataAvailability;
        }

        public async Task<OrgDataAvailabilityQueryResult> Handle(OrgDataAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrgId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            var orgData = _orgDataAvailability.Find(d => d.OrganizationId == request.OrgId && d.Section == request.Section && d.DeadlineId == deadline.Id).FirstOrDefault();

            OrgDataAvailabilityQueryResult result = new OrgDataAvailabilityQueryResult();
             
            result.OrganizationDataAvailability = orgData;

            return result;
        }
    }
}
