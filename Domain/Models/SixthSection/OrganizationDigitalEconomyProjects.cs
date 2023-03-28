using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SixthSection
{
    [Table("organization_digital_economy_projects", Schema = "organizations")]
    public class OrganizationDigitalEconomyProjects:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("project_name")]
        public string ProjectName { get; set; }

        [Column("basis_file_path")]
        public string BasisFilePath { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("date")]
        public DateTime Date { get;set; }

        [Column("project_stage")]
        public ProjectStatus ProjectStatus { get; set; }
    }
}
