using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Models.SecondSection
{
    [Table("organization_messengers", Schema = "organizations")]
    public class OrganizationMessengers:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("messenger_link")]
        public string MessengerLink { get; set; }
        [Column("reason_not_filling")]
        public string ReasonNotFilling { get; set; }
    }
}
