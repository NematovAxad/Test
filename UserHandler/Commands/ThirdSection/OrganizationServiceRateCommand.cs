using Domain.Enums;
using Domain.Models.FirstSection;
using Domain.Models.ThirdSection;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Commands.ThirdSection
{
    public class OrganizationServiceRateCommand:IRequest<OrganizationServiceRateCommandResult>
    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string UserPinfl { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserOrgId { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<string> UserPermissions { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public EventType EventType { get; set; }

        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public int ServiceId { get; set; }

        public string ApplicationNumber { get; set; }

        public bool HasApplicationProblem { get; set; }

        public string ApplicationProblemText { get; set; }

        public bool ApplicationProblemConfirmde { get; set; }

        public bool RecommendService { get; set; }

        public string NotRecommendationComment { get; set; }

        public bool ServiceSatisfactive { get; set; }

        public string ServiceDissatisfactionReason { get; set; }

        public bool ServiceDissatisfactionConfirmed { get; set; }

        public CommentType ServiceCommentType { get; set; }

        public string ServiceComment { get; set; }

        public bool ServiceCommentConfirmed { get; set; }

        public Rate ServiceRate { get; set; }

        public string ExpertComment { get; set; }

        public string ApplicationProblemTextExspert { get; set; }

        public string ServiceDissatisfactionConfirmedExspert { get; set; }

        public string ServiceCommentConfirmedExspert { get; set; }

        public string ConversationAudioLink { get; set; }
    }
}
