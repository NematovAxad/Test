using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.ThirdSection
{
    [Table("organization_services", Schema = "organizations")]
    public class OrganizationServices:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }

        [Column("service_name_uz")]
        public string ServiceNameUz { get; set; }

        [Column("service_name_ru")]
        public string ServiceNameRu { get; set; }

        [Column("service_url")]
        public string ServiceUrl { get; set; }

        public ICollection<OrganizationServicesRate> Rates { get; set; }
    }
}
