using AdminHandler.Results.Organization;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Organization
{
    public class BasedDocsCommand : IRequest<BasedDocsCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string DocumentNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public DocumentType DocumentType { get; set; }
        public CommanderOrgs AcceptedOrg { get; set; }
        public string DocumentName { get; set; }
        public IFormFile File { get; set; }
    }
}
