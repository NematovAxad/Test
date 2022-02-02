using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("cooworkers", Schema = "module_regions")]
    public class Cooworkers:IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("project_id")]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [Column("performencer_id")]
        [ForeignKey("Performencer")]
        public int PerformencerId { get; set; }
        public Performencer Performencer { get; set; }
    }
}
