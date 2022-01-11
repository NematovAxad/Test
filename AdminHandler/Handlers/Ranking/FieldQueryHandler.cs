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
    public class FieldQueryHandler : IRequestHandler<FieldQuery, FieldQueryResult>
    {
        private readonly IRepository<Field, int> _field;

        public FieldQueryHandler(IRepository<Field, int> field)
        {
            _field = field;
        }

        public async Task<FieldQueryResult> Handle(FieldQuery request, CancellationToken cancellationToken)
        {
            var field = _field.GetAll();

            if (request.Id != 0)
            {
                field = field.Where(s => s.Id == request.Id);
            }

            FieldQueryResult result = new FieldQueryResult();
            result.Count = field.Count();
            result.Data = field.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
