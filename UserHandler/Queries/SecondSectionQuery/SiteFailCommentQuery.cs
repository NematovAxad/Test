using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserHandler.Queries.SecondSectionQuery
{
    public class SiteFailCommentQuery:IRequest<SiteFailCommentQueryResult>
    {
        [Required]
        public int OrgId { get; set; }
        [Required]
        public int DeadlineId { get; set; }
    }
}
