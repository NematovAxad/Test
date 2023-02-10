using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserHandler.Commands.SecondSectionCommand
{
    public class SiteFailCommentCommand:IRequest<SiteFailCommentCommandResult>
    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int UserId { get; set; }
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
        [Required]
        public int OrganizationId { get; set; }
        [Required]
        public int DeadlineId { get; set; }
        public string Website { get; set; }
        public string ExpertComment { get; set; }
        public string ImagePath { get; set; }
    }
}
