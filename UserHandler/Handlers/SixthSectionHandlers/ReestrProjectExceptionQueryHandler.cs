using Domain.Models.FifthSection.ReestrModels;
using JohaRepository;
using MediatR;
using SB.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class ReestrProjectExceptionQueryHandler : IRequestHandler<ReestrProjectExceptionQuery, ReestrProjectExceptionQueryResult>
    {
        private readonly IRepository<ReestrProjectException, int> _projectExceptions;

        public ReestrProjectExceptionQueryHandler(IRepository<ReestrProjectException, int> projectExceptions)
        {
            _projectExceptions = projectExceptions;
        }

        public async Task<ReestrProjectExceptionQueryResult> Handle(ReestrProjectExceptionQuery request, CancellationToken cancellationToken)
        {
            var exceptions = _projectExceptions.Find(e=>e.OrganizationId == request.OrganizationId).ToList();

            ReestrProjectExceptionQueryResult result = new ReestrProjectExceptionQueryResult();

            result.reestrProjectExceptions = exceptions;

            return result;
        }
    }
}
