using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
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

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class OrgSocialParametersCommandHandler : IRequestHandler<OrgSocialParametersCommand, OrgSocialParametersCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationSocials, int> _orgSocials;
        private readonly IRepository<OrganizationSocialParameters, int> _orgSocialParameters;

        public OrgSocialParametersCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationSocials, int> orgSocials, IRepository<OrganizationSocialParameters, int> orgSocialParameters)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgSocials = orgSocials;
            _orgSocialParameters = orgSocialParameters;
        }
        public async Task<OrgSocialParametersCommandResult> Handle(OrgSocialParametersCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgSocialParametersCommandResult() { IsSuccess = true };
        }
        public void Add(OrgSocialParametersCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            var socialParameter = _orgSocialParameters.Find(s => s.OrganizationId == model.OrganizationId && s.DeadlineId == deadline.Id).FirstOrDefault();
            if (socialParameter != null)
                throw ErrorStates.NotAllowed(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            OrganizationSocialParameters addModel = new OrganizationSocialParameters();
            addModel.OrganizationId = model.OrganizationId;
            addModel.DeadlineId = deadline.Id;
            if (model.OrgFullName != null)
            {
                addModel.OrgFullName = model.OrgFullName;
            }
            if (model.OrgLegalSite != null)
            {
                addModel.OrgLegalSite = model.OrgLegalSite;
            }
            if (model.OrgPhone != null)
            {
                addModel.OrgPhone = model.OrgPhone;
            }
            if (model.OrgLegalAddress != null)
            {
                addModel.OrgLegalAddress = model.OrgLegalAddress;
            }
            if (model.OrgEmail != null)
            {
                addModel.OrgEmail = model.OrgEmail;
            }
            if (model.LinksToOtherSocials != null)
            {
                addModel.LinksToOtherSocials = model.LinksToOtherSocials;
            }
            if (model.SyncronizedPosts != null)
            {
                addModel.SyncronizedPosts = model.SyncronizedPosts;
            }
            if (model.Pool != null)
            {
                addModel.Pool = model.Pool;
            }
            _orgSocialParameters.Add(addModel);
        }
        public void Update(OrgSocialParametersCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var socialParameter = _orgSocialParameters.Find(s => s.Id == model.Id).FirstOrDefault();
            if (socialParameter == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            if (model.OrgFullName != null)
            {
                socialParameter.OrgFullName = model.OrgFullName;
            }
            if (model.OrgLegalSite != null)
            {
                socialParameter.OrgLegalSite = model.OrgLegalSite;
            }
            if (model.OrgPhone != null)
            {
                socialParameter.OrgPhone = model.OrgPhone;
            }
            if (model.OrgLegalAddress != null)
            {
                socialParameter.OrgLegalAddress = model.OrgLegalAddress;
            }
            if (model.OrgEmail != null)
            {
                socialParameter.OrgEmail = model.OrgEmail;
            }
            if (model.LinksToOtherSocials != null)
            {
                socialParameter.LinksToOtherSocials = model.LinksToOtherSocials;
            }
            if (model.SyncronizedPosts != null)
            {
                socialParameter.SyncronizedPosts = model.SyncronizedPosts;
            }
            if (model.Pool != null)
            {
                socialParameter.Pool = model.Pool;
            }
            _orgSocialParameters.Update(socialParameter);
        }
        public void Delete(OrgSocialParametersCommand model)
        {
            var socialParameter = _orgSocialParameters.Find(s => s.Id == model.Id).FirstOrDefault();
            if (socialParameter == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organization.Find(o => o.Id == socialParameter.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            _orgSocialParameters.Remove(socialParameter);
        }
    }
}
