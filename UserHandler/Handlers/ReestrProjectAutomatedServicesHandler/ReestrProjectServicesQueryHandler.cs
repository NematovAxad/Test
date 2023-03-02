using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ReestrProjectAutomatedServicesQuery;
using UserHandler.Results.ReestrProjectAutomatedServicesResult;
using Domain.States;
using UserHandler.Results.ReestrProjectIdentityResult;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;

namespace UserHandler.Handlers.ReestrProjectAutomatedServicesHandler
{
    public class ReestrProjectServicesQueryHandler : IRequestHandler<ReestrProjectServicesQuery, ReestrProjectServicesQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectAutomatedServices, int> _projectServices;

        public ReestrProjectServicesQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectAutomatedServices, int> projectServices)
        {
            _organization = organization;
            _deadline = deadline;
            _projectServices = projectServices;
        }

        public async Task<ReestrProjectServicesQueryResult> Handle(ReestrProjectServicesQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);
            var projectServices = _projectServices.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).Include(mbox => mbox.AutomatedServices).Include(mbox=>mbox.AutomatedFunctions).FirstOrDefault();

            ReestrProjectServicesQueryResult result = new ReestrProjectServicesQueryResult();
            result.ProjectServices = projectServices;
            return result;
        }
    }
}
