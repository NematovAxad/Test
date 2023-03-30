using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Queries.SixthSectionQueries
{
    public class SpecialForcesQuery : IRequest<SpecialForcesQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
