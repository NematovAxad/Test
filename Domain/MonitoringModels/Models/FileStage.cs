using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("file", Schema = "module_regions")]
    public class FileStage : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("file_save_date")]
        public DateTime FileSaveDate { get; set; }
        [Column("stages_id")]
        [ForeignKey("Stage")]
        public int StageId { get; set; }
        public Stage Stage { get; set; }
    }
}
