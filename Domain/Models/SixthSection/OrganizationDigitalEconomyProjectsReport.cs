using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SixthSection
{
    [Table("organization_digital_economy_projects_report", Schema = "organizations")]
    public class OrganizationDigitalEconomyProjectsReport:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("projects_count")]
        public int ProjectsCount { get; set; }

        [Column("completed_projects")]
        public int CompletedProjects { get; set; }

        [Column("ongoing_plojects")]
        public int OngoingProjects { get; set; }

        [Column("not_finished_projects")]
        public int NotFinishedProjects { get; set; }
    }
}
