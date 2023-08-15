using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("sub_organization", Schema = "organizations")]
    public class SubOrganizations : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("parent_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("director_first_name")]
        public string DirectorFirstName { get; set; }
        [Column("director_last_name")]
        public string DirectorLastName { get; set; }
        [Column("director_mid_name")]
        public string DirectorMidName { get; set; }
        [Column("owner_type")]
        public string OwnerType { get; set; }
        [Column("official_site")]
        public string OfficialSite { get; set; }
        [Column("contacts")]
        public string Contacts { get; set; }
        [Column("inn")]
        public string Inn { get; set; }
        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
