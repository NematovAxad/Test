﻿using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("regions", Schema = "organizations")]
    public class Regions:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("parent_id")]
        public int ParentId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
    }
}
