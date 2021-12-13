using AdminHandler.Results.SecondOptionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.SecondOptionQueries
{
    public class ContentManagerQuery:IRequest<ContentManagerQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
