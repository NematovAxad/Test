using Domain.Models;
using Domain.Models.FifthSection;
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
    public class OrgFinanceCommandHandler : IRequestHandler<OrgFinanceCommand, OrgFinanceCommandResult>
    {
        private readonly IRepository<OrgFinance, int> _orgFinance;
        private readonly IRepository<Organizations, int> _organization;

        public OrgFinanceCommandHandler(IRepository<OrgFinance, int> orgFinance, IRepository<Organizations, int> organization)
        {
            _orgFinance = orgFinance;
            _organization = organization;
        }

        public async Task<OrgFinanceCommandResult> Handle(OrgFinanceCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgFinanceCommandResult() { IsSuccess = true };
        }
        public void Add(OrgFinanceCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            
            var orgFinance = _orgFinance.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (orgFinance != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            OrgFinance addModel = new OrgFinance()
            {
                OrganizationId = model.OrganizationId,
                OrgFinanceAmount = model.OrgFinanceAmount,
                OrgItFinancePercent = model.OrgItFinancePercent,
                OrgDigitalizationFinancePercent = model.OrgDigitalizationFinancePercent
            };
            _orgFinance.Add(addModel);
        }
        public void Update(OrgFinanceCommand model)
        {
            var orgFinance = _orgFinance.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (orgFinance == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            var org = _organization.Find(o => o.Id == orgFinance.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            orgFinance.OrgFinanceAmount = model.OrgFinanceAmount;
            orgFinance.OrgItFinancePercent = model.OrgItFinancePercent;
            orgFinance.OrgDigitalizationFinancePercent = model.OrgDigitalizationFinancePercent;
        
            _orgFinance.Update(orgFinance);
        }
        public void Delete(OrgFinanceCommand model)
        {
            var orgFinance = _orgFinance.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (orgFinance == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgFinance.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _orgFinance.Remove(orgFinance);
        }
    }
}
