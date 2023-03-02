using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.States;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ReestrProjectIdentityQuery;
using UserHandler.Results.ReestrProjectIdentityResult;

namespace UserHandler.Handlers.ReestrProjectIdentityHandler
{
    public class ReestrProjectIdentityQueryHandler:IRequestHandler<ReestrProjectIdentityQuery, ReestrProjectIdentityQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectIdentities, int> _projectIdentities;

        public ReestrProjectIdentityQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectIdentities, int> projectIdentities)
        {
            _organization = organization;
            _deadline = deadline;
            _projectIdentities = projectIdentities;
        }
        public async Task<ReestrProjectIdentityQueryResult> Handle(ReestrProjectIdentityQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectIdentity = _projectIdentities.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).Include(mbox=>mbox.Identities).FirstOrDefault();

            ReestrProjectIdentityQueryResult result = new ReestrProjectIdentityQueryResult();
            result.Identities = projectIdentity;
            return result;
        }
    }
}
