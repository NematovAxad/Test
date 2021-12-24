using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserHandler.Queries.SecondSectionQuery
{
    public class OrgHelplineQuery:IRequest<OrgHelplineQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
