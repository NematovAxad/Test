﻿using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
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

namespace AdminHandler.Handlers.Ranking
{
    public class ReportBySpheresHandler : IRequestHandler<ReportBySpheresQuery, ReportBySpheresResult>
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

        public ReportBySpheresHandler(IRepository<Organizations, int> organizations, 
                                        IRepository<Deadline, int> deadline, 
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

        public async Task<ReportBySpheresResult> Handle(ReportBySpheresQuery request, CancellationToken cancellationToken)
        {
            var deadline = _deadline.Find(d => d.Id == request.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline id " + request.DeadlineId.ToString());
            var org = _organization.Find(o=>o.IsActive == true && o.IsIct == true).OrderBy(o=>o.Id).ToList();
            if (request.OrganizationId != 0)
            {
                org = org.Where(o => o.Id == request.OrganizationId).ToList();
            }

            ReportBySpheresResult result = new ReportBySpheresResult() {Count = 0, Data = new List<ReportBySpheresModel>() };
            var gSpheres = _gSphere.GetAll().ToList();
            var xSpheres = _xSphere.GetAll().ToList();
            var aSpheres = _aSphere.GetAll().ToList();
            var gFields = _gField.GetAll().ToList();
            var xFields = _xField.GetAll().ToList();
            var aFields = _aField.GetAll().ToList();
            var gSubFields = _gSubField.GetAll().ToList();
            var xSubFields = _xSubField.GetAll().ToList();
            var aSubFields = _aSubField.GetAll().ToList();
            var gRankTable = _gRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();
            var xRankTable = _xRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();
            var aRankTable = _aRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();



            //ReportBySpheresModel model = new ReportBySpheresModel() {Spheres = new List<SphereRateElement>()};

            foreach (var o in org)
            {
                ReportBySpheresModel model = new ReportBySpheresModel() {OrganizationId = o.Id, OrgName = o.ShortName, OrgNameRu = o.ShortNameRu, UserServiceId = o.UserServiceId, Category = o.OrgCategory, Spheres = new List<SphereRateElement>() };
                if (o.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
                {
                    if(o.Id==106)
                    {

                    }
                    
                    double maxRate = 0;
                    double reached = 0;
                    foreach (var sphere in gSpheres)
                    {
                        maxRate = maxRate + sphere.MaxRate;

                        double sphereRate = 0;
                        var fields = gFields.Where(f => f.SphereId == sphere.Id).ToList();
                        foreach(var field in fields)
                        {
                            
                            double fieldRate = 0;
                            var subfields = gSubFields.Where(s => s.FieldId == field.Id).ToList();
                            if(subfields.Any())
                            {
                                foreach(var sField in subfields)
                                {
                                    var ranks = gRankTable.Where(a => a.OrganizationId == o.Id && a.SphereId == sphere.Id && a.FieldId == field.Id && a.SubFieldId == sField.Id);
                                    if(ranks.Count()>1)
                                    {
                                        fieldRate += Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                                    }
                                    if(ranks.Count()==1)
                                    {
                                        fieldRate += ranks.First().Rank;
                                    }
                                }
                                
                            }
                            if(subfields.Count()==0)
                            {
                                var fieldR = gRankTable.Where(r => r.OrganizationId == o.Id && r.SphereId == sphere.Id && r.FieldId == field.Id);
                                if (fieldR.Count() > 1)
                                {
                                    fieldRate = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                                }
                                if(fieldR.Count()==1)
                                {
                                    fieldRate = fieldR.First().Rank;
                                }
                            }

                            if(field.Section == "3.4")
                            {
                                sphereRate -= fieldRate;
                            }
                            else
                            {
                                sphereRate += fieldRate;
                            }  
                        }

                        if (sphereRate < 0)
                        {
                            sphereRate = 0;
                        }

                        SphereRateElement addElement = new SphereRateElement();
                        addElement.SphereId = sphere.Id;
                        addElement.SphereName = sphere.Name;
                        addElement.SphereSection = sphere.Section;
                        addElement.SphereRate = sphereRate;
                        model.Spheres.Add(addElement);
                        reached += sphereRate;
                    }
                    model.RateSum = reached;
                    model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                    result.Count = result.Count + 1;
                    result.Data.Add(model);
                }

                if (o.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
                {
                    double maxRate = 0;
                    double reached = 0;
                    foreach (var sphere in xSpheres)
                    {
                        maxRate = maxRate + sphere.MaxRate;

                        double sphereRate = 0;
                        var fields = xFields.Where(f => f.SphereId == sphere.Id).ToList();
                        foreach (var field in fields)
                        {
                            
                            double fieldRate = 0;
                            var subfields = xSubFields.Where(s => s.FieldId == field.Id);
                            if (subfields.Count() > 0)
                            {
                                foreach (var sfield in subfields)
                                {
                                    var ranks = xRankTable.Where(a => a.OrganizationId == o.Id && a.SphereId == sphere.Id && a.FieldId == field.Id && a.SubFieldId == sfield.Id);
                                    if (ranks.Count() > 1)
                                    {
                                        fieldRate += Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                                    }
                                    if (ranks.Count() == 1)
                                    {
                                        fieldRate += ranks.First().Rank;
                                    }
                                }
                            }
                            if (subfields.Count() == 0)
                            {
                                var fieldR = xRankTable.Where(r => r.OrganizationId == o.Id && r.SphereId == sphere.Id && r.FieldId == field.Id);
                                if (fieldR.Count() > 1)
                                {
                                    fieldRate = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                                }
                                if (fieldR.Count() == 1)
                                {
                                    fieldRate = fieldR.First().Rank;
                                }
                            }

                            sphereRate += fieldRate;
                        }
                        SphereRateElement addElement = new SphereRateElement();
                        addElement.SphereId = sphere.Id;
                        addElement.SphereName = sphere.Name;
                        addElement.SphereSection = sphere.Section;
                        addElement.SphereRate = sphereRate;
                        model.Spheres.Add(addElement);
                        reached += sphereRate;
                    }
                    model.RateSum = reached;
                    model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                    result.Count++;
                    result.Data.Add(model);
                }
                if (o.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
                {

                    double maxRate = 0;
                    double reached = 0;
                    foreach (var sphere in aSpheres)
                    {
                        maxRate = maxRate + sphere.MaxRate;

                        double sphereRate = 0;
                        var fields = aFields.Where(f => f.SphereId == sphere.Id).ToList();
                        foreach (var field in fields)
                        {
                            
                            double fieldRate = 0;
                            var subfields = aSubFields.Where(s => s.FieldId == field.Id).ToList();
                            if (subfields.Count() > 0)
                            {
                                foreach (var sfield in subfields)
                                {
                                    var ranks = aRankTable.Where(a => a.OrganizationId == o.Id && a.SphereId == sphere.Id && a.FieldId == field.Id && a.SubFieldId == sfield.Id);
                                    if (ranks.Count() > 1)
                                    {
                                        fieldRate += Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                                    }
                                    if (ranks.Count() == 1)
                                    {
                                        fieldRate += ranks.First().Rank;
                                    }
                                }

                            }
                            if (subfields.Count() == 0)
                            {
                                var fieldR = aRankTable.Where(r => r.OrganizationId == o.Id && r.SphereId == sphere.Id && r.FieldId == field.Id);
                                if (fieldR.Count() > 1)
                                {
                                    fieldRate = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                                }
                                if (fieldR.Count() == 1)
                                {
                                    fieldRate = fieldR.First().Rank;
                                }
                            }

                            sphereRate += fieldRate;
                        }
                        SphereRateElement addElement = new SphereRateElement();
                        addElement.SphereId = sphere.Id;
                        addElement.SphereName = sphere.Name;
                        addElement.SphereSection = sphere.Section;
                        addElement.SphereRate = sphereRate;
                        model.Spheres.Add(addElement);
                        reached += sphereRate;
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
