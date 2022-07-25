using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using Domain.Models.Organization;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class PingQueryHandler : IRequestHandler<PingQuery, PingQueryResult>
    {
        private readonly IRepository<Organizations, int> _org;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<WebSiteAvailability, int> _webSiteAvailability;
        private readonly IRepository<SiteFails, int> _siteFails;


        public PingQueryHandler(IRepository<Organizations, int> org, IRepository<Deadline, int> deadline, IRepository<WebSiteAvailability, int> webSiteAvailability, IRepository<SiteFails, int> siteFails)
        {
            _org = org;
            _deadline = deadline;
            _webSiteAvailability = webSiteAvailability;
            _siteFails = siteFails;

        }
        public async Task<PingQueryResult> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            var org = _org.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound("organization " + request.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline " + request.DeadlineId.ToString());

            var fails = _siteFails.Find(f => f.DeadlineId == deadline.Id && f.OrganizationId == org.Id).ToList();
            var ping = _webSiteAvailability.GetAll();
            if (request.OrganizationId != 0)
                ping = ping.Where(m => m.OrganizationId == request.OrganizationId);
            if (request.DeadlineId != 0)
                ping = ping.Where(m => m.DeadlineId == request.DeadlineId);

            PingQueryResult result = new PingQueryResult();
            result.Count = ping.Count();
            result.Data = ping.OrderBy(u => u.Id).ToList<object>();
            result.Fails = fails.OrderBy(f => f.FailedTime).ToList<object>();
            return result;
        }
    }
}
