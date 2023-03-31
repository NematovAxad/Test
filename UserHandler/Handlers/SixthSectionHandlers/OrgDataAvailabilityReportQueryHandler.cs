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
using Domain.States;
using System.Linq;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrgDataAvailabilityReportQueryHandler : IRequestHandler<OrgDataAvailabilityReportQuery, OrgDataAvailabilityReportQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationDataAvailability, int> _orgDataAvailability;


        public OrgDataAvailabilityReportQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationDataAvailability, int> orgDataAvailability)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDataAvailability = orgDataAvailability;
        }

        public async Task<OrgDataAvailabilityReportQueryResult> Handle(OrgDataAvailabilityReportQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrgId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            var orgData = _orgDataAvailability.Find(d => d.OrganizationId == request.OrgId && d.DeadlineId == deadline.Id).ToList();

            OrgDataAvailabilityReportQueryResult result = new OrgDataAvailabilityReportQueryResult();

            result.OrganizationId = request.OrgId;
            result.AllCheckedSection = orgData.Count();
            result.FullyProvidedSections = orgData.Where(d => d.DataAvailability == Domain.Enums.DataAvailability.FullyProvided).Count();
            result.PartiallyProvidedSections = orgData.Where(d => d.DataAvailability == Domain.Enums.DataAvailability.PartiallyProvided).Count();
            result.NotProvidedSections = orgData.Where(d => d.DataAvailability == Domain.Enums.DataAvailability.NotProvided).Count();
            result.RelevantDataSections = orgData.Where(d => d.DataRelevance == true).Count();

            return result;
        }
    }
}
