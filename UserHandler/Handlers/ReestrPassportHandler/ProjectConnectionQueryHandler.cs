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
using Microsoft.EntityFrameworkCore;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ProjectConnectionQueryHandler : IRequestHandler<ProjectConnectionQuery, ProjectConnectionQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectConnection, int> _projectConnection;

        public ProjectConnectionQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectConnection, int> projectConnection)
        {
            _organization = organization;
            _deadline = deadline;
            _projectConnection = projectConnection;
        }
        public async Task<ProjectConnectionQueryResult> Handle(ProjectConnectionQuery request, CancellationToken cancellationToken)
        {
            if (request.OrgId == 0 || request.ReestrProjectId == 0)
                throw ErrorStates.NotEntered("id not entered");
            var projectPosition = _projectConnection.Find(p => p.OrganizationId == request.OrgId && p.ReestrProjectId == request.ReestrProjectId).Include(mbox=>mbox.ProjectConnections).FirstOrDefault();

            

            ProjectConnectionQueryResult result = new ProjectConnectionQueryResult();
            result.ProjectConnection = projectPosition;
            return result;
        }
    }
}
