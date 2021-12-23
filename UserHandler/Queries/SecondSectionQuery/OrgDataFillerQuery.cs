using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserHandler.Queries.SecondSectionQuery
{
    public class OrgDataFillerQuery:IRequest<OrgDataFillerQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int DeadlineId { get; set; }
    }
}
