using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models.SecondSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class SiteRequirementSampleQueryHandler : IRequestHandler<SiteRequirementSampleQuery, SiteRequirementSampleQueryResult>
    {
        private readonly IRepository<SiteRequirementsSample, int> _sample;
       
        public SiteRequirementSampleQueryHandler(IRepository<SiteRequirementsSample, int> sample)
        {
            _sample = sample;
        }
        public async Task<SiteRequirementSampleQueryResult> Handle(SiteRequirementSampleQuery request, CancellationToken cancellationToken)
        {
            var sample = _sample.GetAll().ToList();

            SiteRequirementSampleQueryResult result = new SiteRequirementSampleQueryResult();
           
            result.Data = sample.OrderBy(u => u.Number).ToList<SiteRequirementsSample>();
            return result;
        }
    }
}
