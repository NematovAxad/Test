using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("deadline", Schema = "ranking")]
    public class Deadline:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("quarter")]
        public Quarters Quarter { get; set; }
        [Column("deadline_date")]
        public DateTime DeadlineDate { get; set; }
        [Column("operator_deadline_date")]
        public DateTime OperatorDeadlineDate { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("ping_service")]
        public bool PingService { get; set; }
    }
}
