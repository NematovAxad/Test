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
        public bool? RegulationShowsPhone { get; set; }
        public bool? RegulationShowsTimetable { get; set; }
        public bool? RegulationShowsServices { get; set; }
        public bool? RegulationShowsRequestProcedure { get; set; }
        public bool? RegulationShowsReplayDeadline { get; set; }
        public bool? RegulationShowsClientRights { get; set; }
        public bool? RegulationVerified { get; set; }
        public bool? HelplinePhoneWorkStatus { get; set; }
        public bool? HelplinePhoneRatingOption { get; set; }
        public bool? WebsiteHasHelplineStatistics { get; set; }
        public bool? HelplineStatisticsByTime { get; set; }
        public bool? HelplineStatisticsByRank { get; set; }
        public bool? HelplineStatisticsArchiving { get; set; }
        public bool? HelplineStatisticsIntime { get; set; }
    }
}
