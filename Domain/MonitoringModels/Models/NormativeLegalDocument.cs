using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("normative_legal_document", Schema = "module_regions")]
    public class NormativeLegalDocument : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("name_uz")]
        public string NameUz { get; set; }

        [Column("name_ru")]
        public string NameRu { get; set; }

        [Column("approved_date")]
        public DateTime ApprovedDate { get; set; }

        [Column("document_type")]
        public NormativeType NormativType { get; set; }
        public ICollection<Application> Applications { get; set; }

    }
}
