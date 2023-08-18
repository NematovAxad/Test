using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Results.SixthSectionResults;
using Domain.Models.SixthSection;
using UserHandler.Results.ReestrPassportResult;
using Domain.States;
using UserHandler.Commands.ReestrPassportCommands;
using System.Linq;
using Domain.Permission;
using Domain;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrgIndicatorCommandHandler : IRequestHandler<OrgIndicatorsCommand, OrgIndicatorsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationIndicators, int> _orgIndicators;
        
        public OrgIndicatorCommandHandler(IRepository<Organizations, int> organizations, IRepository<Deadline, int> deadline, IRepository<OrganizationIndicators, int> orgIndicators)
        {
            _organization = organizations;
            _deadline = deadline;
            _orgIndicators = orgIndicators;
        }

        public async Task<OrgIndicatorsCommandResult> Handle(OrgIndicatorsCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add:
                    id = Add(request);
                    break;
                case Domain.Enums.EventType.Update:
                    id = Update(request);
                    break;
                case Domain.Enums.EventType.Delete:
                    id = Delete(request);
                    break;
            }
            return new OrgIndicatorsCommandResult() { Id = id, IsSuccess = false };
        }
        public int Add(OrgIndicatorsCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.OperatorDeadlineDate.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.SecondSectionDeadlineDate.ToString());



            var orgIndicator = _orgIndicators.Find(p => p.OrganizationId == model.OrganizationId && p.StartDate == model.StartDate && p.EndDate == model.EndDate).FirstOrDefault();
            if (orgIndicator != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            OrganizationIndicators addModel = new OrganizationIndicators();
            addModel.OrganizationId = model.OrganizationId;
            addModel.StartDate = model.StartDate;
            addModel.EndDate = model.EndDate;
            addModel.FileUploadDate = DateTime.Now;
            addModel.IndicatorReportPath = model.IndicatorReportPath;
            addModel.IndicatorFilePath = model.IndicatorFilePath;
            addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _orgIndicators.Add(addModel);

            return addModel.Id;
        }
        public int Update(OrgIndicatorsCommand model)
        {

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            var orgIndicator = _orgIndicators.Find(p => p.Id == model.Id).FirstOrDefault();
            if (orgIndicator == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            var org = _organization.Find(o => o.Id == orgIndicator.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.OperatorDeadlineDate.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.SecondSectionDeadlineDate.ToString());


            orgIndicator.StartDate = model.StartDate;
            orgIndicator.EndDate = model.EndDate;
            orgIndicator.FileUploadDate = DateTime.Now;
            orgIndicator.IndicatorReportPath = model.IndicatorReportPath;
            orgIndicator.IndicatorFilePath = model.IndicatorFilePath;
            orgIndicator.UserPinfl = model.UserPinfl;
            orgIndicator.LastUpdate = DateTime.Now;

            _orgIndicators.Update(orgIndicator);

            return orgIndicator.Id;
        }

        public int Delete(OrgIndicatorsCommand model)
        {
            var orgIndicator = _orgIndicators.Find(p => p.Id == model.Id).FirstOrDefault();
            if (orgIndicator == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            var org = _organization.Find(o => o.Id == orgIndicator.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.OperatorDeadlineDate.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.SecondSectionDeadlineDate.ToString());

            
            _orgIndicators.Remove(orgIndicator);

            return orgIndicator.Id;
        }
    }
}
