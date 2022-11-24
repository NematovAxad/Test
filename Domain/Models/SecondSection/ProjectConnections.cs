using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("project_connections", Schema = "reestrprojects")]
    public class ProjectConnections:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("reestr_project_connection_id")]
        [ForeignKey("ReestrProjectConnection")]
        public int ReestrProjectConnectionId { get; set; }
        public ReestrProjectConnection ReestrProjectConnection { get; set; }

        [Column("connection_type")]
        public ReestrProjectConnectionType ReestrProjectConnectionType { get; set; }

        [Column("platform_reestr_id")]
        public string PlatformReestrId { get; set; }


        [Column("file_path")]
        public string FilePath { get; set; }
    }
}
