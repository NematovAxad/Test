using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking
{
    [Table("is_filled_table", Schema = "ranking")]
    public class IsFilledTable:IDomain<int>
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
        [Column("is_filled")]
        public bool IsFilled { get; set; }
        [Column("sphere_id")]
        [ForeignKey("Sphere")]
        public int SphereId { get; set; }
        public Sphere Sphere { get; set; }
        [Column("field_id")]
        [ForeignKey("Field")]
        public int FieldId { get; set; }
        public Field Field { get; set; }
        [Column("element_id")]
        public int ElementId { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
    }
}
