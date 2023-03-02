using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection
{
    [Table("organization_processes", Schema = "organizations")]
    public class OrgProcesses : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("process_number")]
        public int ProcessNumber { get; set; } 
        [Column("it_process_number")]
        public int ItProcessNumber { get; set; }
        [Column("filepath")]
        public string FilePath { get; set; }
    }
}
