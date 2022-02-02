using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("performencer", Schema = "module_regions")]
    public class Performencer : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
 }
