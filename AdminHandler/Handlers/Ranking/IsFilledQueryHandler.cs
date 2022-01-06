using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Models;
using Domain.Models.Ranking;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Ranking
{
    public class IsFilledQueryHandler : IRequestHandler<IsFilledQuery, IsFilledQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<IsFilledTable, int> _isFilled;

        public IsFilledQueryHandler(IRepository<Organizations, int> organization, IRepository<IsFilledTable, int> isFilled)
        {
            _organization = organization;
            _isFilled = isFilled;
        }
        public async Task<IsFilledQueryResult> Handle(IsFilledQuery request, CancellationToken cancellationToken)
        {
            IsFilledQueryResult result = new IsFilledQueryResult();

            var rank = _isFilled.GetAll();
            if (request.OrganizationId != 0)
            {
                rank = rank.Where(r => r.OrganizationId == request.OrganizationId);
            }
            if (request.Year != 0)
            {
                rank = rank.Where(r => r.Year == request.Year);
            }
            if (request.Quarter != 0)
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
