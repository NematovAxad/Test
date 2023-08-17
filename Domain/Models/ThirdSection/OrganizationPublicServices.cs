using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.ThirdSection
{
    [Table("organization_public_services", Schema = "organizations")]
    public class OrganizationPublicServices:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }

        [Column("service_name_uz")]
        public string ServiceNameUz { get; set; }

        [Column("service_name_ru")]
        public string ServiceNameRu { get; set; }



        [Column("service_based_document_type")]
        public CommanderOrgs ServiceBasedDocumentType { get; set; }

        [Column("service_based_document_name")]
        public string ServiceBasedDocumentName { get; set; }

        [Column("service_based_document_number")]
        public string ServiceBasedDocumentNumber { get; set; }

        [Column("service_based_document_date")]
        public DateTime ServiceBasedDocumentDate { get; set; }



        [Column("paid_for")]
        public OrganizationServiceConsumers PaidFor { get; set; }

        [Column("service_price")]
        public double ServicePrice { get; set; }

        [Column("service_price_comment")]
        public string ServicePriceComment { get; set; }



        [Column("service_complete_period_type")]
        public ServiceCompletePeriodType ServiceCompletePeriodType { get; set; }

        [Column("service_complete_period")]
        public int ServiceCompletePeriod { get; set; }


        [Column("service_type")]
        public OrganizationServiceType ServiceType { get; set; }

        [Column("service_link")]
        public string ServiceLink { get; set; }

        [Column("service_screenshot_link")]
        public string ServiceScreenshotLink { get; set; }


        [Column("service_subject")]
        public OrganizationServiceConsumers ServiceSubjects { get; set; }


        [Column("service_has_reglament")]
        public bool ServiceHasReglament { get; set; }

        [Column("service_reglament_path")]
        public string ServiceReglamentPath { get; set; }

        [Column("service_reglament_comment")]
        public string ServiceReglamentComment { get; set; }

        [Column("service_has_update_reglament")]
        public bool ServiceHasUpdateReglament { get; set; }

        [Column("service_update_reglament_path")]
        public string ServiceUpdateReglamentPath { get; set; }

        [Column("service_update_reglament_comment")]
        public string ServiceUpdateReglamentComment { get; set; }



        [Column("mygov_service")]
        public bool MyGovService { get; set; }

        [Column("mygov_link")]
        public string MyGovLink { get; set; }

        [Column("mygov_screenshot_link")]
        public string MyGovScreenshotLink { get; set; }



        [Column("other_apps")]
        public bool OtherApps { get; set; }

        [Column("app_name")]
        public string AppName { get; set; }

        [Column("app_link")]
        public string AppLink { get; set; }

        [Column("app_screenshot")]
        public string AppScreenshot { get; set; }


        [Column("service_has_reglament_expert")]
        public bool ServiceHasReglamentExpert { get; set; }

        [Column("service_has_reglament_expert_comment")]
        public string ServiceHasReglamentExpertComment { get; set; }

        [Column("service_has_update_reglament_expert")]
        public bool ServiceHasUpdateReglamentExpert { get; set; }

        [Column("service_has_update_reglament_expert_comment")]
        public string ServiceHasUpdateReglamentExpertComment { get; set; }

        [Column("mygov_service_expert")]
        public bool MyGovServiceExpert { get; set; }

        [Column("mygov_service_expert_commetn")]
        public string MyGovServiceExpertComment { get; set; }

        [Column("other_apps_expert")]
        public bool OtherAppsExpert { get; set; }

        [Column("other_apps_expert_commetn")]
        public string OtherAppsExpertComment { get; set; }

        [Column("service_type_expert")]
        public bool ServiceTypeExpert { get; set; }

        [Column("service_type_expert_comment")]
        public string ServiceTypeExpertComment { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

    }
}
