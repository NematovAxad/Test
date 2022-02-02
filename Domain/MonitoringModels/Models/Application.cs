using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("application", Schema = "module_regions")]
    public class Application : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name_uz")]
        public string NameUz { get; set; }
        [Column("name_ru")]
        public string NameRu { get; set; }
        [Column("short_name")]
        public string ShortName { get; set; }
        [Column("performance_year")]
        public DateTime PerformanceYear { get; set; }
        [Column("normative_legal_document_id")]
        [ForeignKey("NormativeLegalDocument")]
        public int NormativeLegalDocumentId { get; set; }
        public NormativeLegalDocument NormativeLegalDocument { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
