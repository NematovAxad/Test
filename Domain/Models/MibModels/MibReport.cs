using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.MibModels
{
    [Table("mib_report", Schema = "organizations")]
    public class MibReport:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("api_name")]
        public string ApiName { get; set; }

        [Column("owner_inn")]
        public string OwnerInn { get; set; }

        [Column("api_description")]
        public string ApiDescription { get; set; }

        [Column("api_version")]
        public string ApiVersion { get; set; }

        [Column("success_count")]
        public int SuccessCount { get; set; }

        [Column("fail_count")]
        public int FailCount { get; set; }

        [Column("overall")]
        public int Overall { get; set; }

        [Column("success_share")]
        public double SuccessShare { get; set; }

    }
}
