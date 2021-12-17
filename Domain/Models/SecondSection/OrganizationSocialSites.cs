using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("content_manager", Schema = "organizations")]
    public class OrganizationSocialSites:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
