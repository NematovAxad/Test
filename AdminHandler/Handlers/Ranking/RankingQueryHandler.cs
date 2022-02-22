using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Enums;
using Domain.Models;
using Domain.States;
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
        private readonly IRepository<XRankTable, int> _xRankTable;
        private readonly IRepository<GRankTable, int> _gRankTable;

        public RankingQueryHandler(IRepository<Organizations, int> organization, IRepository<RankTable, int> rankTable, IRepository<XRankTable, int> xRankTable, IRepository<GRankTable, int> gRankTable)
        {
            _organization = organization;
            _rankTable = rankTable;
            _gRankTable = gRankTable;
            _xRankTable = xRankTable;
        }

        public async Task<RankingQueryResult> Handle(RankingQuery request, CancellationToken cancellationToken)
        {
            RankingQueryResult result = new RankingQueryResult();

            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound("organization " + request.OrganizationId.ToString());

            if(org.OrgCategory == OrgCategory.FarmOrganizations)
            {
                var xRank = _xRankTable.GetAll();

                if (request.OrganizationId != 0)
                {
                    xRank = xRank.Where(r => r.OrganizationId == request.OrganizationId);
                }
                if (request.Year != 0)
                {
                    xRank = xRank.Where(r => r.Year == request.Year);
                }
                if (request.Quarter != 0)
                {
                    xRank = xRank.Where(r => r.Quarter == request.Quarter);
                }
                if (request.FieldId != 0)
                {
                    xRank = xRank.Where(r => r.FieldId == request.FieldId);
                }
                result.Count = xRank.Count();
                result.Data = xRank.OrderBy(u => u.Id).ToList<object>();
            }
            if (org.OrgCategory == OrgCategory.GovernmentOrganizations)
            {
                var gRank = _xRankTable.GetAll();

                if (request.OrganizationId != 0)
                {
                    gRank = gRank.Where(r => r.OrganizationId == request.OrganizationId);
                }
                if (request.Year != 0)
                {
                    gRank = gRank.Where(r => r.Year == request.Year);
                }
                if (request.Quarter != 0)
                {
                    gRank = gRank.Where(r => r.Quarter == request.Quarter);
                }
                if (request.FieldId != 0)
                {
                    gRank = gRank.Where(r => r.FieldId == request.FieldId);
                }
                result.Count = gRank.Count();
                result.Data = gRank.OrderBy(u => u.Id).ToList<object>();
            }

            
            return result;
        }
    }
}
