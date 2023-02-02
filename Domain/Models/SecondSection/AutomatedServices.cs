using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("automated_services", Schema = "reestrprojects")]
    public class AutomatedServices:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ReestrProjectAutomatedServices ReestrProjectAutomatedServices { get; set; }

        [Column("service_name")]
        public string ServiceName { get; set; }

        [Column("file_path")]
        public string FilePath { get; set; }
    }
}
