using Domain.Enums;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UserHandler.Results.EigthSectionResult;

namespace UserHandler.Commands.EigthSectionCommand
{
    public class OrgFinanceCommand : IRequest<OrgFinanceCommandResult>
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


        public double Plan11 { get; set; }
        public double Fact11 { get; set; }
        public double Plan21 { get; set; }
        public double Fact21 { get; set; }
        public double Plan31 { get; set; }
        public double Fact31 { get; set; }
        public double Plan41 { get; set; }
        public double Fact41 { get; set; }
        public double Plan51 { get; set; }
        public double Fact51 { get; set; }
        public double Plan61 { get; set; }
        public double Fact61 { get; set; }
        public double Plan71 { get; set; }
        public double Fact71 { get; set; }
        public double Plan81 { get; set; }
        public double Fact81 { get; set; }



        public double Plan12 { get; set; }
        public double Fact12 { get; set; }
        public double Plan22 { get; set; }
        public double Fact22 { get; set; }
        public double Plan32 { get; set; }
        public double Fact32 { get; set; }
        public double Plan42 { get; set; }
        public double Fact42 { get; set; }
        public double Plan52 { get; set; }
        public double Fact52 { get; set; }
        public double Plan62 { get; set; }
        public double Fact62 { get; set; }
        public double Plan72 { get; set; }
        public double Fact72 { get; set; }
        public double Plan82 { get; set; }
        public double Fact82 { get; set; }


        public double Plan13 { get; set; }
        public double Fact13 { get; set; }
        public double Plan23 { get; set; }
        public double Fact23 { get; set; }
        public double Plan33 { get; set; }
        public double Fact33 { get; set; }
        public double Plan43 { get; set; }
        public double Fact43 { get; set; }
        public double Plan53 { get; set; }
        public double Fact53 { get; set; }
        public double Plan63 { get; set; }
        public double Fact63 { get; set; }
        public double Plan73 { get; set; }
        public double Fact73 { get; set; }
        public double Plan83 { get; set; }
        public double Fact83 { get; set; }


        public double Plan14 { get; set; }
        public double Fact14 { get; set; }
        public double Plan24 { get; set; }
        public double Fact24 { get; set; }
        public double Plan34 { get; set; }
        public double Fact34 { get; set; }
        public double Plan44 { get; set; }
        public double Fact44 { get; set; }
        public double Plan54 { get; set; }
        public double Fact54 { get; set; }
        public double Plan64 { get; set; }
        public double Fact64 { get; set; }
        public double Plan74 { get; set; }
        public double Fact74 { get; set; }
        public double Plan84 { get; set; }
        public double Fact84 { get; set; }
    }
}
