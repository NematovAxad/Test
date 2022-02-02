using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("financier", Schema = "module_regions")]
    public class Financier : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name_uz")]
        public string NameUz { get; set; }
        [Column("name_ru")]
        public string NameRu { get; set; }
    }
}
