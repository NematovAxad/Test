using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models.SecondSection;
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

        public WebSiteRequirementsQueryHandler(IRepository<WebSiteRequirements, int> webSiteRequirements)
        {
            _webSiteRequirements = webSiteRequirements;
        }
        public async Task<WebSiteRequirementsQueryResult> Handle(WebSiteRequirementsQuery request, CancellationToken cancellationToken)
        {
            var manager = _webSiteRequirements.GetAll();
            if (request.Id != 0)
                manager = manager.Where(m => m.Id == request.Id);
            if (request.OrganizationId != 0)
                manager = manager.Where(m => m.OrganizationId == request.OrganizationId);

            WebSiteRequirementsQueryResult result = new WebSiteRequirementsQueryResult();
            result.Count = manager.Count();
            result.Data = manager.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
