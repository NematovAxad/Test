using Domain.Models;
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
    public class HelplineInfoQueryHandler : IRequestHandler<HelplineInfoQuery, HelplineInfoQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<HelplineInfo, int> _helplineInfo;

        public HelplineInfoQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<HelplineInfo, int> helplineInfo)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _helplineInfo = helplineInfo;
        }

        public async Task<HelplineInfoQueryResult> Handle(HelplineInfoQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            

            var helpLine = _helplineInfo.Find(h=>h.OrganizationId == request.OrganizationId).FirstOrDefault();

            HelplineInfoQueryResult result = new HelplineInfoQueryResult();
            result.Data = helpLine;
            return result;
        }
    }
}
