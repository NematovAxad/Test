using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("reestr_project_passport", Schema = "reestrprojects")]
    public class ReestrProjectPassport:IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }

        public Organizations Organizations { get; set; }

        [Column("reestr_project_id")]
        public int ReestrProjectId { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("short_name")]
        public string ShortName { get; set; }

        [Column("passport_status")]
        public ReesterProjectStatus PassportStatus { get; set; }

        [Column("has_terms")]
        public bool HasTerms { get; set; }

        [Column("has_expertise")]
        public bool HasExpertise { get; set; }

        [Column("link_for_system")]
        public string LinkForSystem { get; set; }

        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
