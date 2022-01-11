using Domain.Enums;
using Domain.Models.Ranking;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("g_sphere", Schema = "ranking")]
    public class GSphere:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("category")]
        public ICollection<GField> GFields { get; set; }
    }
}
