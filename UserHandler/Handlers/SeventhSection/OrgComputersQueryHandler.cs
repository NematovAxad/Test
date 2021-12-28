using Domain.Models.SeventhSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SeventhSection;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Handlers.SeventhSection
{
    public class OrgComputersQueryHandler : IRequestHandler<OrgComputersQuery, OrgComputersQueryResult>
    {
        private readonly IRepository<OrganizationComputers, int> _orgComputers;

        public OrgComputersQueryHandler(IRepository<OrganizationComputers, int> orgComputers)
        {
            _orgComputers = orgComputers;
        }

        public async Task<OrgComputersQueryResult> Handle(OrgComputersQuery request, CancellationToken cancellationToken)
        {
            var orgComputers = _orgComputers.GetAll();
            if (request.Id != 0)
            {
                orgComputers = orgComputers.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                orgComputers = orgComputers.Where(s => s.OrganizationId == request.OrganizationId);
            }

            OrgComputersQueryResult result = new OrgComputersQueryResult();
            result.Count = orgComputers.Count();
            result.Data = orgComputers.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
