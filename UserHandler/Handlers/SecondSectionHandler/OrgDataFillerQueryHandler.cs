using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
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
using UserHandler.Queries.SecondSectionQuery;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserHandler.Handlers.SecondSectionHandler
{
    public class OrgDataFillerQueryHandler : IRequestHandler<OrgDataFillerQuery, OrgDataFillerQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrgDataFiller, int> _orgDataFiller;

        public OrgDataFillerQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrgDataFiller, int> orgDataFiller)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDataFiller = orgDataFiller;
        }

        public async Task<OrgDataFillerQueryResult> Handle(OrgDataFillerQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());

            

            var orgDataFiller = _orgDataFiller.GetAll();

            if (request.OrganizationId != 0)
            {
                orgDataFiller = orgDataFiller.Where(s => s.OrganizationId == request.OrganizationId);
            }
            
            OrgDataFillerQueryResult result = new OrgDataFillerQueryResult
            {
                Count = orgDataFiller.Count(),
                Data = orgDataFiller.OrderBy(u => u.Id).ToList<object>()
            };
            return result;
        }
    }
}
