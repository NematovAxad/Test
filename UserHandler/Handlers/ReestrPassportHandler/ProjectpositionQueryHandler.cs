using Domain.Models;
using Domain.Models.SecondSection;
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
    public class ProjectpositionQueryHandler : IRequestHandler<ProjectPositionQuery, ProjectPositionQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectPosition, int> _projectPosition;

        public ProjectpositionQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectPosition, int> projectPosition)
        {
            _organization = organization;
            _deadline = deadline;
            _projectPosition = projectPosition;
        }
        public async Task<ProjectPositionQueryResult> Handle(ProjectPositionQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectPosition = _projectPosition.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).FirstOrDefault();

            ProjectPositionQueryResult result = new ProjectPositionQueryResult();
            result.ProjectPosition = projectPosition;
            return result;
        }
    }
}
