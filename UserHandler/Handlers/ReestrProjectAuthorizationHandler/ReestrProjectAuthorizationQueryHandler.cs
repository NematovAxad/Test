using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ReestrProjectAuthorizationQuery;
using UserHandler.Results.ReestrProjectAuthorizationResult;
using Domain.States;
using UserHandler.Results.ReestrProjectClassificationResult;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;

namespace UserHandler.Handlers.ReestrProjectAuthorizationHandler
{
    public class ReestrProjectAuthorizationQueryHandler : IRequestHandler<ReestrProjectAuthorizationQuery, ReestrProjectAuthorizationQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectAuthorizations, int> _projectAuthorizations;

        public ReestrProjectAuthorizationQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectAuthorizations, int> projectAuthorizations)
        {
            _organization = organization;
            _deadline = deadline;
            _projectAuthorizations = projectAuthorizations;
        }

        public async Task<ReestrProjectAuthorizationQueryResult> Handle(ReestrProjectAuthorizationQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectAuthorization = _projectAuthorizations.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).Include(mbox => mbox.Authorizations).FirstOrDefault();

            ReestrProjectAuthorizationQueryResult result = new ReestrProjectAuthorizationQueryResult();
            result.Authorizations = projectAuthorization;
            return result;
        }
    }
}
