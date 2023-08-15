using AdminHandler.Results.Organization;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.Organization
{
    public class EmployeeStatisticsCommand:IRequest<EmployeeStatisticsCommandResult>

    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string UserPinfl { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserOrgId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<string> UserPermissions { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int CentralManagementPositions { get; set; }
        public int CentralManagementEmployees { get; set; }
        public int TerritorialManagementPositions { get; set; }
        public int TerritorialManagementEmployees { get; set; }
        public int SubordinationPositions { get; set; }
        public int SubordinationEmployees { get; set; }
        public int OtherPositions { get; set; }
        public int OtherEmployees { get; set; }
        public int HeadPositions { get; set; }
        public int HeadEmployees { get; set; }
        public int DepartmentHeadPositions { get; set; }
        public int DepartmentHeadEmployees { get; set; }
        public int SpecialistsPosition { get; set; }
        public int SpecialistsEmployee { get; set; }
        public int ProductionPersonnelsPosition { get; set; }
        public int ProductionPersonnelsEmployee { get; set; }
        public int TechnicalStuffPositions { get; set; }
        public int TechnicalStuffEmployee { get; set; }
        public int ServiceStuffPositions { get; set; }
        public int ServiceStuffEmployee { get; set; }
    }
}
