using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.EighthSection
{
    [Table("organization_finance", Schema = "organizations")]
    public class OrganizationFinance : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }

        [Column("year")]
        public int Year { get; set; }


        [Column("plan_11")]
        public double Plan11 { get; set; }

        [Column("fact_11")]
        public double Fact11 { get; set; }
        [Column("plan_21")]
        public double Plan21 { get; set; }

        [Column("fact_21")]
        public double Fact21 { get; set; }
        [Column("plan_31")]
        public double Plan31 { get; set; }

        [Column("fact_31")]
        public double Fact31 { get; set; }
        [Column("plan_41")]
        public double Plan41 { get; set; }

        [Column("fact_41")]
        public double Fact41 { get; set; }
        [Column("plan_51")]
        public double Plan51 { get; set; }

        [Column("fact_51")]
        public double Fact51 { get; set; }
        [Column("plan_61")]
        public double Plan61 { get; set; }

        [Column("fact_61")]
        public double Fact61 { get; set; }
        [Column("plan_71")]
        public double Plan71 { get; set; }

        [Column("fact_71")]
        public double Fact71 { get; set; }
        [Column("plan_81")]
        public double Plan81 { get; set; }

        [Column("fact_81")]
        public double Fact81 { get; set; }

        [Column("all_plan_1")]
        public double AllPlan1 { get; set; }
        [Column("all_fact_1")]
        public double AllFact1 { get; set; }


        [Column("plan_12")]
        public double Plan12 { get; set; }

        [Column("fact_12")]
        public double Fact12 { get; set; }
        [Column("plan_22")]
        public double Plan22 { get; set; }

        [Column("fact_22")]
        public double Fact22 { get; set; }
        [Column("plan_32")]
        public double Plan32 { get; set; }

        [Column("fact_32")]
        public double Fact32 { get; set; }
        [Column("plan_42")]
        public double Plan42 { get; set; }

        [Column("fact_42")]
        public double Fact42 { get; set; }
        [Column("plan_52")]
        public double Plan52 { get; set; }

        [Column("fact_52")]
        public double Fact52 { get; set; }
        [Column("plan_62")]
        public double Plan62 { get; set; }

        [Column("fact_62")]
        public double Fact62 { get; set; }
        [Column("plan_72")]
        public double Plan72 { get; set; }

        [Column("fact_72")]
        public double Fact72 { get; set; }
        [Column("plan_82")]
        public double Plan82 { get; set; }

        [Column("fact_82")]
        public double Fact82 { get; set; }

        [Column("all_plan_2")]
        public double AllPlan2 { get; set; }
        [Column("all_fact_2")]
        public double AllFact2 { get; set; }



        [Column("plan_13")]
        public double Plan13 { get; set; }

        [Column("fact_13")]
        public double Fact13 { get; set; }
        [Column("plan_23")]
        public double Plan23 { get; set; }

        [Column("fact_23")]
        public double Fact23 { get; set; }
        [Column("plan_33")]
        public double Plan33 { get; set; }

        [Column("fact_33")]
        public double Fact33 { get; set; }
        [Column("plan_43")]
        public double Plan43 { get; set; }

        [Column("fact_43")]
        public double Fact43 { get; set; }
        [Column("plan_53")]
        public double Plan53 { get; set; }

        [Column("fact_53")]
        public double Fact53 { get; set; }
        [Column("plan_63")]
        public double Plan63 { get; set; }

        [Column("fact_63")]
        public double Fact63 { get; set; }
        [Column("plan_73")]
        public double Plan73 { get; set; }

        [Column("fact_73")]
        public double Fact73 { get; set; }
        [Column("plan_83")]
        public double Plan83 { get; set; }

        [Column("fact_83")]
        public double Fact83 { get; set; }

        [Column("all_plan_3")]
        public double AllPlan3 { get; set; }
        [Column("all_fact_3")]
        public double AllFact3 { get; set; }



        [Column("plan_14")]
        public double Plan14 { get; set; }

        [Column("fact_14")]
        public double Fact14 { get; set; }
        [Column("plan_24")]
        public double Plan24 { get; set; }

        [Column("fact_24")]
        public double Fact24 { get; set; }
        [Column("plan_34")]
        public double Plan34 { get; set; }

        [Column("fact_34")]
        public double Fact34 { get; set; }
        [Column("plan_44")]
        public double Plan44 { get; set; }

        [Column("fact_44")]
        public double Fact44 { get; set; }
        [Column("plan_54")]
        public double Plan54 { get; set; }

        [Column("fact_54")]
        public double Fact54 { get; set; }
        [Column("plan_64")]
        public double Plan64 { get; set; }

        [Column("fact_64")]
        public double Fact64 { get; set; }
        [Column("plan_74")]
        public double Plan74 { get; set; }

        [Column("fact_74")]
        public double Fact74 { get; set; }
        [Column("plan_84")]
        public double Plan84 { get; set; }

        [Column("fact_84")]
        public double Fact84 { get; set; }

        [Column("all_plan_4")]
        public double AllPlan4 { get; set; }
        [Column("all_fact_4")]
        public double AllFact4 { get; set; }

        [Column("user_pinfl")]
        public string UserPinfl { get; set; }

        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
