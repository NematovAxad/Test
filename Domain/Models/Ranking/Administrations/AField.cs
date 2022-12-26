using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking.Administrations
{
    [Table("a_field", Schema = "ranking")]
    public class AField : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("sphere_id")]
        [ForeignKey("ASphere")]
        public int SphereId { get; set; }
        public ASphere ASphere { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("max_rate")]
        public double MaxRate { get; set; }
        [Column("category")]
        public ICollection<ASubField> ASubFields { get; set; }

    }
}
