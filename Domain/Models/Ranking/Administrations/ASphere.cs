using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Ranking.Administrations
{
    [Table("a_sphere", Schema = "ranking")]
    public class ASphere:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("section")]
        public string Section { get; set; }

        public ICollection<AField> AFields { get; set; }
    }
}
