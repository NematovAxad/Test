using Domain.Enums;
using Domain.Models.Ranking;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("x_rank_table", Schema = "ranking")]
    public class XRankTable:IDomain<int>
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
        [ForeignKey("XSphere")]
        public int SphereId { get; set; }
        public XSphere XSphere { get; set; }
        [Column("field_id")]
        [ForeignKey("XField")]
        public int FieldId { get; set; }
        public XField XField { get; set; }
        [Column("sub_field_id")]
        public int SubFieldId { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
    }
}
