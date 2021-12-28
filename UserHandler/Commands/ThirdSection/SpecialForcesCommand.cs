using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Commands.ThirdSection
{
    public class SpecialForcesCommand:IRequest<SpecialForcesCommandResult>
    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserId { get; set; }
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
        public bool HasSpecialForces { get; set; }
        public string SpecialForcesName { get; set; }
        public string FormOfSpecialForces { get; set; }
        public string FullNameDirector { get; set; }
        public string HeadPosition { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public bool MinistryAgreedHead { get; set; }
        public bool HasCharacterizingDocument { get; set; }
        public IFormFile CharacterizingDocument { get; set; }
        public bool HasMinistryAgreedCharacterizingDocument { get; set; }
        public IFormFile MinistryAgreedCharacterizingDocument { get; set; }
        public int EmployeesSum { get; set; }
        public int CentralofficeEmployees { get; set; }
        public int RegionalEmployees { get; set; }
        public int SubordinateEmployees { get; set; }
        public int InformationSecurityEmployees { get; set; }
        public int InformationSystemDatabaseEmployees { get; set; }
        public IFormFile OrganizationalStructureFile { get; set; }
        public IFormFile SpecialistsStuffingDocument { get; set; }
        public IFormFile EmployeesSertificates { get; set; }
        public bool EmployeesResumesSentMinistry { get; set; }
        public bool HasWorkPlanOfSpecialForces { get; set; }
        public IFormFile WorkPlanOfSpecialForces { get; set; }
        public bool FinanceProvisionMaterial { get; set; }
        public double AmountOfFunds { get; set; }
        public double LastYearAmountOfFunds { get; set; }
        public double FundForKeepingForces { get; set; }
        public double AmountOfSpentFund { get; set; }
        public double NextYearFundForKeepingForces { get; set; }
        public double OutsourcingSpentFund { get; set; }
        public bool OutsourcingHasCertificates { get; set; }
        public int OutsourcingEmployees { get; set; }
        public bool OutsourcingHasWorkPlan { get; set; }
        public bool QuarterlyReportOutsourcing { get; set; }
    }
}
