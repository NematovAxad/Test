﻿using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("website_requirements", Schema = "organizations")]
    public class WebSiteRequirements:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
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
        public Steps RequirementStatus { get; set; }
    }
}
