﻿using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("org_data_filler", Schema = "organizations")]
    public class OrgDataFiller:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("position")]
        public string Position { get; set; }
        [Column("contacts")]
        public string Contacts { get; set; }
        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
