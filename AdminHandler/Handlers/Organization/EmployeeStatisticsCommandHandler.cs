using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain.Models;
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
    public class EmployeeStatisticsCommandHandler : IRequestHandler<EmployeeStatisticsCommand, EmployeeStatisticsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<EmployeeStatistics, int> _employeeStatistics;

        public EmployeeStatisticsCommandHandler(IRepository<Organizations, int> organizations, IRepository<EmployeeStatistics, int> employeeStatistics)
        {
            _organizations = organizations;
            _employeeStatistics = employeeStatistics;
        }

        public async Task<EmployeeStatisticsCommandResult> Handle(EmployeeStatisticsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new EmployeeStatisticsCommandResult() { IsSuccess = true };
        }
        public void Add(EmployeeStatisticsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var employeeStat = _employeeStatistics.Find(e => e.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (employeeStat != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            EmployeeStatistics addModel = new EmployeeStatistics()
            {
                OrganizationId = model.OrganizationId,
                CentralManagementPositions = model.CentralManagementPositions,
                CentralManagementEmployees = model.CentralManagementEmployees,
                TerritorialManagementPositions = model.TerritorialManagementPositions,
                TerritorialManagementEmployees = model.TerritorialManagementEmployees,
                SubordinationPositions = model.SubordinationPositions,
                SubordinationEmployees = model.SubordinationEmployees,
                OtherPositions = model.OtherPositions,
                OtherEmployees = model.OtherEmployees,
                HeadPositions = model.HeadPositions,
                HeadEmployees = model.HeadEmployees,
                DepartmentHeadPositions = model.DepartmentHeadPositions,
                DepartmentHeadEmployees = model.DepartmentHeadEmployees,
                SpecialistsPosition = model.SpecialistsPosition,
                SpecialistsEmployee = model.SpecialistsEmployee,
                ProductionPersonnelsPosition = model.ProductionPersonnelsPosition,
                ProductionPersonnelsEmployee = model.ProductionPersonnelsEmployee,
                TechnicalStuffPositions = model.TechnicalStuffPositions,
                TechnicalStuffEmployee = model.TechnicalStuffEmployee,
                ServiceStuffPositions = model.ServiceStuffPositions,
                ServiceStuffEmployee = model.ServiceStuffEmployee
            };
            _employeeStatistics.Add(addModel);
        }
        public void Update(EmployeeStatisticsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var employeeStat = _employeeStatistics.Find(e =>e.Id == model.Id && e.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (employeeStat == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            employeeStat.CentralManagementPositions = model.CentralManagementPositions;
            employeeStat.CentralManagementEmployees = model.CentralManagementEmployees;
            employeeStat.TerritorialManagementPositions = model.TerritorialManagementPositions;
            employeeStat.TerritorialManagementEmployees = model.TerritorialManagementEmployees;
            employeeStat.SubordinationPositions = model.SubordinationPositions;
            employeeStat.SubordinationEmployees = model.SubordinationEmployees;
            employeeStat.OtherPositions = model.OtherPositions;
            employeeStat.OtherEmployees = model.OtherEmployees;
            employeeStat.HeadPositions = model.HeadPositions;
            employeeStat.HeadEmployees = model.HeadEmployees;
            employeeStat.DepartmentHeadPositions = model.DepartmentHeadPositions;
            employeeStat.DepartmentHeadEmployees = model.DepartmentHeadEmployees;
            employeeStat.SpecialistsPosition = model.SpecialistsPosition;
            employeeStat.SpecialistsEmployee = model.SpecialistsEmployee;
            employeeStat.ProductionPersonnelsPosition = model.ProductionPersonnelsPosition;
            employeeStat.ProductionPersonnelsEmployee = model.ProductionPersonnelsEmployee;
            employeeStat.TechnicalStuffPositions = model.TechnicalStuffPositions;
            employeeStat.TechnicalStuffEmployee = model.TechnicalStuffEmployee;
            employeeStat.ServiceStuffPositions = model.ServiceStuffPositions;
            employeeStat.ServiceStuffEmployee = model.ServiceStuffEmployee;
            _employeeStatistics.Update(employeeStat);
        }
        public void Delete(EmployeeStatisticsCommand model)
        {

            var subOrgStat = _employeeStatistics.Find(s => s.Id == model.Id).FirstOrDefault();
            if (subOrgStat == null)
                throw ErrorStates.NotAllowed(model.Id.ToString());
            var org = _organizations.Find(o => o.Id == subOrgStat.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _employeeStatistics.Remove(model.Id);
        }
    }
}
