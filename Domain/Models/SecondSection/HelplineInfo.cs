using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("helpline_info", Schema = "organizations")]
    public class HelplineInfo:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("deadline_id")]
        [ForeignKey("Deadline")]
        public int DeadlineId { get; set; }
        public Deadline Deadline { get; set; }
        [Column("regulation_shows_phone")]
        public bool? RegulationShowsPhone { get; set; }
        [Column("regulation_shows_timetable")]
        public bool? RegulationShowsTimetable { get; set; }
        [Column("regulation_shows_services")]
        public bool? RegulationShowsServices { get; set; }
        [Column("regulation_shows_request_procedure")]
        public bool? RegulationShowsRequestProcedure { get; set; }
        [Column("regulation_shows_replay_deadline")]
        public bool? RegulationShowsReplayDeadline { get; set; }
        [Column("regulation_shows_client_rights")]
        public bool? RegulationShowsClientRights { get; set; }
        [Column("regulation_verified")]
        public bool? RegulationVerified { get; set; }
        [Column("helpline_phone_work_status")]
        public bool? HelplinePhoneWorkStatus { get; set; }
        [Column("helpline_phone_rating_option")]
        public bool? HelplinePhoneRatingOption { get; set; }
        [Column("website_has_helpline_statistics")]
        public bool? WebsiteHasHelplineStatistics { get; set; }
        [Column("helpline_statistics_by_time")]
        public bool? HelplineStatisticsByTime { get; set; }
        [Column("helpline_statistics_by_rank")]
        public bool? HelplineStatisticsByRank { get; set; }
        [Column("helpline_statistics_archiving")]
        public bool? HelplineStatisticsArchiving { get; set; }
        [Column("helpline_statistics_intime")]
        public bool? HelplineStatisticsIntime { get; set; }
    }
}
