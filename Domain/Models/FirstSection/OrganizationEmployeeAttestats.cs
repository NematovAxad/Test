using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("organization_employee_attestats", Schema = "organizations")]
    public class OrganizationEmployeeAttestats:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
    }
}
