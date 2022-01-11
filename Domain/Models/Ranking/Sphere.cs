using Domain.Enums;
using Domain.Models.Ranking;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("sphere", Schema = "ranking")]
    public class Sphere:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public ICollection<Field> Fields { get; set; }
    }
}
