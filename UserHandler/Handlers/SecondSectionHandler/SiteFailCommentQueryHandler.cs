using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SecondSectionQuery;
using UserHandler.Results.SecondSectionQueryResult;
using System.Linq.Dynamic.Core;
using System.Linq;
using Domain.Models.FirstSection;

namespace UserHandler.Handlers.SecondSectionHandler
{
    public class SiteFailCommentQueryHandler : IRequestHandler<SiteFailCommentQuery, SiteFailCommentQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<SiteFailComments, int> _fails;

        public SiteFailCommentQueryHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<SiteFailComments, int> fails)
        {
            _organization = organization;
            _deadline = deadline;
            _fails = fails;
        }

        public async Task<SiteFailCommentQueryResult> Handle(SiteFailCommentQuery request, CancellationToken cancellationToken)
        {
            var fails = _fails.Find(f=>f.OrganizationId == request.OrgId && f.DeadlineId == request.DeadlineId).ToList();

            SiteFailCommentQueryResult result = new SiteFailCommentQueryResult();

            result.Count = fails.Count();
            result.Data = fails;

            return result;
        }
    }
}
