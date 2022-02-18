using Domain.Models.FifthSection;
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
    public class OrgProcessesQueryHandler : IRequestHandler<OrgProcessesQuery, OrgProcessesQueryResult>
    {
        private readonly IRepository<OrgProcesses, int> _orgProcesses;

        public OrgProcessesQueryHandler(IRepository<OrgProcesses, int> orgProcesses)
        {
            _orgProcesses = orgProcesses;
        }

        public async Task<OrgProcessesQueryResult> Handle(OrgProcessesQuery request, CancellationToken cancellationToken)
        {
            var orgProcesses = _orgProcesses.GetAll();
            if (request.Id != 0)
            {
                orgProcesses = orgProcesses.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                orgProcesses = orgProcesses.Where(s => s.OrganizationId == request.OrganizationId);
            }

            OrgProcessesQueryResult result = new OrgProcessesQueryResult();
            result.Count = orgProcesses.Count();
            result.Data = orgProcesses.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
