using AdminHandler.Results.SecondOptionResults;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.SecondOptionCommands
{
    public class ContentManagerCommand:IRequest<ContentManagerCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public IFormFile File { get; set; }
    }
}
