using AdminHandler.Results.Organization;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Organization
{
    public class SubOrgCommand:IRequest<SubOrgCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string DirectorMidName { get; set; }
        public string OwnerType { get; set; }
        public string OfficialSite { get; set; }
        public string Contacts { get; set; }
    }
}
