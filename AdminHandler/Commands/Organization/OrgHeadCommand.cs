using AdminHandler.Results.Organization;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Organization
{
    public class OrgHeadCommand:IRequest<OrgHeadCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public IFormFile File { get; set; }
    }
}
