using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("file_project", Schema = "module_regions")]
    public class FileProject:IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("file_save_date")]
        public DateTime FileSaveDate { get; set; }
        [Column("project_id")]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
