using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SeventhSection;

namespace UserHandler.Commands.SeventhSection
{
    public class OrgComputersCommand:IRequest<OrgComputersCommandResult>
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


        public int CentralAllComputers { get; set; }
        public int TerritorialAllComputers { get; set; }
        public int SubordinateAllComputers { get; set; }
        public int DevicionsAllComputers { get; set; }

        public int CentralWorkingComputers { get; set; }
        public int TerritorialWorkingComputers { get; set; }
        public int SubordinateWorkingComputers { get; set; }
        public int DevicionsWorkingComputers { get; set; }


        public int CentralConnectedLocalSet { get; set; }
        public int TerritorialConnectedLocalSet { get; set; }
        public int SubordinateConnectedLocalSet { get; set; }
        public int DevicionsConnectedLocalSet { get; set; }


        public int CentralConnectedNetwork { get; set; }
        public int TerritorialConnectedNetwork { get; set; }
        public int SubordinateConnectedNetwork { get; set; }
        public int DevicionsConnectedNetwork { get; set; }


        public int CentralConnectedCorporateNetwork { get; set; }
        public int TerritorialConnectedCorporateNetwork { get; set; }
        public int SubordinateConnectedCorporateNetwork { get; set; }
        public int DevicionsConnectedCorporateNetwork { get; set; }


        public int CentralConnectedExat { get; set; }
        public int TerritorialConnectedExat { get; set; }
        public int SubordinateConnectedExat { get; set; }
        public int DevicionsConnectedExat { get; set; }


        public int CentralConnectedEijro { get; set; }
        public int TerritorialConnectedEijro { get; set; }
        public int SubordinateConnectedEijro { get; set; }
        public int DevicionsConnectedEijro { get; set; }


        public int CentralConnectedProjectGov { get; set; }
        public int TerritorialConnectedProjectGov { get; set; }
        public int SubordinateConnectedProjectGov { get; set; }
        public int DevicionsConnectedProjectGov { get; set; }


        public int CentralConnectedProjectAppeal { get; set; }
        public int TerritorialConnectedProjectAppeal { get; set; }
        public int SubordinateConnectedProjectAppeal { get; set; }
        public int DevicionsConnectedProjectAppeal { get; set; }


        public int CentralConnectedProjectResolution { get; set; }
        public int TerritorialConnectedProjectResolution { get; set; }
        public int SubordinateConnectedProjectResolution { get; set; }
        public int DevicionsConnectedProjectResolution { get; set; }


        public int CentralConnectedProjectMyWork { get; set; }
        public int TerritorialConnectedProjectMyWork { get; set; }
        public int SubordinateConnectedProjectMyWork { get; set; }
        public int DevicionsConnectedProjectMyWork { get; set; }
    }
}
