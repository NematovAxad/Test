﻿using Domain;
using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
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
using UserHandler.Commands.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class FutureYearsStrategiesCommandHandler : IRequestHandler<FutureYearsStrategiesCommand, FutureYearsStrategiesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;

        private readonly IRepository<OrgFutureYearsStrategies, int> _futureStrategies;

        public FutureYearsStrategiesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrgFutureYearsStrategies, int> futureStrategies)
        {
            _organization = organization;
            _deadline = deadline;
            _futureStrategies = futureStrategies;
        }
        public async Task<FutureYearsStrategiesCommandResult> Handle(FutureYearsStrategiesCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new FutureYearsStrategiesCommandResult() { IsSuccess = true };
        }
        public void Add(FutureYearsStrategiesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var futureStrategies = _futureStrategies.Find(h => h.OrganizationId == model.OrganizationId && h.DocumentNumber == model.DocumentNumber).FirstOrDefault();
            if (futureStrategies != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);
            OrgFutureYearsStrategies addModel = new OrgFutureYearsStrategies()
            {
                OrganizationId = model.OrganizationId,
                DocumentName = model.DocumentName,
                DocumentNumber = model.DocumentNumber,
                ApprovalDate = model.ApprovalDate,
                DocumentPath = model.DocumentPath,
            };
            
            _futureStrategies.Add(addModel);
        }
        public void Update(FutureYearsStrategiesCommand model)
        {

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var futureStrategies = _futureStrategies.Find(h => h.Id == model.Id).FirstOrDefault();
            if (futureStrategies == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == futureStrategies.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            futureStrategies.DocumentName = model.DocumentName;
            futureStrategies.DocumentNumber = model.DocumentNumber;
            futureStrategies.ApprovalDate = model.ApprovalDate;
            futureStrategies.DocumentPath = model.DocumentPath;
            
            _futureStrategies.Update(futureStrategies);
        }
        public void Delete(FutureYearsStrategiesCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var futureStrategies = _futureStrategies.Find(h=>h.Id == model.Id).FirstOrDefault();
            if (futureStrategies == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            var org = _organization.Find(o => o.Id == futureStrategies.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);
            _futureStrategies.Remove(futureStrategies);
        }
    }
}
