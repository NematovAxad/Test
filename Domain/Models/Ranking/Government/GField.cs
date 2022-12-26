using Domain.Enums;
using Domain.Models.Ranking;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking
{
    [Table("g_field", Schema = "ranking")]
    public class GField:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("sphere_id")]
        [ForeignKey("GSphere")]
        public int SphereId { get; set; }

        public GSphere GSphere { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("max_rate")]
        public double MaxRate { get; set; }

        [Column("section")]
        public string Section { get; set; }

        public ICollection<GSubField> GSubFields { get; set; }

    }
}
