using Domain.Enums;
using Domain.Models.Ranking;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking
{
    [Table("x_field", Schema = "ranking")]
    public class XField:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("sphere_id")]
        [ForeignKey("XSphere")]
        public int SphereId { get; set; }

        public XSphere XSphere { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("max_rate")]
        public double MaxRate { get; set; }

        public ICollection<XSubField> XSubFields { get; set; }

        [Column("section")]
        public string Section { get; set; }

    }
}
