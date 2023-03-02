using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UserHandler.Queries.ReestrProjectEfficiencyQuery;
using UserHandler.Results.ReestrProjectEfficiencyResult;
using MainInfrastructures.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;

namespace UserHandler.Handlers.ReestrProjectEfficiencyHandler
{
    public class ReestrProjectEfficiencyQueryResultHandler:IRequestHandler<ReestrProjectEfficiencyQuery, ReestrProjectEfficiencyQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectEfficiency, int> _projectEfficiency;


        public ReestrProjectEfficiencyQueryResultHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectEfficiency, int> projectEfficiency)
        {
            _organization = organization;
            _deadline = deadline;
            _projectEfficiency = projectEfficiency;
        }

        public async Task<ReestrProjectEfficiencyQueryResult> Handle(ReestrProjectEfficiencyQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectEfficiency = _projectEfficiency.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).Include(mbox => mbox.Efficiencies).FirstOrDefault();

            ReestrProjectEfficiencyQueryResult result = new ReestrProjectEfficiencyQueryResult();
            result.ProjectEfficiency = projectEfficiency;
            return result;
        }
    }
}
