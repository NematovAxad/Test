using AdminHandler.Results.Organization;
using Domain.Enums;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AdminHandler.Commands.Organization
{
    public class OrgCommand:IRequest<OrgCommandResult>
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
        public int UserServiceId { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string DirectorMidName { get; set; }
        public string DirectorPosition { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressHomeNo { get; set; }
        public string AddressStreet { get; set; }
        public string AddressProvince { get; set; }
        public string AddressDistrict { get; set; }
        public string PostIndex { get; set; }
        public string Department { get; set; }
        public string DirectorMail { get; set; }
        public string OrgMail { get; set; }
        public string WebSite { get; set; }
        public OrgTypes OrgType { get; set; }
        public string Fax { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsIct { get; set; }
        public bool? IsMonitoring { get; set; }
    }
}
