﻿using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrgSocialQueryHandler : IRequestHandler<OrgSocialQuery, OrgSocialQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationSocials, int> _orgSocials;
        private readonly IRepository<OrganizationSocialParameters, int> _orgSocialParameters;

        public OrgSocialQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationSocials, int> orgSocials, IRepository<OrganizationSocialParameters, int> orgSocialParameters)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgSocials = orgSocials;
            _orgSocialParameters = orgSocialParameters;
        }
        public async Task<OrgSocialQueryResult> Handle(OrgSocialQuery request, CancellationToken cancellationToken)
        {
            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            var socials = _orgSocials.Find(s => s.OrganizationId == request.OrganizationId).ToList();

            OrgSocialQueryResult result = new OrgSocialQueryResult();
            result.Count = socials.Count();
            result.Socials = socials.OrderBy(u => u.Id).ToList<OrganizationSocials>();
            return result;
        }
    }
}
