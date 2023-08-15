using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("sub_org_statistics", Schema = "organizations")]
    public class SubOrgStatistics : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("central_managements")]
        public int CentralManagements { get; set; }
        [Column("territorial_managements")]
        public int TerritorialManagements { get; set; }
        [Column("subordinations")]
        public int Subordinations { get; set; }
        [Column("others")]
        public int Others { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
