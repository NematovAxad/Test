using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ReestrPassportResult;

namespace UserHandler.Queries.ReestrPassportQuery
{
    public class ProjectExpertDecisionQuery: IRequest<ProjectExpertDecisionQueryResult>
    {
        public int OrgId { get; set; }
        public int ReestrProjectId { get; set; }
    }
}
