﻿using Domain.Models;
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
    public class HelplineInfoCommandHandler : IRequestHandler<HelplineInfoCommand, HelplineInfoCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<HelplineInfo, int> _helplineInfo;

        public HelplineInfoCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<HelplineInfo, int> helplineInfo)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _helplineInfo = helplineInfo;
        }

        public async Task<HelplineInfoCommandResult> Handle(HelplineInfoCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new HelplineInfoCommandResult() { IsSuccess = true };
        }
        public void Add(HelplineInfoCommand model)
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
            var helplineInfo = _helplineInfo.Find(h => h.OrganizationId == model.OrganizationId && h.DeadlineId == model.DeadlineId).FirstOrDefault();
            if (helplineInfo != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.Id) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            HelplineInfo addModel = new HelplineInfo()
            {
                OrganizationId = model.OrganizationId,
                FieldId = model.FieldId,
                DeadlineId = model.DeadlineId,
                HelplineNumber = model.HelplineNumber,
                OfficialSiteHasHelpline = model.OfficialSiteHasHelpline,
                CanGiveFeedbackToHelpline = model.CanGiveFeedbackToHelpline,
                OfficialSiteHasHelplinefeedback = model.OfficialSiteHasHelplinefeedback
            };
            _helplineInfo.Add(addModel);
        }
        public void Update(HelplineInfoCommand model)
        {
            var helplineInfo = _helplineInfo.Find(h => h.Id == model.Id).FirstOrDefault();
            if (helplineInfo == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == helplineInfo.OrganizationId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            helplineInfo.HelplineNumber = model.HelplineNumber;
            helplineInfo.OfficialSiteHasHelpline = model.OfficialSiteHasHelpline;
            helplineInfo.CanGiveFeedbackToHelpline = model.CanGiveFeedbackToHelpline;
            helplineInfo.OfficialSiteHasHelplinefeedback = model.OfficialSiteHasHelplinefeedback;

            _helplineInfo.Update(helplineInfo);
        }
        public void Delete(HelplineInfoCommand model)
        {
            var helplineInfo = _helplineInfo.Find(h => h.Id == model.Id).FirstOrDefault();
            if (helplineInfo == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == helplineInfo.OrganizationId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _helplineInfo.Remove(helplineInfo);
        }
    }
}