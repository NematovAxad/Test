using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Queries.ThirdSection
{
    public class ProjectsDelayQuery:IRequest<ProjectsDelayQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
