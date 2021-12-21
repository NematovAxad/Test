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
    public class OrgSocialSitesQueryHandler : IRequestHandler<OrgSocialSitesQuery, OrgSocialSitesQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationSocialSites, int> _orgSocialSites;

        public OrgSocialSitesQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationSocialSites, int> orgSocialSites)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgSocialSites = orgSocialSites;
        }
        public async Task<OrgSocialSitesQueryResult> Handle(OrgSocialSitesQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());
            
            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(request.DeadlineId.ToString());
            var sites = _orgSocialSites.GetAll();

            if(request.OrganizationId!=0)
            {
                sites = sites.Where(s => s.OrganizationId == request.OrganizationId);
            }
            if (request.DeadlineId != 0)
            {
                sites = sites.Where(s => s.DeadlineId == request.DeadlineId);
            }

            OrgSocialSitesQueryResult result = new OrgSocialSitesQueryResult();
            result.Count = sites.Count();
            result.Data = sites.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
