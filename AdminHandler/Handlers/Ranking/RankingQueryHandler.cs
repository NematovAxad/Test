using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Enums;
using Domain.Models;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Ranking
{
    public class RankingQueryHandler : IRequestHandler<RankingQuery, RankingQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<RankTable, int> _rankTable;

        public RankingQueryHandler(IRepository<Organizations, int> organization, IRepository<RankTable, int> rankTable)
        {
            _organization = organization;
            _rankTable = rankTable;
        }

        public async Task<RankingQueryResult> Handle(RankingQuery request, CancellationToken cancellationToken)
        {
            RankingQueryResult result = new RankingQueryResult();

            var rank = _rankTable.GetAll();
            if(request.OrganizationId != 0)
            {
                rank = rank.Where(r => r.OrganizationId == request.OrganizationId);
            }
            if(request.Year != 0)
            {
                rank = rank.Where(r => r.Year == request.Year);
            }
            if(request.Quarter!=0)
            {
                rank = rank.Where(r => r.Quarter == request.Quarter);
            }
            if (request.FieldId != 0)
            {
                rank = rank.Where(r => r.FieldId == request.FieldId);
            }
            result.Count = rank.Count();
            result.Data = rank.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
