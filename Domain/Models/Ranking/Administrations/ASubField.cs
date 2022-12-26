using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking.Administrations
{
    [Table("a_sub_field", Schema = "ranking")]
    public class ASubField:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("field_id")]
        [ForeignKey("AField")]
        public int FieldId { get; set; }

        public AField AField { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("max_rate")]
        public double MaxRate { get; set; }

        [Column("section")]
        public string Section { get; set; }
    }
}
