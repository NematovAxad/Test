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
    public class OrgServersQueryHandler : IRequestHandler<OrgServersQuery, OrgServersQueryResult>
    {
        private readonly IRepository<OrganizationServers, int> _orgServers;

        public OrgServersQueryHandler(IRepository<OrganizationServers, int> orgServers)
        {
            _orgServers = orgServers;
        }
        public async Task<OrgServersQueryResult> Handle(OrgServersQuery request, CancellationToken cancellationToken)
        {
            var orgServers = _orgServers.GetAll();
            if (request.Id != 0)
            {
                orgServers = orgServers.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                orgServers = orgServers.Where(s => s.OrganizationId == request.OrganizationId);
            }

            OrgServersQueryResult result = new OrgServersQueryResult();
            result.Count = orgServers.Count();
            result.Data = orgServers.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
