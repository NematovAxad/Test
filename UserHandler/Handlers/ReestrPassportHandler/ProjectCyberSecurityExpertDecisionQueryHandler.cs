using Domain.Models.SecondSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ReestrPassportQuery;
using UserHandler.Results.ReestrPassportResult;
using Domain.States;
using System.Linq;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ProjectCyberSecurityExpertDecisionQueryHandler : IRequestHandler<ProjectCyberSecurityExpertDecisionQuery, ProjectCyberSecurityExpertDecisionQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectCyberSecurityExpertDecision, int> _projectCyberSecurityExpertDecision;

        public ProjectCyberSecurityExpertDecisionQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectCyberSecurityExpertDecision, int> projectCyberSecurityExpertDecision)
        {
            _organization = organization;
            _deadline = deadline;
            _projectCyberSecurityExpertDecision = projectCyberSecurityExpertDecision;
        }

        public async Task<ProjectCyberSecurityExpertDecisionQueryResult> Handle(ProjectCyberSecurityExpertDecisionQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectPosition = _projectCyberSecurityExpertDecision.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).FirstOrDefault();

            ProjectCyberSecurityExpertDecisionQueryResult result = new ProjectCyberSecurityExpertDecisionQueryResult();
            result.ReestrProjectCyberSecurityExpertDecision = projectPosition;
            return result;
        }
    }
}
