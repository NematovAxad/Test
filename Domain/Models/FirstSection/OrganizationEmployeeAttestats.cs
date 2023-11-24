using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("organization_employee_attestats", Schema = "organizations")]
    public class OrganizationEmployeeAttestats:IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }

        public Organizations Organization { get; set; }

        [Column("first_name")]
        public string FirstName { get; set;}

        [Column("last_name")]
        public string LastName { get; set; }
    }
}
