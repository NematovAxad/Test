using Domain.Models.FirstSection;
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
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment")]
        public string Comment { get; set; }
        [Column("screenshot_link")]
        public string ScreenshotLink { get; set; }
        [Column("regulation_shows_phone")]
        public bool? RegulationShowsPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment2")]
        public string Comment2 { get; set; }
        [Column("screenshot2_link")]
        public string Screenshot2Link { get; set; }
        [Column("regulation_shows_timetable")]
        public bool? RegulationShowsTimetable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment3")]
        public string Comment3 { get; set; }
        [Column("screenshot3_link")]
        public string Screenshot3Link { get; set; }
        [Column("regulation_shows_services")]
        public bool? RegulationShowsServices { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment4")]
        public string Comment4 { get; set; }
        [Column("screenshot4_link")]
        public string Screenshot4Link { get; set; }
        [Column("regulation_shows_request_procedure")]
        public bool? RegulationShowsRequestProcedure { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment5")]
        public string Comment5 { get; set; }
        [Column("screenshot5_link")]
        public string Screenshot5Link { get; set; }
        [Column("regulation_shows_replay_deadline")]
        public bool? RegulationShowsReplayDeadline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment6")]
        public string Comment6 { get; set; }
        [Column("screenshot6_link")]
        public string Screenshot6Link { get; set; }
        [Column("regulation_shows_client_rights")]
        public bool? RegulationShowsClientRights { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment7")]
        public string Comment7 { get; set; }
        [Column("screenshot7_link")]
        public string Screenshot7Link { get; set; }
        [Column("regulation_verified")]
        public bool? RegulationVerified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 


        [Column("Comment8")]
        public string Comment8 { get; set; }
        [Column("screenshot8_link")]
        public string Screenshot8Link { get; set; }
        [Column("helpline_phone_work_status")]
        public bool? HelplinePhoneWorkStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 

        [Column("Comment9")]
        public string Comment9 { get; set; }
        [Column("screenshot9_link")]
        public string Screenshot9Link { get; set; }
        [Column("helpline_phone_rating_option")]
        public bool? HelplinePhoneRatingOption { get; set; }
        /// <summary>
        /// 
        /// </summary>


        [Column("Comment10")]
        public string Comment10 { get; set; }
        [Column("screenshot10_link")]
        public string Screenshot10Link { get; set; }
        [Column("website_has_helpline_statistics")]
        public bool? WebsiteHasHelplineStatistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment11")]
        public string Comment11 { get; set; }
        [Column("screenshot11_link")]
        public string Screenshot11Link { get; set; }
        [Column("helpline_statistics_by_time")]
        public bool? HelplineStatisticsByTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment12")]
        public string Comment12 { get; set; }
        [Column("screenshot12_link")]
        public string Screenshot12Link { get; set; }
        [Column("helpline_statistics_by_rank")]
        public bool? HelplineStatisticsByRank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment13")]
        public string Comment13 { get; set; }
        [Column("screenshot13_link")]
        public string Screenshot13Link { get; set; }
        [Column("helpline_statistics_archiving")]
        public bool? HelplineStatisticsArchiving { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Comment14")]
        public string Comment14 { get; set; }
        [Column("screenshot14_link")]
        public string Screenshot14Link { get; set; }
        [Column("helpline_statistics_intime")]
        public bool? HelplineStatisticsIntime { get; set; }


        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
