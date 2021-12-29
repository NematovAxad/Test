using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Models;
using Domain.Models.Models;
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
    public class ReportBySpheresHandler : IRequestHandler<ReportBySpheresQuery, ReportBySpheresResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;

        public ReportBySpheresHandler(IRepository<Organizations, int> organizations, IRepository<Deadline, int> deadline, IRepository<Field, int> field)
        {
            _organization = organizations;
            _deadline = deadline;
            _field = field;
        }

        public async Task<ReportBySpheresResult> Handle(ReportBySpheresQuery request, CancellationToken cancellationToken)
        {
            ReportBySpheresResult result = new ReportBySpheresResult() {Count = 0, Data = new List<ReportBySpheresModel>() };

            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline id " + request.DeadlineId.ToString());
            var org = _organization.GetAll().Include(mbox => mbox.OrgRanks).ThenInclude(mbox => mbox.Field).Select(o => new {Id = o.Id, ShortName = o.ShortName, Category = o.OrgCategory, OrgRanks = o.OrgRanks.Where(r => r.Quarter == deadline.Quarter && r.Year == deadline.Year)});
            if(request.OrganizationId != 0)
            {
                org = org.Where(o => o.Id == request.OrganizationId);
            }
            double maxRate = _field.GetAll().Select(f => f.MaxRate).Sum();
            foreach (var o in org)
            {
                if (o.OrgRanks != null)
                {
                    ReportBySpheresModel model = new ReportBySpheresModel()
                    {
                        OrganizationId = o.Id,
                        OrgName = o.ShortName,
                        Category = o.Category
                    };
                    if (o.OrgRanks.Where(r => r.SphereId == 1).FirstOrDefault() != null)
                        model.SphereRate1 = o.OrgRanks.Where(r => r.SphereId == 1).Select(r => r.Rank).Sum();

                    if (o.OrgRanks.Where(r => r.SphereId == 2).FirstOrDefault() != null)
                        model.SphereRate2 = o.OrgRanks.Where(r => r.SphereId == 2).Select(r => r.Rank).Sum();

                    if (o.OrgRanks.Where(r => r.SphereId == 3).FirstOrDefault() != null)
                        model.SphereRate3 = o.OrgRanks.Where(r => r.SphereId == 3).Select(r => r.Rank).Sum();

                    if (o.OrgRanks.Where(r => r.SphereId == 4).FirstOrDefault() != null)
                        model.SphereRate4 = o.OrgRanks.Where(r => r.SphereId == 4).Select(r => r.Rank).Sum();

                    if (o.OrgRanks.Where(r => r.SphereId == 5).FirstOrDefault() != null)
                        model.SphereRate5 = o.OrgRanks.Where(r => r.SphereId == 5).Select(r => r.Rank).Sum();

                    if (o.OrgRanks != null)
                    {
                        model.RatePercent = Math.Round((o.OrgRanks.Select(r => r.Rank).Sum() / maxRate) * 100, 2);
                        model.RateSum = Math.Round(o.OrgRanks.Select(r => r.Rank).Sum(), 2);
                    }

                    result.Count += 1;
                    result.Data.Add(model);
                }
            }
            return result;
        }
    }
}
