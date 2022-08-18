using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection
{
    [Table("org_information_systems", Schema = "organizations")]
    public class OrgInformationSystems:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("system_full_name")]
        public string SystemFullName { get; set; }
        [Column("system_short_name")]
        public string SystemShortName { get; set; }
        [Column("system_status")]
        public ReestrStatus SystemStatus { get; set; }
        [Column("system_id")]
        public string SystemId { get; set; }
        [Column("system_link")]
        public string SystemLink { get; set; }
        [Column("system_basis")]
        public string SystemBasis { get; set; }
        [Column("system_purpose")]
        public string SystemPurpose { get; set; }
        [Column("system_connections")]
        public bool SystemConnections { get; set; }
        [Column("classifiers_used")]
        public bool ClassifiersUsed { get; set; }
        [Column("system_unique_ids")]
        public bool SystemUniqueIds { get; set; }
        [Column("expert_decision")]
        public bool ExpertDecision { get; set; }
        [Column("cybersecurity_decision")]
        public bool SybersecurityDecision { get; set; }
    }
}