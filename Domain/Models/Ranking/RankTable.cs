using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("rank_table", Schema = "ranking")]
    public class RankTable:IDomain<int>
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
        [Column("rank")]
        public double Rank { get; set; }
        [Column("is_exception")]
        public bool IsException { get; set; }
        [Column("sphere_id")]
        [ForeignKey("Sphere")]
        public int SphereId { get; set; }
        public Sphere Sphere { get; set; }
        [Column("field_id")]
        [ForeignKey("Field")]
        public int FieldId { get; set; }
        public Field Field { get; set; }
    }
}
