using Domain.Enums;
using Domain.Models.FirstSection;
using Domain.ReesterModels;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("reestr_project_passport_details", Schema = "reestrprojects")]
    public class ReestrProjectPassportDetails:IDomain<int>
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

        [Column("basis_name")]
        public string BasisName { get; set; }

        [Column("tasks")]
        public string Tasks { get; set; }

        [Column("is_interdepartmental_information_system")]
        public bool IsInterdepartmentalInformationSystem { get; set; }

        [Column("cybersecurity_expertise")]
        public string CybersecurityExpertise { get; set; }
    }
}
