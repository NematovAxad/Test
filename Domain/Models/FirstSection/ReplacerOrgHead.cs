using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("replacer_org_head", Schema = "organizations")]
    public class ReplacerOrgHead : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("mid_name")]
        public string MidName { get; set; }
        [Column("position")]
        public string Position { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("mail")]
        public string Email { get; set; }
        [Column("fax")]
        public string Fax { get; set; }
        [Column("file_path")]
        public string FilePath { get; set; }
    }
}
