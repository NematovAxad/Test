using AdminHandler.Commands.SecondOptionCommands;
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
    public class OrgSocialSitesCommandHandler : IRequestHandler<OrgSocialSitesCommand, OrgSocialSitesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationSocialSites, int> _orgSocialSites;

        public OrgSocialSitesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationSocialSites, int> orgSocialSites)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgSocialSites = orgSocialSites;
        }
        public async Task<OrgSocialSitesCommandResult> Handle(OrgSocialSitesCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgSocialSitesCommandResult() { IsSuccess = true };
        }
        public void Add(OrgSocialSitesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var field = _field.Find(f => f.Id == model.FieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound(model.FieldId.ToString());
            var deadline = _deadline.Find(d => d.Id == model.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(model.DeadlineId.ToString());
            var socialSite = _orgSocialSites.Find(s => s.OrganizationId == model.OrganizationId && s.DeadlineId == model.DeadlineId && s.FieldId == model.FieldId && s.SocialSiteLink == model.SocialSiteLink).FirstOrDefault();
            if (socialSite != null)
                throw ErrorStates.NotAllowed(model.SocialSiteLink);

            OrganizationSocialSites addModel = new OrganizationSocialSites()
            {
                OrganizationId = model.OrganizationId,
                FieldId = model.FieldId,
                DeadlineId = model.DeadlineId,
                SocialSiteLink = model.SocialSiteLink
            };
            _orgSocialSites.Add(addModel);
        }
        public void Update(OrgSocialSitesCommand model)
        {
            var socialSites = _orgSocialSites.Find(s => s.Id == model.Id).FirstOrDefault();
            if (socialSites == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            socialSites.SocialSiteLink = model.SocialSiteLink;

            _orgSocialSites.Update(socialSites);
        }
        public void Delete(OrgSocialSitesCommand model)
        {
            var socialSites = _orgSocialSites.Find(s => s.Id == model.Id).FirstOrDefault();
            if (socialSites == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _orgSocialSites.Remove(socialSites);
        }
    }
}
