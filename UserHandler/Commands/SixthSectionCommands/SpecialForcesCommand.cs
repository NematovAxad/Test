using Domain.Enums;
using Domain.Models.FirstSection;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Commands.SixthSectionCommands
{
    public class SpecialForcesCommand : IRequest<SpecialForcesCommandResult>
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

        public bool HasSpecialForces { get; set; }

        public string SpecialForcesName { get; set; }

        public string FormOfSpecialForces { get; set; }

        public string FullNameDirector { get; set; }

        public string PhotoPath { get; set; }

        public string HeadPosition { get; set; }

        public string WorkPhone { get; set; }

        public string MobilePhone { get; set; }

        public string Email { get; set; }

        public bool MinistryAgreedHead { get; set; }

        public string MinistryAgreedHeadDocument { get; set; }

        public bool HasCharacterizingDocument { get; set; }

        public string CharacterizingDocument { get; set; }

        public bool HasMinistryAgreedCharacterizingDocument { get; set; }

        public string MinistryAgreedCharacterizingDocument { get; set; }

        public int EmployeesSum { get; set; }

        public int CentralofficeEmployees { get; set; }

        public int RegionalEmployees { get; set; }

        public int SubordinateEmployees { get; set; }

        public int InformationSecurityEmployees { get; set; }

        public int InformationSystemDatabaseEmployees { get; set; }

        public string OrganizationalStructureFile { get; set; }

        public string SpecialistsStuffingDocument { get; set; }

        public string EmployeesSertificates { get; set; }

        public bool EmployeesResumesSentMinistry { get; set; }

        public bool HasWorkPlanOfSpecialForces { get; set; }

        public string WorkPlanOfSpecialForces { get; set; }

        public bool FinanceProvisionMaterial { get; set; }

        public string FinanceProvisionMaterialDocument { get; set; }

        public double AmountOfFunds { get; set; }

        public double LastYearAmountOfFunds { get; set; }

        public double FundForKeepingForces { get; set; }

        public double AmountOfSpentFund { get; set; }

        public double NextYearFundForKeepingForces { get; set; }

        public double OutsourcingSpentFund { get; set; }

        public bool OutsourcingHasCertificates { get; set; }

        public string OutsourcingCompanySertificate { get; set; }

        public int OutsourcingEmployees { get; set; }

        public bool OutsourcingHasWorkPlan { get; set; }

        public string OutsourcingWorkPlanFile { get; set; }

        public bool QuarterlyReportOutsourcing { get; set; }

        public string QuarterlyReportOutsourcingFile { get; set; }

        public bool ExpertExept { get; set; }

        public string ExpertComment { get; set; }

    }
}
