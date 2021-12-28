using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection
{
    [Table("delays_on_projects", Schema = "organizations")]
    public class DelaysOnProjects:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("project_name")]
        public string ProjectName { get; set; }
        [Column("project_document_number")]
        public string ProjectDocumentNumber { get; set; }
        [Column("project_document_date")]
        public DateTime ProjectDocumentDate { get; set; }
        [Column("project_applying_mechanism")]
        public string ProjectApplyingMechanism { get; set; }
        [Column("project_applying_date")]
        public DateTime ProjectApplyingDate { get; set; }
        [Column("project_financing_source")]
        public string ProjectFinancingSource { get; set; }
    }
}
