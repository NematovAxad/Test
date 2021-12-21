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
using UserHandler.Queries.DownloadQuery;
using UserHandler.Results.DownloadResult;

namespace UserHandler.Handlers.DownloadHandler
{
    public class ExportReportHandler : IRequestHandler<ExportReportQuery, ExportReportResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<RankTable, int> _rankTable;

        public ExportReportHandler(IRepository<Organizations, int> organizations, IRepository<Deadline, int> deadline, IRepository<RankTable, int> rankTable)
        {
            _organizations = organizations;
            _deadline = deadline;
            _rankTable = rankTable;
        }

        public async Task<ExportReportResult> Handle(ExportReportQuery request, CancellationToken cancellationToken)
        {
            ExportReportResult result = new ExportReportResult() { Item = new List<ExportReportResultModel>() };

            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(request.DeadlineId.ToString());

            var org = _organizations.GetAll().Include(mbox=>mbox.OrgRanks).ThenInclude(mbox=>mbox.Field).Where(o=>o.OrgRanks.All(r=>r.Quarter == deadline.Quarter && r.Year == deadline.Year));
            
            foreach(var o in org)
            {
                if(o.OrgRanks != null)
                {
                    ExportReportResultModel model = new ExportReportResultModel()
                    {
                        Count = result.Item.Count() + 1,
                        OrgName = o.ShortName,
                    };
                    if(o.OrgRanks.Where(r => r.FieldId == 1).FirstOrDefault() != null)
                        model.FieldRate11 = o.OrgRanks.Where(r => r.FieldId == 1).FirstOrDefault().Rank;
                    if(o.OrgRanks.Where(r => r.FieldId == 2).FirstOrDefault() != null)
                        model.FieldRate12 = o.OrgRanks.Where(r => r.FieldId == 2).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 3).FirstOrDefault() != null)
                        model.FieldRate13 = o.OrgRanks.Where(r => r.FieldId == 3).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 4).FirstOrDefault() != null)
                        model.FieldRate14 = o.OrgRanks.Where(r => r.FieldId == 4).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 5).FirstOrDefault() != null)
                        model.FieldRate15 = o.OrgRanks.Where(r => r.FieldId == 5).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 6).FirstOrDefault() != null)
                        model.FieldRate16 = o.OrgRanks.Where(r => r.FieldId == 6).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 7).FirstOrDefault() != null)
                        model.FieldRate17 = o.OrgRanks.Where(r => r.FieldId == 7).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.SphereId == 1).FirstOrDefault() != null)
                        model.SphereRate1 = o.OrgRanks.Where(r => r.SphereId == 1).Select(r => r.Rank).Sum();

                    if (o.OrgRanks.Where(r => r.FieldId == 8).FirstOrDefault() != null)
                        model.FieldRate21 = o.OrgRanks.Where(r => r.FieldId == 8).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 9).FirstOrDefault() != null)
                        model.FieldRate22 = o.OrgRanks.Where(r => r.FieldId == 9).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.SphereId == 2).FirstOrDefault() != null)
                        model.SphereRate2 = o.OrgRanks.Where(r => r.SphereId == 2).Select(r => r.Rank).Sum();

                    if (o.OrgRanks.Where(r => r.FieldId == 10).FirstOrDefault() != null)
                        model.FieldRate31 = o.OrgRanks.Where(r => r.FieldId == 10).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 11).FirstOrDefault() != null)
                        model.FieldRate41 = o.OrgRanks.Where(r => r.FieldId == 11).FirstOrDefault().Rank;
                    if (o.OrgRanks.Where(r => r.FieldId == 12).FirstOrDefault() != null)
                        model.FieldRate51 = o.OrgRanks.Where(r => r.FieldId == 12).FirstOrDefault().Rank;

                    if (o.OrgRanks != null)
                        model.RateSum = o.OrgRanks.Select(r => r.Rank).Sum();

                    if (o.OrgRanks != null)
                        model.RatePercent = Math.Round((o.OrgRanks.Select(r => r.Rank).Sum() / o.OrgRanks.Select(r => r.Field.MaxRate).Sum()) * 100, 2);
                    
                    result.Item.Add(model);
                }
            }
            return result;
        }
    }
}
