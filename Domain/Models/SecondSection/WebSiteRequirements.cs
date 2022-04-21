using Domain.Enums;
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
        [Column("requirement1")]
        public Steps Requirement1 { get; set; }
        [Column("requirement2")]
        public Steps Requirement2 { get; set; }
        [Column("requirement3")]
        public Steps Requirement3 { get; set; }
        [Column("requirement4")]
        public Steps Requirement4 { get; set; }
        [Column("requirement5")]
        public Steps Requirement5 { get; set; }
        [Column("requirement6")]
        public Steps Requirement6 { get; set; }
        [Column("requirement7")]
        public Steps Requirement7 { get; set; }
        [Column("requirement8")]
        public Steps Requirement8 { get; set; }
        [Column("requirement9")]
        public Steps Requirement9 { get; set; }
        [Column("requirement10")]
        public Steps Requirement10 { get; set; }
        [Column("requirement11")]
        public Steps Requirement11 { get; set; }
        [Column("requirement12")]
        public Steps Requirement12 { get; set; }
        [Column("requirement13")]
        public Steps Requirement13 { get; set; }
        [Column("requirement14")]
        public Steps Requirement14 { get; set; }
        [Column("requirement15")]
        public Steps Requirement15 { get; set; }
        [Column("requirement16")]
        public Steps Requirement16 { get; set; }
        [Column("requirement17")]
        public Steps Requirement17 { get; set; }
        [Column("requirement18")]
        public Steps Requirement18 { get; set; }
        [Column("requirement19")]
        public Steps Requirement19 { get; set; }
        [Column("requirement20")]
        public Steps Requirement20 { get; set; }
        [Column("requirement21")]
        public Steps Requirement21 { get; set; }
        [Column("requirement22")]
        public Steps Requirement22 { get; set; }
        [Column("requirement23")]
        public Steps Requirement23 { get; set; }
        [Column("requirement24")]
        public Steps Requirement24 { get; set; }
        [Column("requirement25")]
        public Steps Requirement25 { get; set; }
        [Column("requirement26")]
        public Steps Requirement26 { get; set; }
        [Column("requirement27")]
        public Steps Requirement27 { get; set; }
        [Column("requirement28")]
        public Steps Requirement28 { get; set; }
        [Column("requirement29")]
        public Steps Requirement29 { get; set; }
        [Column("requirement30")]
        public Steps Requirement30 { get; set; }
        [Column("requirement31")]
        public Steps Requirement31 { get; set; }
        [Column("requirement32")]
        public Steps Requirement32 { get; set; }
        [Column("requirement33")]
        public Steps Requirement33 { get; set; }
        [Column("requirement34")]
        public Steps Requirement34 { get; set; }
        [Column("requirement35")]
        public Steps Requirement35 { get; set; }
        [Column("requirement36")]
        public Steps Requirement36 { get; set; }
        [Column("requirement37")]
        public Steps Requirement37 { get; set; }
        [Column("requirement38")]
        public Steps Requirement38 { get; set; }
        [Column("requirement39")]
        public Steps Requirement39 { get; set; }
        [Column("requirement40")]
        public Steps Requirement40 { get; set; }
        [Column("requirement41")]
        public Steps Requirement41 { get; set; }
        [Column("requirement42")]
        public Steps Requirement42 { get; set; }
        [Column("requirement43")]
        public Steps Requirement43 { get; set; }
        [Column("requirement44")]
        public Steps Requirement44 { get; set; }
        [Column("requirement45")]
        public Steps Requirement45 { get; set; }
    }
}
