using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking
{
    [Table("g_sub_field", Schema = "ranking")]
    public class GSubField:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("field_id")]
        [ForeignKey("GField")]
        public int FieldId { get; set; }
        public GField GField { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("max_rate")]
        public double MaxRate { get; set; }
    }
}
