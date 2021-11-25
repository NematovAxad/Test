using AdminHandler.Results.Organization;
using Domain.Enums;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Organization
{
    public class OrgCommand:IRequest<OrgCommandResult>
    {
        public EventType EventType { get; set; }
        public Organizations Organization { get; set; }
    }
}
