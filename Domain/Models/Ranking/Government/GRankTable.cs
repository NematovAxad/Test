using Domain.Enums;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("g_rank_table", Schema = "ranking")]
    public class GRankTable:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("quarter")]
        public Quarters Quarter { get; set; }
        [Column("element_id")]
        public int ElementId { get; set; }
        [Column("rank")]
        public double Rank { get; set; }
        [Column("is_exception")]
        public bool IsException { get; set; }
        [Column("sphere_id")]
        [ForeignKey("GSphere")]
        public int SphereId { get; set; }
        public GSphere GSphere { get; set; }
        [Column("field_id")]
        [ForeignKey("GField")]
        public int FieldId { get; set; }
        public GField GField { get; set; }
        [Column("sub_field_id")]
        public int SubFieldId { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("expert_id")]
        public int ExpertId { get; set; }
        [Column("espert_pinfl")]
        public string ExpertPinfl { get; set; }
        [Column("created_date")]
        public DateTime CreatedDAte { get; set; }
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
