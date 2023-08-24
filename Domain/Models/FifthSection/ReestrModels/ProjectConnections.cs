using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("project_connections", Schema = "reestrprojects")]
    public class ProjectConnections : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ReestrProjectConnection ReestrProjectConnection { get; set; }

        [Column("connection_type")]
        public ReestrProjectConnectionType ConnectionType { get; set; }

        [Column("platform_reestr_id")]
        public string PlatformReestrId { get; set; }


        [Column("file_path")]
        public string FilePath { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
    }
}
