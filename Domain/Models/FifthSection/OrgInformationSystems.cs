using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection
{
    [Table("org_information_systems", Schema = "organizations")]
    public class OrgInformationSystems:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("system_name")]
        public string SystemName { get; set; }
        [Column("system_appointment")]
        public string SystemAppointment { get; set; }
        [Column("system_reestr_number")]
        public string SystemReestrNumber { get; set; }
        [Column("system_condition")]
        public string SystemCondition { get; set; }
        [Column("commissioning_date")]
        public DateTime CommissioningDate { get; set; }
        [Column("expert_opinion_date")]
        public DateTime ExpertOpinionDate { get; set; }
        [Column("expert_opinion_number")]
        public string ExpertOpinionNumber { get; set; }
        [Column("list_of_services")]
        public string ListOfServices { get; set; }
        [Column("users_count")]
        public int UsersCount { get; set; }
        [Column("uses_classifiers")]
        public bool UsesClassifiers { get; set; }
        [Column("used_clasifiers")]
        public string UsedClassifiers { get; set; }
        [Column("other_clasifiers")]
        public string OtherClassifiers { get; set; }
        [Column("has_integration_with_egovernment")]
        public bool HasIntegrationWithEgovernment { get; set; }
        [Column("integration_interdepartmental_platform")]
        public bool IntegrationInterdepartmentalPlatform { get; set; }
        [Column("transmitting_informations_first")]
        public string TransmittingInformationsFirst { get; set; }
        [Column("integrated_register_classifiers")]
        public bool IntegratedRegisterClassifiers { get; set; }
        [Column("transmitting_informations_second")]
        public string TransmittingInformationsSecond { get; set; }
        [Column("integrated_central_database")]
        public bool IntegratedCentralDatabase { get; set; }
        [Column("transmitting_informations_third")]
        public string TransmittingInformationsThird { get; set; }
        [Column("integrated_complexes_of_systems")]
        public bool IntegratedComplexesOfSystems { get; set; }
        [Column("transmitting_informations_fourth")]
        public string TransmittingInformationsFourth { get; set; }
        [Column("integrated_payment_system")]
        public bool IntegratedPaymentSystem { get; set; }
        [Column("payment_system_name")]
        public string PaymentSystemName { get; set; }
        [Column("availability_automation_of_service")]
        public bool AvailabilityAutomationOfService { get; set; }
        [Column("automated_services")]
        public string AutomatedServices { get; set; }
    }
}
