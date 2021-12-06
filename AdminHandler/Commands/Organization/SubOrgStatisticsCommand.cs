using AdminHandler.Results.Organization;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Organization
{
    public class SubOrgStatisticsCommand:IRequest<SubOrgStatisticsCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int CentralManagements { get; set; }
        public int TerritorialManagements { get; set; }
        public int Subordinations { get; set; }
        public int Others { get; set; }
    }
}
