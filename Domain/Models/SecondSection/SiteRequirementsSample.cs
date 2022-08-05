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
        [Column("comment")]
        public string Comment { get; set; }
        [Column("site_link_1")]
        public string SiteLink1 { get; set; }
        [Column("screen_link_1")]
        public string ScreenLink1 { get; set; }
        [Column("site_link_2")]
        public string SiteLink2 { get; set; }
        [Column("screen_link_2")]
        public string ScreenLink2 { get; set; }
        [Column("site_link_3")]
        public string SiteLink3 { get; set; }
        [Column("screen_link_3")]
        public string ScreenLink3 { get; set; }
        [Column("status")]
        public Steps? RequirementStatus { get; set; }
    }
}
