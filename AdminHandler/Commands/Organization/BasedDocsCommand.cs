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
        public BasedDocuments BasedDoc { get; set; }
        public IFormFile File { get; set; }
    }
}
