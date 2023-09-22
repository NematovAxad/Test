using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SixthSection
{
    [Table("organization_digital_economy_projects_detail", Schema = "organizations")]
    public class OrganizationDigitalEconomyProjectsDetail:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("normative_document_number")]
        public string NormativeDocumentNumber { get; set;}

        [Column("application_number")]
        public string ApplicationNumber { get; set; }

        [Column("project_index")]
        public string ProjectIndex { get; set; }

        [Column("responsibles")]
        public string Responsibles { get; set; }

        [Column("actions")]
        public string Actions { get; set; }

        [Column("status")]
        public ProjectStatus Status { get; set; }
    }
}
