using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Organization
{
    [Table("mygov_reports_detail", Schema = "organizations")]
    public class MygovReportsDetail:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("task_id")]
        public int TaskId { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("service_name")]
        public string ServiceName { get; set; }

        [Column("deadline_from")]
        public string DeadlineFrom { get; set; }

        [Column("deadline_to")]
        public string DeadlineTo { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("part")]
        public int Part { get; set; }
    }
}
