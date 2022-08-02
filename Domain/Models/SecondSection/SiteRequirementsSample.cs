using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("site_requirements_sample", Schema = "organizations")]
    public class SiteRequirementsSample:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("number")]
        public int Number { get; set; }
        [Column("site_link")]
        public string SiteLink { get; set; }
        [Column("ScreenLink")]
        public string ScreenLink { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("status")]
        public Steps? RequirementStatus { get; set; }
    }
}
