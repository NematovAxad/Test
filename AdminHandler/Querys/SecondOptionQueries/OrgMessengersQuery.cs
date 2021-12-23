using AdminHandler.Results.SecondOptionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.SecondOptionQueries
{
    public class OrgMessengersQuery:IRequest<OrgMessengersQueryResult>
    {
        public int DeadlineId { get; set; }
        public int OrganizationId { get; set; }
        public int Id { get; set; }
    }
}
