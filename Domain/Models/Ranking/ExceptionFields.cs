using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking
{
    [Table("exception_fields", Schema = "ranking")]
    public class ExceptionFields:IDomain<int>
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
        
        [Column("sphere_id")]
        public int SphereId { get; set; }
        
        [Column("field_id")]
        public int FieldId { get; set; }

        [Column("comment")]
        public string Comment { get; set; }
       
        [Column("expert_id")]
        public int ExpertId { get; set; }
        
        [Column("espert_pinfl")]
        public string ExpertPinfl { get; set; }
        
        [Column("created_date")]
        public DateTime CreatedDAte { get; set; }
    }
}
