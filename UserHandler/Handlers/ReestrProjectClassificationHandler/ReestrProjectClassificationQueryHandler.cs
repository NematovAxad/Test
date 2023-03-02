using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ReestrProjectClassificationQuery;
using UserHandler.Results.ReestrProjectClassificationResult;
using Domain.States;
using UserHandler.Results.ReestrProjectIdentityResult;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;

namespace UserHandler.Handlers.ReestrProjectClassificationHandler
{
    public class ReestrProjectClassificationQueryHandler : IRequestHandler<ReestrProjectClassificationQuery, ReestrProjectClassificationQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectClassifications, int> _projectClassifications;


        public ReestrProjectClassificationQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectClassifications, int> projectClassifications)
        {
            _organization = organization;
            _deadline = deadline;
            _projectClassifications = projectClassifications;
        }

        public async Task<ReestrProjectClassificationQueryResult> Handle(ReestrProjectClassificationQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectIdentity = _projectClassifications.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).Include(mbox => mbox.Classifications).FirstOrDefault();

            ReestrProjectClassificationQueryResult result = new ReestrProjectClassificationQueryResult();
            result.Classification = projectIdentity;
            return result;
        }
    }
}
