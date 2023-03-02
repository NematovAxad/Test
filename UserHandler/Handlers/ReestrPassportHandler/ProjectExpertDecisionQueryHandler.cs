using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ReestrPassportQuery;
using UserHandler.Results.ReestrPassportResult;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ProjectExpertDecisionQueryHandler : IRequestHandler<ProjectExpertDecisionQuery, ProjectExpertDecisionQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectExpertDecision, int> _projectExpertDecision;

        public ProjectExpertDecisionQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectExpertDecision, int> projectExpertDecision)
        {
            _organization = organization;
            _deadline = deadline;
            _projectExpertDecision = projectExpertDecision;
        }
        public async Task<ProjectExpertDecisionQueryResult> Handle(ProjectExpertDecisionQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectPosition = _projectExpertDecision.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).FirstOrDefault();

            ProjectExpertDecisionQueryResult result = new ProjectExpertDecisionQueryResult();
            result.ReestrProjectExpertDecision = projectPosition;
            return result;
        }
    }
}
