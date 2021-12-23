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
    public class OrgHelplineCommandHandler : IRequestHandler<OrgHelplineCommand, OrgHelplineCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrgHelpline, int> _orgHelpline;

        public OrgHelplineCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrgHelpline, int> orgHelpline)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
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
            var field = _field.Find(f => f.Id == model.FieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound(model.FieldId.ToString());
            var deadline = _deadline.Find(d => d.Id == model.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(model.DeadlineId.ToString());
            var orgHelpline = _orgHelpline.Find(h => h.OrganizationId == model.OrganizationId && h.DeadlineId == model.DeadlineId).FirstOrDefault();
            if (orgHelpline != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.Id) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            OrgHelpline addModel = new OrgHelpline()
            {
                OrganizationId = model.OrganizationId,
                FieldId = model.FieldId,
                DeadlineId = model.DeadlineId,
                HasOnlineConsultant = model.HasOnlineConsultant,
                OperatesInWorkingDay = model.OperatesInWorkingDay,
                AcceptableResponseTime = model.AcceptableResponseTime
            };
            _orgHelpline.Add(addModel);
        }
        public void Update(OrgHelplineCommand model)
        {
            var orgHelpline = _orgHelpline.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgHelpline == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == orgHelpline.OrganizationId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            orgHelpline.HasOnlineConsultant = model.HasOnlineConsultant;
            orgHelpline.OperatesInWorkingDay = model.OperatesInWorkingDay;
            orgHelpline.AcceptableResponseTime = model.AcceptableResponseTime;

            _orgHelpline.Update(orgHelpline);
        }
        public void Delete(OrgHelplineCommand model)
        {
            var orgHelpline = _orgHelpline.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgHelpline == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == orgHelpline.OrganizationId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _orgHelpline.Remove(orgHelpline);
        }
    }
}