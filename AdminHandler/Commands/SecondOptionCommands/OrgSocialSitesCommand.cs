using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class OrgSocialSitesCommand:IRequest<OrgSocialSitesCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int FieldId { get; set; }
        public int DeadlineId { get; set; }
        public string SocialSiteLink { get; set; }
    }
}
