using Domain.Models;
using Domain.Models.SixthSection;
using JohaRepository;
using MediatR;
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
    public class SpecialForcesQueryHandler : IRequestHandler<SpecialForcesQuery, SpecialForcesQueryResult>
    {
        private readonly IRepository<OrganizationIctSpecialForces, int> _specialForces;

        public SpecialForcesQueryHandler(IRepository<OrganizationIctSpecialForces, int> specialForces)
        {
            _specialForces = specialForces;
        }
        public async Task<SpecialForcesQueryResult> Handle(SpecialForcesQuery request, CancellationToken cancellationToken)
        {
            var specialForces = _specialForces.GetAll();
            if (request.Id != 0)
            {
                specialForces = specialForces.Where(s => s.Id == request.Id);
            }
            if (request.OrganizationId != 0)
            {
                specialForces = specialForces.Where(s => s.OrganizationId == request.OrganizationId);
            }

            SpecialForcesQueryResult result = new SpecialForcesQueryResult();
            result.Count = specialForces.Count();
            result.Data = specialForces.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
