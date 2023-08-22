using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrgSocialQueryHandler : IRequestHandler<OrgSocialQuery, OrgSocialQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationSocials, int> _orgSocials;
      

        public OrgSocialQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationSocials, int> orgSocials)
        {
            _organization = organization;
            _deadline = deadline;
            _orgSocials = orgSocials;
           
        }
        public async Task<OrgSocialQueryResult> Handle(OrgSocialQuery request, CancellationToken cancellationToken)
        {
            OrgSocialQueryResult result = new OrgSocialQueryResult();

            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            var socials = _orgSocials.Find(s => s.OrganizationId == request.OrganizationId).ToList();

            if(socials.Any())
            {
                if (socials.Any(s => s.Pool == true && s.PoolExpert == true))
                    result.IsPoolExceptExpert = true;
            }

            
            result.Count = socials.Count();
            result.Socials = socials.OrderBy(u => u.Id).ToList<OrganizationSocials>();
            return result;
        }
    }
}
