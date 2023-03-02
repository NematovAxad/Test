using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SeventhSection
{
    [Table("organization_servers", Schema = "organizations")]
    public class OrganizationServers:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("server_type")]
        public string ServerType { get; set; }
        [Column("server_brand")]
        public string ServerBrand { get; set; }
        [Column("server_config")]
        public string ServerConfig { get; set; }
        [Column("server_automatic_tasks")]
        public string ServerAutomaticTasks { get; set; }
        [Column("number_of_servers")]
        public string NumberOfServers { get; set; }
    }
}
