using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.MygovModels
{
    [Table("mygov_exception_cases", Schema = "organizations")]
    public class MygovExceptionCases:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("mygov_service_id")]
        public int MygovServiceId { get; set; }


        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }

        public Organizations Organization { get; set; }
    }
}
