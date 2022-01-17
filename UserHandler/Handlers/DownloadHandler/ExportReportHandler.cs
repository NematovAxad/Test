using Domain.Models;
using Domain.Models.Models;
using Domain.Models.Ranking;
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
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<GRankTable, int> _gRankTable;
        private readonly IRepository<XRankTable, int> _xRankTable;
        private readonly IRepository<GSphere, int> _gSphere;
        private readonly IRepository<GField, int> _gField;
        private readonly IRepository<GSubField, int> _gSubField;
        private readonly IRepository<XSphere, int> _xSphere;
        private readonly IRepository<XField, int> _xField;
        private readonly IRepository<XSubField, int> _xSubField;

        public ExportReportHandler(IRepository<Organizations, int> organizations,
                                        IRepository<Deadline, int> deadline,
                                        IRepository<Field, int> field,
                                        IRepository<GRankTable, int> gRankTable,
                                        IRepository<XRankTable, int> xRankTable,
                                        IRepository<GSphere, int> gSphere,
                                        IRepository<GField, int> gField,
                                        IRepository<GSubField, int> gSubField,
                                        IRepository<XSphere, int> xSphere,
                                        IRepository<XField, int> xField,
                                        IRepository<XSubField, int> xSubField
                                        )
        {
            _organization = organizations;
            _deadline = deadline;
            _gRankTable = gRankTable;
            _xRankTable = xRankTable;
            _gSphere = gSphere;
            _gField = gField;
            _gSubField = gSubField;
            _xSphere = xSphere;
            _xField = xField;
            _xSubField = xSubField;
        }

        public async Task<ExportReportResult> Handle(ExportReportQuery request, CancellationToken cancellationToken)
        {
            ExportReportResult result = new ExportReportResult() { Item = new List<ExportReportResultModelG>() };

            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(request.DeadlineId.ToString());

            var org = _organization.Find(o=>o.OrgCategory == request.Category).ToList();
            if (request.OrganizationId != 0)
            {
                org = org.Where(o => o.Id == request.OrganizationId).ToList();
            }
            
            if(request.Category == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                
                double maxRate = _gField.GetAll().Select(f => f.MaxRate).Sum();
                foreach(var o in org)
                {
                    ExportReportResultModelG model = new ExportReportResultModelG();
                    model.Count = result.Item.Count() + 1;
                    model.OrgName = o.ShortName;
                    double rateSum = 0;
                    var field = _gField.GetAll().ToList();
                    foreach(var f in field)
                    {
                        var subfields = _gSubField.Find(s => s.FieldId == f.Id).ToList();
                        if (subfields.Count() > 0)
                        {
                            foreach (var s in subfields)
                            {
                                double sFieldRank = 0;
                                var ranks = _gRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id && r.SubFieldId == s.Id).ToList();
                                if (ranks.Count() > 1)
                                {
                                    sFieldRank = Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                                }
                                if (ranks.Count() == 1)
                                {
                                    sFieldRank = ranks.First().Rank;
                                }
                                switch(f.Id)
                                {
                                    case 1: model.FieldRate11 = model.FieldRate11 + sFieldRank;
                                        break;
                                    case 2:
                                        model.FieldRate12 = model.FieldRate12 + sFieldRank;
                                        break;
                                    case 3:
                                        model.FieldRate13 = model.FieldRate13 + sFieldRank;
                                        break;
                                    case 4:
                                        model.FieldRate14 = model.FieldRate14 + sFieldRank;
                                        break;
                                    case 5:
                                        model.FieldRate15 = model.FieldRate15 + sFieldRank;
                                        break;
                                    case 6:
                                        model.FieldRate16 = model.FieldRate16 + sFieldRank;
                                        break;
                                    case 7:
                                        model.FieldRate17 = model.FieldRate17 + sFieldRank;
                                        break;
                                    case 8:
                                        model.FieldRate21 = model.FieldRate21 + sFieldRank;
                                        break;
                                    case 9:
                                        model.FieldRate21 = model.FieldRate21 + sFieldRank;
                                        break;
                                    case 10:
                                        model.FieldRate21 = model.FieldRate21 + sFieldRank;
                                        break;
                                    case 11:
                                        model.FieldRate21 = model.FieldRate21 + sFieldRank;
                                        break;
                                    case 12:
                                        model.FieldRate22 = model.FieldRate22 + sFieldRank;
                                        break;
                                    case 13:
                                        model.FieldRate41 = model.FieldRate41 + sFieldRank;
                                        break;
                                    case 14:
                                        model.FieldRate51 = model.FieldRate51 + sFieldRank;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            double fieldRank = 0;
                            var ranks = _gRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id).ToList();
                            if (ranks.Count() > 1)
                            {
                                fieldRank = Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                            }
                            if (ranks.Count() == 1)
                            {
                                fieldRank = ranks.First().Rank;
                            }
                            switch (f.Id)
                            {
                                case 1:
                                    model.FieldRate11 = model.FieldRate11 + fieldRank;
                                    break;
                                case 2:
                                    model.FieldRate12 = model.FieldRate12 + fieldRank;
                                    break;
                                case 3:
                                    model.FieldRate13 = model.FieldRate13 + fieldRank;
                                    break;
                                case 4:
                                    model.FieldRate14 = model.FieldRate14 + fieldRank;
                                    break;
                                case 5:
                                    model.FieldRate15 = model.FieldRate15 + fieldRank;
                                    break;
                                case 6:
                                    model.FieldRate16 = model.FieldRate16 + fieldRank;
                                    break;
                                case 7:
                                    model.FieldRate17 = model.FieldRate17 + fieldRank;
                                    break;
                                case 8:
                                    model.FieldRate21 = model.FieldRate21 + fieldRank;
                                    break;
                                case 9:
                                    model.FieldRate21 = model.FieldRate21 + fieldRank;
                                    break;
                                case 10:
                                    model.FieldRate21 = model.FieldRate21 + fieldRank;
                                    break;
                                case 11:
                                    model.FieldRate21 = model.FieldRate21 + fieldRank;
                                    break;
                                case 12:
                                    model.FieldRate22 = model.FieldRate22 + fieldRank;
                                    break;
                                case 13:
                                    model.FieldRate41 = model.FieldRate41 + fieldRank;
                                    break;
                                case 14:
                                    model.FieldRate51 = model.FieldRate51 + fieldRank;
                                    break;
                            }
                        }
                    }
                    model.SphereRate1 = model.FieldRate11 + model.FieldRate12 + model.FieldRate13 + model.FieldRate14 + model.FieldRate15 + model.FieldRate16 + model.FieldRate17;
                    model.SphereRate2 = model.FieldRate21 + model.FieldRate22;
                    rateSum = model.SphereRate1 + model.SphereRate2 + model.FieldRate31 + model.FieldRate41 + model.FieldRate51;
                    model.RateSum = rateSum;
                    model.RatePercent = Math.Round((rateSum / maxRate)*100, 2);
                    result.Item.Add(model);
                }
            }
            
            
            return result;
        }
    }
}
