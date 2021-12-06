using AdminHandler.Results.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Organization
{
    public class EmployeeStatisticsQuery:IRequest<EmployeeStatisticsQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
