using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("stage", Schema = "module_regions")]
    public class Stage : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name_ru")]
        public string NameRu { get; set; }
        [Column("name_uz")]
        public string NameUz { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FileStage> Files { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("stage_status")]
        public StageStatus StageStatus { get; set; }
        [Column("project_id")]
        [ForeignKey("Projects")]
        public int ProjectId { get; set; }
        public Project Projects { get; set; }
        [Column("creation_user_id")]
        public int CreationUserId { get; set; }
        [Column("creation_username")]
        public string CreationUserName { get; set; }
        [Column("creation_date")]
        public DateTime CreationDate { get; set; }
    }
}
