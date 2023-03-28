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
    public class OrgHelplineQueryHandler : IRequestHandler<OrgHelplineQuery, OrgHelplineQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrgHelpline, int> _orgHelpline;

        public OrgHelplineQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrgHelpline, int> orgHelpline)
        {
            _organization = organization;
            _deadline = deadline;
            _orgHelpline = orgHelpline;
        }

        public async Task<OrgHelplineQueryResult> Handle(OrgHelplineQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());

           

            var helpLine = _orgHelpline.GetAll();

            if (request.OrganizationId != 0)
            {
                helpLine = helpLine.Where(s => s.OrganizationId == request.OrganizationId);
            }
            
            OrgHelplineQueryResult result = new OrgHelplineQueryResult
            {
                Count = helpLine.Count(),
                Data = helpLine.OrderBy(u => u.Id).ToList<object>()
            };
            return result;
        }
    }
}
