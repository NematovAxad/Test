using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class OrganizationAppCommand:IRequest<OrganizationAppsCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public bool HasAndroidApp { get; set; }
        public string AndroidAppLink { get; set; }
        public bool HasIosApp { get; set; }
        public string IosAppLink { get; set; }
        public bool HasOtherApps { get; set; }
        public string OtherAppLink { get; set; }
        public bool HasResponsiveWebsite { get; set; }
    }
}
