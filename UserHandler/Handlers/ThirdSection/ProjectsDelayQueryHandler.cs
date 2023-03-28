using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class ProjectsDelayQueryHandler : IRequestHandler<ProjectsDelayQuery, ProjectsDelayQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<DelaysOnProjects, int> _delaysOnProjects;

        public ProjectsDelayQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<DelaysOnProjects, int> delaysOnProjects)
        {
            _organization = organization;
            _deadline = deadline;
            _delaysOnProjects = delaysOnProjects;
        }
        public async Task<ProjectsDelayQueryResult> Handle(ProjectsDelayQuery request, CancellationToken cancellationToken)
        {
            var services = _delaysOnProjects.GetAll();
            if (request.Id != 0)
            {
                services = services.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                services = services.Where(s => s.OrganizationId == request.OrganizationId);
            }

            ProjectsDelayQueryResult result = new ProjectsDelayQueryResult();
            result.Count = services.Count();
            result.Data = services.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
