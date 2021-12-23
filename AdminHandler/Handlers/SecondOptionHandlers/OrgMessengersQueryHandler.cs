using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrgMessengersQueryHandler : IRequestHandler<OrgMessengersQuery, OrgMessengersQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationMessengers, int> _organizationMessengers;

        public OrgMessengersQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationMessengers, int> organizationMessengers)
        {
            _organization = organization;
            _deadline = deadline;
            _organizationMessengers = organizationMessengers;
        }
        public async Task<OrgMessengersQueryResult> Handle(OrgMessengersQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());
           
            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(request.DeadlineId.ToString());

            var messengers = _organizationMessengers.Find(m => m.OrganizationId == request.OrganizationId && m.DeadlineId == request.DeadlineId).ToList();

            if(request.Id!=0)
            {
                messengers = messengers.Where(m => m.Id == request.Id).ToList();
            }
            
            OrgMessengersQueryResult result = new OrgMessengersQueryResult();
            result.Count = messengers.Count();
            result.Data = messengers.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
