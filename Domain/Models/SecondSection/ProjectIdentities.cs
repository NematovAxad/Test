using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("project_identities", Schema = "reestrprojects")]
    public class ProjectIdentities:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ReestrProjectIdentities ReestrProjectIdentities { get; set; }

        [Column("identity_type")]
        public ReestrProjectIdentityType IdentitiyType { get; set; }

        [Column("identity_url")]
        public string IdentityUrl { get; set; }


        [Column("file_path")]
        public string FilePath { get; set; }
    }
}
