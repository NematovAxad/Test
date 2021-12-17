using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("field", Schema = "ranking")]
    public class Field:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("sphere_id")]
        [ForeignKey("Sphere")]
        public int SphereId { get; set; }
        public Sphere Sphere { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("max_rate")]
        public double MaxRate { get; set; }
        
    }
}
