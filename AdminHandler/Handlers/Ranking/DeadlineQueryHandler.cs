using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;

using Domain.Models;

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
    public class DeadlineQueryHandler : IRequestHandler<DeadlineQuery, DeadlineQueryResult>
    {
        private readonly IRepository<Deadline, int> _deadline;

        public DeadlineQueryHandler(IRepository<Deadline, int> deadline)
        {
            _deadline = deadline;
        }

        public async Task<DeadlineQueryResult> Handle(DeadlineQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rank = _deadline.GetAll();
                if (request.Year != 0)
                {
                    rank = rank.Where(r => r.Year == request.Year);
                }
                if (request.Quarter != 0)
                {
                    rank = rank.Where(r => r.Quarter == request.Quarter);
                }
                if (request.IsActive == true)
                {
                    rank = rank.Where(r => r.IsActive == true);
                }

                DeadlineQueryResult result = new DeadlineQueryResult();
                result.Count = rank.Count();
                result.Data = rank.OrderBy(u => u.Id).ToList<object>();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;

        }
    }
}
