using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserHandler.Queries.SecondSectionQuery
{
    public class HelplineInfoQuery:IRequest<HelplineInfoQueryResult>
    {
        public int OrganizationId { get; set; }
    }
}
