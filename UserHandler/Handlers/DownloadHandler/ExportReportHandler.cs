using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Models;
using Domain.Models.Ranking;
using Domain.Models.Ranking.Administrations;
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
        private readonly IRepository<ARankTable, int> _aRankTable;
        private readonly IRepository<GSphere, int> _gSphere;
        private readonly IRepository<GField, int> _gField;
        private readonly IRepository<GSubField, int> _gSubField;
        private readonly IRepository<XSphere, int> _xSphere;
        private readonly IRepository<XField, int> _xField;
        private readonly IRepository<XSubField, int> _xSubField;
        private readonly IRepository<ASphere, int> _aSphere;
        private readonly IRepository<AField, int> _aField;
        private readonly IRepository<ASubField, int> _aSubField;

        public ExportReportHandler(IRepository<Organizations, int> organizations,
                                        IRepository<Deadline, int> deadline,
                                        IRepository<Field, int> field,
                                        IRepository<GRankTable, int> gRankTable,
                                        IRepository<XRankTable, int> xRankTable,
                                        IRepository<ARankTable, int> aRankTable,
                                        IRepository<GSphere, int> gSphere,
                                        IRepository<GField, int> gField,
                                        IRepository<GSubField, int> gSubField,
                                        IRepository<XSphere, int> xSphere,
                                        IRepository<XField, int> xField,
                                        IRepository<XSubField, int> xSubField,
                                        IRepository<ASphere, int> aSphere,
                                        IRepository<AField, int> aField,
                                        IRepository<ASubField, int> aSubField
                                        )
        {
            _organization = organizations;
            _deadline = deadline;
            _gRankTable = gRankTable;
            _xRankTable = xRankTable;
            _aRankTable = aRankTable;
            _gSphere = gSphere;
            _gField = gField;
            _gSubField = gSubField;
            _xSphere = xSphere;
            _xField = xField;
            _xSubField = xSubField;
            _aSphere = aSphere;
            _aField = aField;
            _aSubField = aSubField;
        }
        public List<XRankTable> xRankTable;
        public List<GRankTable> gRankTable;
        public async Task<ExportReportResult> Handle(ExportReportQuery request, CancellationToken cancellationToken)
        {
            xRankTable = _xRankTable.GetAll().ToList();
            gRankTable = _gRankTable.GetAll().ToList();
            ExportReportResult result = new ExportReportResult() { ItemGov = new List<ExportReportResultModelG>(), ItemXoz = new List<ExportReportResultModelX>() };
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
                    model.Count = result.ItemGov.Count() + 1;
                    model.OrgName = o.ShortName;

                    double rateSum = 0;
                    
                    model.FieldRate11 = FieldRateCalculateG(15, deadline, o).Result.IsException == false ? FieldRateCalculateG(15, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate12 = FieldRateCalculateG(2, deadline, o).Result.IsException == false ? FieldRateCalculateG(2, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate13 = FieldRateCalculateG(3, deadline, o).Result.IsException == false ? FieldRateCalculateG(3, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate14 = FieldRateCalculateG(4, deadline, o).Result.IsException == false ? FieldRateCalculateG(4, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate15 = FieldRateCalculateG(5, deadline, o).Result.IsException == false ? FieldRateCalculateG(5, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate16 = FieldRateCalculateG(6, deadline, o).Result.IsException == false ? FieldRateCalculateG(6, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate17 = FieldRateCalculateG(7, deadline, o).Result.IsException == false ? FieldRateCalculateG(7, deadline, o).Result.Rate.ToString() : exception;
                    model.SphereRate1 = SphereRateCalculateG(1, deadline, o).Result.Rate.ToString();
                    model.FieldRate21 = FieldRateCalculateG(8, deadline, o).Result.IsException == false ? FieldRateCalculateG(8, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate22 = FieldRateCalculateG(9, deadline, o).Result.IsException == false ? FieldRateCalculateG(9, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate23 = FieldRateCalculateG(10, deadline, o).Result.IsException == false ? FieldRateCalculateG(10, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate24 = FieldRateCalculateG(11, deadline, o).Result.IsException == false ? FieldRateCalculateG(11, deadline, o).Result.Rate.ToString() : exception;
                    model.SphereRate2 = SphereRateCalculateG(2, deadline, o).Result.Rate.ToString();
                    rateSum = Convert.ToDouble(model.SphereRate1) + Convert.ToDouble(model.SphereRate2) + SphereRateCalculateG(4, deadline, o).Result.Rate + SphereRateCalculateG(5, deadline, o).Result.Rate;
                    model.RateSum = rateSum.ToString();
                    model.RatePercent = Math.Round((rateSum / maxRate)*100, 2).ToString()+"%";
                    result.ItemGov.Add(model);
                }
            }
            if(request.Category == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                double maxRate = _gField.GetAll().Select(f => f.MaxRate).Sum() + 20;
                foreach(var o in org)
                {
                    ExportReportResultModelX model = new ExportReportResultModelX();
                    model.Count = result.ItemXoz.Count() + 1;
                    model.OrgName = o.ShortName;

                    double rateSumArifm = 0;
                    double rateSum = 0;
                    model.FieldRate23 = FieldRateCalculateX(1, deadline, o).Result.IsException == false ? FieldRateCalculateX(1, deadline, o).Result.Rate.ToString() : exception; 
                    model.FieldRate24 = FieldRateCalculateX(2, deadline, o).Result.IsException == false ? FieldRateCalculateX(2, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate25 = FieldRateCalculateX(3, deadline, o).Result.IsException == false ? FieldRateCalculateX(3, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate26 = FieldRateCalculateX(4, deadline, o).Result.IsException == false ? FieldRateCalculateX(4, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate27 = FieldRateCalculateX(5, deadline, o).Result.IsException == false ? FieldRateCalculateX(5, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate29 = FieldRateCalculateX(6, deadline, o).Result.IsException == false ? FieldRateCalculateX(6, deadline, o).Result.Rate.ToString() : exception;
                    model.FieldRate210 = FieldRateCalculateX(17, deadline, o).Result.IsException == false ? FieldRateCalculateX(17, deadline, o).Result.Rate.ToString() : exception;
                    model.Sphere1Arifm = ((FieldRateCalculateX(1, deadline, o).Result.IsException == false ? FieldRateCalculateX(1, deadline, o).Result.Rate : 0) +
                                            (FieldRateCalculateX(2, deadline, o).Result.IsException == false ? FieldRateCalculateX(2, deadline, o).Result.Rate : 0) +
                                            (FieldRateCalculateX(3, deadline, o).Result.IsException == false ? FieldRateCalculateX(3, deadline, o).Result.Rate : 0) +
                                            (FieldRateCalculateX(4, deadline, o).Result.IsException == false ? FieldRateCalculateX(4, deadline, o).Result.Rate : 0) +
                                            (FieldRateCalculateX(5, deadline, o).Result.IsException == false ? FieldRateCalculateX(5, deadline, o).Result.Rate : 0) +
                                            (FieldRateCalculateX(6, deadline, o).Result.IsException == false ? FieldRateCalculateX(6, deadline, o).Result.Rate : 0) +
                                            (FieldRateCalculateX(17, deadline, o).Result.IsException == false ? FieldRateCalculateX(17, deadline, o).Result.Rate : 0)).ToString();
                    rateSumArifm = rateSumArifm + Convert.ToDouble(model.Sphere1Arifm);
                    model.Sphere1All = SphereRateCalculateX(1, deadline, o).Result.Rate.ToString();
                    rateSum = rateSum + Convert.ToDouble(model.Sphere1All);


                    model.FieldRate31 = FieldRateCalculateX(7, deadline, o).Result.IsException == false ? FieldRateCalculateX(7, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate31!=exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate31);

                    model.FieldRate32 = FieldRateCalculateX(8, deadline, o).Result.IsException == false ? FieldRateCalculateX(8, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate32 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate32);
                    
                    model.FieldRate33 = FieldRateCalculateX(9, deadline, o).Result.IsException == false ? FieldRateCalculateX(9, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate33 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate33);

                    model.FieldRate34 = FieldRateCalculateX(10, deadline, o).Result.IsException == false ? FieldRateCalculateX(10, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate34 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate34);

                    model.Sphere2All = SphereRateCalculateX(2, deadline, o).Result.Rate.ToString();
                    rateSum = rateSum + Convert.ToDouble(model.Sphere2All);


                    model.FieldRate41 = exception;
                    model.FieldRate42 = exception;
                    model.FieldRate43 = exception;
                    model.Sphere3All = "0";


                    model.FieldRate511 = FieldRateCalculateX(18, deadline, o).Result.IsException == false ? FieldRateCalculateX(18, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate511 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate511);

                    model.FieldRate512 = FieldRateCalculateX(12, deadline, o).Result.IsException == false ? FieldRateCalculateX(12, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate512 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate512);

                    model.FieldRate513 = FieldRateCalculateX(14, deadline, o).Result.IsException == false ? FieldRateCalculateX(14, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate513 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate513);

                    model.FieldRate514 = FieldRateCalculateX(13, deadline, o).Result.IsException == false ? FieldRateCalculateX(13, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate514 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate514);

                    model.Sphere4All = SphereRateCalculateX(4, deadline, o).Result.Rate.ToString();
                    rateSum = rateSum + Convert.ToDouble(model.Sphere4All);


                    model.FieldRate61 = FieldRateCalculateX(21, deadline, o).Result.IsException == false ? FieldRateCalculateX(21, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate61 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate61);
                    model.FieldRate62 = FieldRateCalculateX(15, deadline, o).Result.IsException == false ? FieldRateCalculateX(15, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate62 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate62);
                    model.FieldRate63 = FieldRateCalculateX(16, deadline, o).Result.IsException == false ? FieldRateCalculateX(16, deadline, o).Result.Rate.ToString() : exception;
                    if (model.FieldRate63 != exception)
                        rateSumArifm = rateSumArifm + Convert.ToDouble(model.FieldRate63);
                    model.Sphere5All = SphereRateCalculateX(5, deadline, o).Result.Rate.ToString();
                    rateSum = rateSum + Convert.ToDouble(model.Sphere5All);


                    model.AllArifm = rateSumArifm.ToString();
                    model.Percentage = Math.Round((rateSum / maxRate) * 100, 2).ToString()+"%";
                    result.ItemXoz.Add(model);
                }
            }
            
            return result;
        }
        public async Task<SubFieldRateCalculateResult> SubFieldRateCalculateX(int subFieldId, Deadline deadline, Organizations o)
        {
            SubFieldRateCalculateResult result = new SubFieldRateCalculateResult();
            double subFieldRate = 0;
          
            var fieldRates = xRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SubFieldId == subFieldId).ToList();
            if (fieldRates.Count()>0 && fieldRates.All(r => r.IsException == true))
            {
                result.IsException = true;
            }
            if(fieldRates.Count() > 0 && !fieldRates.All(r => r.IsException == true))
            {
                result.IsException = false;
            }
            if (fieldRates.Count() > 1)
            {
                subFieldRate = subFieldRate + Math.Round(fieldRates.Select(r => r.Rank).Sum() / fieldRates.Count(), 2);
            }
            if (fieldRates.Count() == 1)
            {
                subFieldRate = subFieldRate + fieldRates.First().Rank;
            }
         
            result.Rate = subFieldRate;
            return result;
        }
        public async Task<SubFieldRateCalculateResult> SubFieldRateCalculateG(int subFieldId, Deadline deadline, Organizations o)
        {
            SubFieldRateCalculateResult result = new SubFieldRateCalculateResult();
            double subFieldRate = 0;
            
            var fieldRates = gRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SubFieldId == subFieldId).ToList();
            if (fieldRates.Count() > 0 && fieldRates.All(r => r.IsException == true))
            {
                result.IsException = true;
            }
            if (fieldRates.Count() > 0 && !fieldRates.All(r => r.IsException == true))
            {
                result.IsException = false;
            }
            if (fieldRates.Count() > 1)
            {
                subFieldRate = subFieldRate + Math.Round(fieldRates.Select(r => r.Rank).Sum() / fieldRates.Count(), 2);
            }
            if (fieldRates.Count() == 1)
            {
                subFieldRate = subFieldRate + fieldRates.First().Rank;
            }

            result.Rate = subFieldRate;
            return result;
        }
        public async Task<FieldRateCalculateResult> FieldRateCalculateX(int fieldId, Deadline deadline, Organizations o)
        {
            FieldRateCalculateResult result = new FieldRateCalculateResult();
            double fieldRate = 0;
            
            var fieldRates = xRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == fieldId).ToList();
            if (fieldRates.Count()>0 && fieldRates.All(r => r.IsException == true))
            {
                result.IsException = true;
            }
            else
            {
                result.IsException = false;
            }
            var subfields = _xSubField.Find(s => s.FieldId == fieldId).ToList();
            if (subfields.Count() > 0)
            {
                foreach (var sField in subfields)
                {
                    var ranks = xRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == fieldId && r.SubFieldId == sField.Id).ToList();
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
                var fieldR = xRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == fieldId).ToList();
                if (fieldR.Count() > 1)
                {
                    fieldRate = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                }
                if (fieldR.Count() == 1)
                {
                    fieldRate = fieldR.First().Rank;
                }
            }
            result.Rate = fieldRate;
            return result;
        }
        public async Task<FieldRateCalculateResult> FieldRateCalculateG(int fieldId, Deadline deadline, Organizations o)
        {
            FieldRateCalculateResult result = new FieldRateCalculateResult();
            double fieldRate = 0;
            
            var fieldRates = gRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == fieldId).ToList();
            if (fieldRates.All(r => r.IsException == true) && fieldRates.Count()>0)
            {
                result.IsException = true;
            }
            else
            {
                result.IsException = false;
            }
            var subfields = _gSubField.Find(s => s.FieldId == fieldId).ToList();
            if (subfields.Count() > 0)
            {
                foreach (var sField in subfields)
                {
                    var ranks = gRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == fieldId && r.SubFieldId == sField.Id).ToList();
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
                var fieldR = gRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == fieldId).ToList();
                if (fieldR.Count() > 1)
                {
                    fieldRate = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                }
                if (fieldR.Count() == 1)
                {
                    fieldRate = fieldR.First().Rank;
                }
            }
            result.Rate = fieldRate;
            return result;
        }
        public async Task<SphereRateCalculateResult> SphereRateCalculateX(int sphereId, Deadline deadline, Organizations o)
        {
            SphereRateCalculateResult result = new SphereRateCalculateResult();

            double sphereRate = 0;
            var fields = _xField.Find(f => f.SphereId == sphereId).ToList();
            var sphereRates = xRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == sphereId).ToList();
            if (sphereRates.Count()>0 && sphereRates.All(r => r.IsException == true))
            {
                result.IsException = true;
            }
            else
            {
                result.IsException = false;
            }
            foreach (var f in fields)
            {
                double fieldRate = 0;
                var subfields = _xSubField.Find(s => s.FieldId == f.Id).ToList();
                if (subfields.Count() > 0)
                {
                    foreach (var sField in subfields)
                    {
                        var ranks = xRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id && r.SubFieldId == sField.Id).ToList();
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
                    var fieldR = xRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id).ToList();
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
            result.Rate = sphereRate;
            return result;
        }
        public async Task<SphereRateCalculateResult> SphereRateCalculateG(int sphereId, Deadline deadline, Organizations o)
        {
            SphereRateCalculateResult result = new SphereRateCalculateResult();

            double sphereRate = 0;
            var fields = _gField.Find(f => f.SphereId == sphereId).ToList();
            var sphereRates = gRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.SphereId == sphereId).ToList();
            if(sphereRates.Count()>0 && sphereRates.All(r=>r.IsException == true))
            {
                result.IsException = true;
            }
            else
            {
                result.IsException = false;
            }
            foreach (var f in fields)
            {
                double fieldRate = 0;
                var subfields = _gSubField.Find(s => s.FieldId == f.Id).ToList();
                if (subfields.Count() > 0)
                {
                    foreach (var sField in subfields)
                    {
                        var ranks = gRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id && r.SubFieldId == sField.Id).ToList();
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
                    var fieldR = gRankTable.Where(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id).ToList();
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
            result.Rate = sphereRate;
            return result;
        }
    }
}
