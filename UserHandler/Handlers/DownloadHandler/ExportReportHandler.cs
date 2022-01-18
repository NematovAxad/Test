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
            string exception = "ИСТИСНО";
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

                double maxRate = _gField.GetAll().Select(f => f.MaxRate).Sum() + 20;
                foreach(var o in org)
                {
                    ExportReportResultModelG model = new ExportReportResultModelG();
                    model.Count = result.Item.Count() + 1;
                    model.OrgName = o.ShortName;

                    double rateSum = 0;
                    var field = _gField.GetAll().ToList();
                    foreach(var f in field)
                    {
                        string fieldRank = "0";
                        var fRanks = _gRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id).ToList();

                        var subfields = _gSubField.Find(s => s.FieldId == f.Id).ToList();
                        if (subfields.Count() > 0)
                        {
                            if(fRanks.Count()>0 && fRanks.All(r=>r.IsException == true))
                            {
                                fieldRank = exception;
                            }
                            else
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
                                    fieldRank = (Convert.ToDouble(fieldRank) + sFieldRank).ToString();
                                }
                            }
                        }
                        else
                        {
                            if (fRanks.Count() > 1)
                            {
                                fieldRank = Math.Round(fRanks.Select(r => r.Rank).Sum() / fRanks.Count(), 2).ToString();
                            }
                            if (fRanks.Count() == 1)
                            {
                                fieldRank = fRanks.First().Rank.ToString();
                            }
                            if(fRanks.Count() > 0 && fRanks.All(r=>r.IsException == true))
                            {
                                fieldRank = exception;
                            }

                        }
                        switch (f.Id)
                        {
                            case 1:
                                model.FieldRate11 = fieldRank;
                                break;
                            case 2:
                                model.FieldRate12 = fieldRank;
                                break;
                            case 3:
                                model.FieldRate13 = fieldRank;
                                break;
                            case 4:
                                model.FieldRate14 = fieldRank;
                                break;
                            case 5:
                                model.FieldRate15 = fieldRank;
                                break;
                            case 6:
                                model.FieldRate16 = fieldRank;
                                break;
                            case 7:
                                model.FieldRate17 = fieldRank;
                                break;
                            case 8:
                                model.FieldRate21 = fieldRank;
                                break;
                            case 9:
                                model.FieldRate21 = fieldRank;
                                break;
                            case 10:
                                model.FieldRate21 = fieldRank;
                                break;
                            case 11:
                                model.FieldRate21 = fieldRank;
                                break;
                            case 12:
                                model.FieldRate22 = fieldRank;
                                break;
                            case 13:
                                model.FieldRate41 = fieldRank;
                                break;
                            case 14:
                                model.FieldRate51 = fieldRank;
                                break;
                        }
                    }

                    model.SphereRate1 = SphereRateCalculate(1, deadline, o).ToString();
                    model.SphereRate2 = SphereRateCalculate(2, deadline, o).ToString();
                    rateSum = Convert.ToDouble(model.SphereRate1) + Convert.ToDouble(model.SphereRate2) + SphereRateCalculate(4, deadline, o) + SphereRateCalculate(5, deadline, o);
                    model.RateSum = rateSum.ToString();
                    model.RatePercent = Math.Round((rateSum / maxRate)*100, 2).ToString()+"%";
                    result.Item.Add(model);
                }
            }
            
            
            return result;
        }
        public double SphereRateCalculate(int sphereId, Deadline deadline, Organizations o)
        {
            double sphereRate = 0;
            var fields = _gField.Find(f => f.SphereId == sphereId).ToList();
            foreach (var f in fields)
            {
                double fieldRate = 0;
                var subfields = _gSubField.Find(s => s.FieldId == f.Id).ToList();
                if (subfields.Count() > 0)
                {
                    foreach (var sField in subfields)
                    {
                        var ranks = _gRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id && r.SubFieldId == sField.Id).ToList();
                        if (ranks.Count() > 1)
                        {
                            fieldRate = fieldRate + Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                        }
                        if (ranks.Count() == 1)
                        {
                            fieldRate = fieldRate + ranks.First().Rank;
                        }
                    }

                }
                if (subfields.Count() == 0)
                {
                    var fieldR = _gRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id).ToList();
                    if (fieldR.Count() > 1)
                    {
                        fieldRate = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                    }
                    if (fieldR.Count() == 1)
                    {
                        fieldRate = fieldR.First().Rank;
                    }
                }

                sphereRate = sphereRate + fieldRate;
            }
            return sphereRate;
        }
    }
}
