using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("comment", Schema = "module_regions")]
    public class Comment : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("date_comment")]
        public DateTime DateComment { get; set; }

        [Column("stages_id")]
        [ForeignKey("Stage")]
        public int StageId { get; set; }
        public Stage Stage { get; set; }
    }
}
