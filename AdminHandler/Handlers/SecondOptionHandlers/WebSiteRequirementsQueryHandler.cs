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
    public class WebSiteRequirementsQueryHandler : IRequestHandler<WebSiteRequirementsQuery, WebSiteRequirementsQueryResult>
    {
        private readonly IRepository<WebSiteRequirements, int> _webSiteRequirements;
        private readonly IRepository<Deadline, int> _deadline;
        public WebSiteRequirementsQueryHandler(IRepository<WebSiteRequirements, int> webSiteRequirements, IRepository<Deadline, int> deadline)
        {
            _webSiteRequirements = webSiteRequirements;
            _deadline = deadline;
        }
        public async Task<WebSiteRequirementsQueryResult> Handle(WebSiteRequirementsQuery request, CancellationToken cancellationToken)
        {
            var requirements = _webSiteRequirements.Find(r=>r.OrganizationId == request.OrganizationId).ToList();
            
            WebSiteRequirementsQueryResult result = new WebSiteRequirementsQueryResult();
            result.Count = requirements.Count();
            result.Data = requirements.OrderBy(u => u.Number).ToList<object>();
            return result;
        }
    }
}
