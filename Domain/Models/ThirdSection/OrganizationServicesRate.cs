using Domain.Enums;
using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.ThirdSection
{
    [Table("organization_services_rate", Schema = "organizations")]
    public class OrganizationServicesRate:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }

        [Column("service_id")]
        [ForeignKey("OrganizationServices")]
        public int ServiceId { get; set; }
        public OrganizationServices OrganizationServices { get; set; }

        [Column("application_number")]
        public string ApplicationNumber { get; set; }

        [Column("has_application_problem")]
        public bool HasApplicationProblem { get; set; }

        [Column("application_problem_text")]
        public string ApplicationProblemText { get; set; }

        [Column("application_problem_confirmed")]
        public bool ApplicationProblemConfirmde { get; set; }

        [Column("recommend_service")]
        public bool RecommendService { get; set; }

        [Column("not_recommendation_comment")]
        public string NotRecommendationComment { get; set; }

        [Column("service_satisfactive")]
        public bool ServiceSatisfactive { get; set; }

        [Column("service_dissatisfaction_reason")]
        public string ServiceDissatisfactionReason { get; set; }

        [Column("service_dissatisfaction_confirmed")]
        public bool ServiceDissatisfactionConfirmed { get; set; }

        [Column("service_comment_type")]
        public CommentType ServiceCommentType { get; set; }

        [Column("service_comment")]
        public string ServiceComment { get; set; }

        [Column("service_comment_confirmed")]
        public bool ServiceCommentConfirmed { get; set; }

        [Column("service_rate")]
        public Rate ServiceRate { get; set; }

        [Column("expert_comment")]
        public string ExpertComment { get; set; }

        [Column("application_problem_text_exspert")]
        public string ApplicationProblemTextExspert { get; set; }

        [Column("service_dissatisfaction_confirmed_exspert")]
        public string ServiceDissatisfactionConfirmedExspert { get; set; }

        [Column("service_comment_confirmed_exspert")]
        public string ServiceCommentConfirmedExspert { get; set; }

        [Column("conversation_audio_link")]
        public string ConversationAudioLink { get; set; }
    }
}
