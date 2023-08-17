using Domain;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.Models.SecondSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SecondSectionCommand;
using UserHandler.Results.SecondSectionCommandResult;

namespace UserHandler.Handlers.SecondSectionHandler
{
    public class OrgHelplineCommandHandler : IRequestHandler<OrgHelplineCommand, OrgHelplineCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrgHelpline, int> _orgHelpline;

        public OrgHelplineCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrgHelpline, int> orgHelpline)
        {
            _organization = organization;
            _deadline = deadline;
            _orgHelpline = orgHelpline;
        }

        public async Task<OrgHelplineCommandResult> Handle(OrgHelplineCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgHelplineCommandResult() { IsSuccess = true };
        }
        public void Add(OrgHelplineCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var orgHelpline = _orgHelpline.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (orgHelpline != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            OrgHelpline addModel = new OrgHelpline()
            {
                OrganizationId = model.OrganizationId,
                HelplineNumber = model.HelplineNumber
            };

            addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _orgHelpline.Add(addModel);
        }
        public void Update(OrgHelplineCommand model)
        {
            var orgHelpline = _orgHelpline.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgHelpline == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgHelpline.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            orgHelpline.HelplineNumber = model.HelplineNumber;
            orgHelpline.UserPinfl = model.UserPinfl;
            orgHelpline.LastUpdate = DateTime.Now;

            _orgHelpline.Update(orgHelpline);
        }
        public void Delete(OrgHelplineCommand model)
        {
            var orgHelpline = _orgHelpline.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgHelpline == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgHelpline.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);
            _orgHelpline.Remove(orgHelpline);
        }
    }
}
