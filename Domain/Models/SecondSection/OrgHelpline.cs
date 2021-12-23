using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("org_helpline", Schema = "organizations")]
    public class OrgHelpline:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("field_id")]
        [ForeignKey("Field")]
        public int FieldId { get; set; }
        public Field Field { get; set; }
        [Column("deadline_id")]
        [ForeignKey("Deadline")]
        public int DeadlineId { get; set; }
        public Deadline Deadline { get; set; }
        [Column("has_online_consultant")]
        public bool HasOnlineConsultant { get; set; }
        [Column("operates_in_working_day")]
        public bool OperatesInWorkingDay { get; set; }
        [Column("acceptable_response_time")]
        public bool AcceptableResponseTime { get; set; }

    }
}
