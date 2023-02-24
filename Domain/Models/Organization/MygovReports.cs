using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Organization
{
    [Table("mygov_reports", Schema = "organizations")]
    public class MygovReports:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("mygov_main_org_id")]
        public int MygovMainOrgId { get; set; }

        [Column("mygov_org_id")]
        public int MygovOrgId { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }

        public Organizations Organization { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("service_name")]
        public string ServiceName { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("part")]
        public int Part { get; set; }

        [Column("all_requests")]
        public int AllRequests { get; set; }

        [Column("late_requests")]
        public int LateRequests { get; set; }
    }
}
