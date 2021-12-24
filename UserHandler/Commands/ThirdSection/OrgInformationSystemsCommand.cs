using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Commands.ThirdSection
{
    public class OrgInformationSystemsCommand:IRequest<OrgInformationSystemsCommandResult>
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
        public string SystemName { get; set; }
        public string SystemAppointment { get; set; }
        public string SystemReestrNumber { get; set; }
        public string SystemCondition { get; set; }
        public DateTime CommissioningDate { get; set; }
        public DateTime ExpertOpinionDate { get; set; }
        public DateTime ExpertOpinionNumber { get; set; }
        public string ListOfServices { get; set; }
        public int UsersCount { get; set; }
        public bool UsesClassifiers { get; set; }
        public string UsedClassifiers { get; set; }
        public string OtherClassifiers { get; set; }
        public bool HasIntegrationWithEgovernment { get; set; }
        public bool IntegrationInterdepartmentalPlatform { get; set; }
        public string TransmittingInformationsFirst { get; set; }
        public bool IntegratedRegisterClassifiers { get; set; }
        public string TransmittingInformationsSecond { get; set; }
        public bool IntegratedCentralDatabase { get; set; }
        public string TransmittingInformationsThird { get; set; }
        public bool IntegratedComplexesOfSystems { get; set; }
        public string TransmittingInformationsFourth { get; set; }
        public bool IntegratedPaymentSystem { get; set; }
        public string PaymentSystemName { get; set; }
        public bool AvailabilityAutomationOfService { get; set; }
        public string AutomatedServices { get; set; }
    }
}
