using Domain.Models.FirstSection;
using JohaRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SixthSection
{
    [Table("organization_ict_special_forces", Schema = "organizations")]
    public class OrganizationIctSpecialForces : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }

        public Organizations Organization { get; set; }

        [Column("has_special_forces")]
        public bool HasSpecialForces { get; set; }


        [Column("special_forces_name")]
        public string SpecialForcesName { get; set; }


        [Column("form_of_special_forces")]
        public string FormOfSpecialForces { get; set; }


        [Column("full_name_head")]
        public string FullNameDirector { get; set; }


        [Column("head_position")]
        public string HeadPosition { get; set; }


        [Column("work_phone")]
        public string WorkPhone { get; set; }


        [Column("mobile_phone")]
        public string MobilePhone { get; set; }


        [Column("email")]
        public string Email { get; set; }


        [Column("ministry_agreed_head")]
        public bool MinistryAgreedHead { get; set; }

        [Column("ministry_agreed_head_document")]
        public string MinistryAgreedHeadDocument { get; set; } // added


        [Column("has_characterizing_document")]
        public bool HasCharacterizingDocument { get; set; }


        [Column("characterizing_document")]
        public string CharacterizingDocument { get; set; }


        [Column("has_ministry_agreed_characterizing_document")]
        public bool HasMinistryAgreedCharacterizingDocument { get; set; }


        [Column("ministry_agreed_characterizing_document")]
        public string MinistryAgreedCharacterizingDocument { get; set; }


        [Column("employees_sum")]
        public int EmployeesSum { get; set; }


        [Column("central_office_employees")]
        public int CentralofficeEmployees { get; set; }


        [Column("regional_employees")]
        public int RegionalEmployees { get; set; }


        [Column("subordinate_employees")]
        public int SubordinateEmployees { get; set; }


        [Column("information_security_employees")]
        public int InformationSecurityEmployees { get; set; }


        [Column("information_system_database_employees")]
        public int InformationSystemDatabaseEmployees { get; set; }


        [Column("organizational_structure_file")]
        public string OrganizationalStructureFile { get; set; }


        [Column("specialists_stuffing_document")]
        public string SpecialistsStuffingDocument { get; set; }


        [Column("employees_sertificates")]
        public string EmployeesSertificates { get; set; }


        [Column("employees_resumes_sent_ministry")]
        public bool EmployeesResumesSentMinistry { get; set; }


        [Column("has_work_plan_of_special_forces")]
        public bool HasWorkPlanOfSpecialForces { get; set; }


        [Column("work_plan_of_special_forces")]
        public string WorkPlanOfSpecialForces { get; set; }


        [Column("finance_provision_material")]
        public bool FinanceProvisionMaterial { get; set; }

        [Column("finance_provision_material_document")]
        public string FinanceProvisionMaterialDocument { get; set; }     // added


        [Column("amount_of_funds")]
        public double AmountOfFunds { get; set; }


        [Column("last_year_amount_of_funds")]
        public double LastYearAmountOfFunds { get; set; }


        [Column("fund_for_keeping_forces")]
        public double FundForKeepingForces { get; set; }


        [Column("amount_of_spent_fund")]
        public double AmountOfSpentFund { get; set; }


        [Column("next_year_fund_for_keeping_forces")]
        public double NextYearFundForKeepingForces { get; set; }


        [Column("outsourcing_spent_fund")]
        public double OutsourcingSpentFund { get; set; }


        [Column("outsourcing_has_certificates")]
        public bool OutsourcingHasCertificates { get; set; }


        [Column("outsourcing_company_sertificate")]
        public string OutsourcingCompanySertificate { get; set; }


        [Column("outsourcing_employees")]
        public int OutsourcingEmployees { get; set; }


        [Column("outsourcing_has_work_plan")]
        public bool OutsourcingHasWorkPlan { get; set; }


        [Column("outsourcing_work_plan_file")]
        public string OutsourcingWorkPlanFile { get; set; }


        [Column("quarterly_report_outsourcing")]
        public bool QuarterlyReportOutsourcing { get; set; }


        [Column("quarterly_report_outsourcing_file")]
        public string QuarterlyReportOutsourcingFile { get; set; }


        [Column("expert_except")]
        public bool ExpertExept { get; set; }

        [Column("expert_comment")]
        public string ExpertComment { get; set; }

    }
}
