using Domain.Models.FirstSection;
using Domain.Models;
using Domain.Models.SixthSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;
using System.Linq;
using Domain.States;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrganizationDigitalEconomyProjectsQueryHandler : IRequestHandler<OrganizationDigitalEconomyProjectsQuery, OrganizationDigitalEconomyProjectsQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationDigitalEconomyProjects, int> _orgDigitalEconomyProjects;

        public OrganizationDigitalEconomyProjectsQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationDigitalEconomyProjects, int> orgDigitalEconomyProjects)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDigitalEconomyProjects = orgDigitalEconomyProjects;
        }

        public async Task<OrganizationDigitalEconomyProjectsQueryResult> Handle(OrganizationDigitalEconomyProjectsQuery request, CancellationToken cancellationToken)
        {
           
            var digitalEconomyProjects = _orgDigitalEconomyProjects.Find(p => p.OrganizationId == request.OrganizationId).ToList();

            OrganizationDigitalEconomyProjectsQueryResult result = new OrganizationDigitalEconomyProjectsQueryResult();

            result.Projects = digitalEconomyProjects;
            result.ProjectsCount = digitalEconomyProjects.Count();

            return result;
        }
    }
}
