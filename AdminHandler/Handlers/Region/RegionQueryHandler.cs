using AdminHandler.Commands.Region;
using AdminHandler.Querys.Region;
using AdminHandler.Results.Region;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Region
{
    public class RegionQueryHandler : IRequestHandler<RegionQuery, RegionQueryResult>
    {
        private readonly IRepository<Regions, int> _regions;

        public RegionQueryHandler(IRepository<Regions, int> regions)
        {
            _regions = regions;
        }

        public async Task<RegionQueryResult> Handle(RegionQuery request, CancellationToken cancellationToken)
        {
            var reg = _regions.GetAll();

            if (request.Id != 0)
            {
                reg = reg.Where(o => o.Id == request.Id);
            }

            if (request.ParentId != 0)
            {
                reg = reg.Where(o => o.ParentId == request.ParentId);
            }
            

            RegionQueryResult result = new RegionQueryResult();
            result.Count = reg.Count();
            result.Data = reg.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
