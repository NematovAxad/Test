using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain.Models.FirstSection;
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

namespace AdminHandler.Handlers.Organization
{
    public class SubOrgStatisticsCommandHandler : IRequestHandler<SubOrgStatisticsCommand, SubOrgStatisticsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<SubOrgStatistics, int> _subOrgStatistics;

        public SubOrgStatisticsCommandHandler(IRepository<Organizations, int> organizations, IRepository<SubOrgStatistics, int> subOrgStatistics)
        {
            _organizations = organizations;
            _subOrgStatistics = subOrgStatistics;
        }

        public async Task<SubOrgStatisticsCommandResult> Handle(SubOrgStatisticsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new SubOrgStatisticsCommandResult() { IsSuccess = true };
        }

        public void Add(SubOrgStatisticsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var subOrgStat = _subOrgStatistics.Find(s => s.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (subOrgStat != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            SubOrgStatistics addModel = new SubOrgStatistics()
            {
                OrganizationId = model.OrganizationId,
                CentralManagements = model.CentralManagements,
                TerritorialManagements = model.TerritorialManagements,
                Subordinations = model.Subordinations,
                Others = model.Others
            };

            if(!String.IsNullOrEmpty(model.UserPinfl))
                addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _subOrgStatistics.Add(addModel);
        }
        public void Update(SubOrgStatisticsCommand model)
         {
            var subOrgStat = _subOrgStatistics.Find(s => s.Id == model.Id).FirstOrDefault();
            if (subOrgStat == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organizations.Find(o => o.Id == subOrgStat.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            subOrgStat.CentralManagements = model.CentralManagements;
            subOrgStat.TerritorialManagements = model.TerritorialManagements;
            subOrgStat.Subordinations = model.Subordinations;
            subOrgStat.Others = model.Others;

            if (!String.IsNullOrEmpty(model.UserPinfl))
                subOrgStat.UserPinfl = model.UserPinfl;
            subOrgStat.LastUpdate = DateTime.Now;

            _subOrgStatistics.Update(subOrgStat);
        }
        public void Delete(SubOrgStatisticsCommand model)
        {
            var subOrgStat = _subOrgStatistics.Find(s => s.Id == model.Id).FirstOrDefault();
            if (subOrgStat == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organizations.Find(o => o.Id == subOrgStat.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _subOrgStatistics.Remove(model.Id);
        }
    }
}
