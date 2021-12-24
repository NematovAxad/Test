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
    public class OrgPublicServicesCommand:IRequest<OrgPublicServicesCommandResult>
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
        public string ServiceName { get; set; }
        public string UserTypes { get; set; }
        public string RenderingForm { get; set; }
        public string PortalLink { get; set; }
        public string ServiceLink { get; set; }
        public string MobileApp { get; set; }
        public string OtherResources { get; set; }
        public bool IsPaid { get; set; }
        public string ServiceResult { get; set; }
        public string ServiceOtherResult { get; set; }
        public bool MechanizmForTrackingProgress { get; set; }
        public string TrackingProgressBy { get; set; }
        public bool ReglamentUpdated { get; set; }
        public IFormFile ReglamentFile { get; set; }
    }
}
