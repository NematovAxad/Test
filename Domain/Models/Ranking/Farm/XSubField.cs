using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking
{
    [Table("x_sub_field", Schema = "ranking")]
    public class XSubField:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("field_id")]
        [ForeignKey("XField")]
        public int FieldId { get; set; }
        public XField XField { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("max_rate")]
        public double MaxRate { get; set; }
    }
}
