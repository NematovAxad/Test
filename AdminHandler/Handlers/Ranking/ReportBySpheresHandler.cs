using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
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

namespace AdminHandler.Handlers.Ranking
{
    public class ReportBySpheresHandler : IRequestHandler<ReportBySpheresQuery, ReportBySpheresResult>
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

        public ReportBySpheresHandler(IRepository<Organizations, int> organizations, 
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

        public async Task<ReportBySpheresResult> Handle(ReportBySpheresQuery request, CancellationToken cancellationToken)
        {
            ReportBySpheresResult result = new ReportBySpheresResult() {Count = 0, Data = new List<ReportBySpheresModel>() };

            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline id " + request.DeadlineId.ToString());
            var org = _organization.GetAll().ToList();
            if(request.OrganizationId != 0)
            {
                org = org.Where(o => o.Id == request.OrganizationId).ToList();
            }
            
            //ReportBySpheresModel model = new ReportBySpheresModel() {Spheres = new List<SphereRateElement>()};
           
            foreach (var o in org)
            {
                ReportBySpheresModel model = new ReportBySpheresModel() {OrganizationId = o.Id, OrgName = o.ShortName, Category = o.OrgCategory, Spheres = new List<SphereRateElement>() };
                if (o.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
                {
                    var spheres = _gSphere.GetAll().ToList();
                    double maxRate = 20;
                    double reached = 0;
                    foreach (var s in spheres)
                    {
                       
                        double sphereRate = 0;
                        var fields = _gField.Find(f => f.SphereId == s.Id).ToList();
                        foreach(var f in fields)
                        {
                            maxRate = maxRate + f.MaxRate;
                            double fieldRate = 0;
                            var subfields = _gSubField.Find(s => s.FieldId == f.Id).ToList();
                            if(subfields.Count()>0)
                            {
                                foreach(var sField in subfields)
                                {
                                    var ranks = _gRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id && r.SubFieldId == sField.Id).ToList();
                                    if(ranks.Count()>1)
                                    {
                                        fieldRate = fieldRate + Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                                    }
                                    if(ranks.Count()==1)
                                    {
                                        fieldRate = fieldRate + ranks.First().Rank;
                                    }
                                }
                                
                            }
                            if(subfields.Count()==0)
                            {
                                var fieldR = _gRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id).ToList();
                                if (fieldR.Count() > 1)
                                {
                                    fieldRate = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                                }
                                if(fieldR.Count()==1)
                                {
                                    fieldRate = fieldR.First().Rank;
                                }
                            }

                            sphereRate = sphereRate + fieldRate;
                        }
                        SphereRateElement addElement = new SphereRateElement();
                        addElement.SphereId = s.Id;
                        addElement.SphereName = s.Name;
                        addElement.SphereRate = sphereRate;
                        model.Spheres.Add(addElement);
                        reached = reached + sphereRate;
                    }
                    model.RateSum = reached;
                    model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                    result.Count = result.Count + 1;
                    result.Data.Add(model);
                }

                if (o.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
                {
                    var spheres = _xSphere.GetAll().ToList();
                    double maxRate = 0;
                    double reached = 0;
                    foreach (var s in spheres)
                    {

                        double sphereRate = 0;
                        var fields = _xField.Find(f => f.SphereId == s.Id).ToList();
                        foreach (var f in fields)
                        {
                            maxRate = maxRate + f.MaxRate;
                            double fieldRate = 0;
                            var subfields = _xSubField.Find(s => s.FieldId == f.Id).ToList();
                            if (subfields.Count() > 0)
                            {
                                foreach (var sField in subfields)
                                {
                                    var ranks = _xRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id && r.SubFieldId == sField.Id).ToList();
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
                                var fieldR = _xRankTable.Find(r => r.OrganizationId == o.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.FieldId == f.Id).ToList();
                                if (fieldR.Count > 0)
                                    fieldRate = fieldR.First().Rank;

                            }

                            sphereRate = sphereRate + fieldRate;
                        }
                        SphereRateElement addElement = new SphereRateElement();
                        addElement.SphereId = s.Id;
                        addElement.SphereName = s.Name;
                        addElement.SphereRate = sphereRate;
                        model.Spheres.Add(addElement);
                        reached = reached + sphereRate;
                    }
                    model.RateSum = reached;
                    model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                    result.Count = result.Count + 1;
                    result.Data.Add(model);
                }
            }
            return result;
        }
    }
}
