using Domain.Enums;
using Domain.Models.FirstSection;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Commands.ThirdSection
{
    public class OrgPublicServicesCommand:IRequest<OrgPublicServicesCommandResult>
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

        public string ServiceNameUz { get; set; }

        public string ServiceNameRu { get; set; }


        public CommanderOrgs ServiceBasedDocumentType { get; set; }

        public string ServiceBasedDocumentName { get; set; }

        public string ServiceBasedDocumentNumber { get; set; }

        public DateTime ServiceBasedDocumentDate { get; set; }



        public OrganizationServiceConsumers PaidFor { get; set; }

        public double ServicePrice { get; set; }

        public string ServicePriceComment { get; set; }



        public ServiceCompletePeriodType ServiceCompletePeriodType { get; set; }

        public int ServiceCompletePeriod { get; set; }


        public OrganizationServiceType ServiceType { get; set; }

        public string ServiceLink { get; set; }

        public string ServiceScreenshotLink { get; set; }


        public OrganizationServiceConsumers ServiceSubjects { get; set; }


        public bool ServiceHasReglament { get; set; }

        public string ServiceReglamentPath { get; set; }

        public string ServiceReglamentComment { get; set; }

        public bool ServiceHasUpdateReglament { get; set; }

        public string ServiceUpdateReglamentPath { get; set; }

        public string ServiceUpdateReglamentComment { get; set; }



        public bool MyGovService { get; set; }

        public string MyGovLink { get; set; }

        public string MyGovScreenshotLink { get; set; }



        public bool OtherApps { get; set; }

        public string AppName { get; set; }

        public string AppLink { get; set; }

        public string AppScreenshot { get; set; }


        public bool ServiceHasReglamentExpert { get; set; }

        public string ServiceHasReglamentExpertComment { get; set; }

        public bool ServiceHasUpdateReglamentExpert { get; set; }

        public string ServiceHasUpdateReglamentExpertComment { get; set; }

        public bool MyGovServiceExpert { get; set; }

        public string MyGovServiceExpertComment { get; set; }

        public bool OtherAppsExpert { get; set; }

        public string OtherAppsExpertComment { get; set; }

        public bool ServiceTypeExpert { get; set; }

        public string ServiceTypeExpertComment { get; set; }
    }
}
