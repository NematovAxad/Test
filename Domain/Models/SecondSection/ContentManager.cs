using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("content_manager", Schema = "organizations")]
    public class ContentManager:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("position")]
        public string Position { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("file_path")]
        public string FilePath { get; set; }
    }
}
