using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SecondSectionCommandResult;

namespace UserHandler.Commands.SecondSectionCommand
{
    public class HelplineInfoCommand:IRequest<HelplineInfoCommandResult>
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
        public string Comment { get; set; }
        public string ScreenshotLink { get; set; }
        public string Screenshot { get; set; }
        public bool? RegulationShowsPhone { get; set; }
        public string Comment2 { get; set; }
        public string Screenshot2Link { get; set; }
        public string Screenshot2 { get; set; }
        public bool? RegulationShowsTimetable { get; set; }
        public string Comment3 { get; set; }
        public string Screenshot3Link { get; set; }
        public string Screenshot3 { get; set; }
        public bool? RegulationShowsServices { get; set; }
        public string Comment4 { get; set; }
        public string Screenshot4Link { get; set; }
        public string Screenshot4 { get; set; }
        public bool? RegulationShowsRequestProcedure { get; set; }
        public string Comment5 { get; set; }
        public string Screenshot5Link { get; set; }
        public string Screenshot5 { get; set; }
        public bool? RegulationShowsReplayDeadline { get; set; }
        public string Comment6 { get; set; }
        public string Screenshot6Link { get; set; }
        public string Screenshot6 { get; set; }
        public bool? RegulationShowsClientRights { get; set; }
        public string Comment7 { get; set; }
        public string Screenshot7Link { get; set; }
        public string Screenshot7 { get; set; }
        public bool? RegulationVerified { get; set; }
        public string Comment8 { get; set; }
        public string Screenshot8Link { get; set; }
        public string Screenshot8 { get; set; }
        public bool? HelplinePhoneWorkStatus { get; set; }
        public string Comment9 { get; set; }
        public string Screenshot9Link { get; set; }
        public string Screenshot9 { get; set; }
        public bool? HelplinePhoneRatingOption { get; set; }
        public string Comment10 { get; set; }
        public string Screenshot10Link { get; set; }
        public string Screenshot10 { get; set; }
        public bool? WebsiteHasHelplineStatistics { get; set; }
        public string Comment11 { get; set; }
        public string Screenshot11Link { get; set; }
        public string Screenshot11 { get; set; }
        public bool? HelplineStatisticsByTime { get; set; }
        public string Comment12 { get; set; }
        public string Screenshot12Link { get; set; }
        public string Screenshot12 { get; set; }
        public bool? HelplineStatisticsByRank { get; set; }
        public string Comment13 { get; set; }
        public string Screenshot13Link { get; set; }
        public string Screenshot13 { get; set; }
        public bool? HelplineStatisticsArchiving { get; set; }
        public string Comment14 { get; set; }
        public string Screenshot14Link { get; set; }
        public string Screenshot14 { get; set; }
        public bool? HelplineStatisticsIntime { get; set; }
    }
}
