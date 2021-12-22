using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Models.SecondSection
{
    [Table("org_social_sites", Schema = "organizations")]
    public class OrganizationMessengers:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Organizations Organizations { get; set; }
        [Column("field_id")]
        [ForeignKey("Field")]
        public int FieldId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Field Field { get; set; }
        [Column("deadline_id")]
        [ForeignKey("Deadline")]
        public int DeadlineId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Deadline Deadline { get; set; }
        [Column("messenger_link")]
        public string MessengerLink { get; set; }
        [Column("reason_not_filling")]
        public string ReasonNotFilling { get; set; }
    }
}
