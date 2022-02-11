using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("project_comment", Schema = "module_regions")]
    public class ProjectComment:IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        [Column("user_role")]
        public string UserRole { get; set; }
        [Column("action")]
        public string Action { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("date_comment")]
        public DateTime DateComment { get; set; }

        [Column("project_id")]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
