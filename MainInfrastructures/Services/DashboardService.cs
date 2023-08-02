using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain;
using Domain.Enums;
using Domain.Models;
using Domain.Models.DashboardModels;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Models.Models;
using Domain.Models.Ranking;
using Domain.Models.Ranking.Administrations;
using Domain.Models.ThirdSection;
using Domain.ReesterModels;
using Domain.States;
using JohaRepository;
using MainInfrastructures.Interfaces;
using MainInfrastructures.Migrations;

namespace MainInfrastructures.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<OrganizationPublicServices, int> _organizationPublicServices;
        private readonly IRepository<ReestrProjectException, int> _reestrProjectException;
        private readonly IRepository<ReestrProjectPosition, int> _reestrProjectPosition;
        private readonly IRepository<ReestrProjectExpertDecision, int> _reestrProjectExpertDecision;
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
        private readonly IReesterService _reesterService;

        public DashboardService(IRepository<Organizations, int> organizations, 
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
                                        IRepository<ASubField, int> aSubField,
                                        IRepository<OrganizationPublicServices, int> organizationPublicServices,
                                        IRepository<ReestrProjectException, int> reestrProjectException,
                                        IRepository<ReestrProjectPosition, int> reestrProjectPosition,
                                        IReesterService reesterService,
                                        IRepository<ReestrProjectExpertDecision, int> reestrProjectExpertDecision
                                        )
        {
            _organization = organizations;
            _organizationPublicServices = organizationPublicServices;
            _reestrProjectException = reestrProjectException;
            _reestrProjectPosition = reestrProjectPosition;
            _reestrProjectExpertDecision = reestrProjectExpertDecision;
            _reesterService = reesterService;
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
        
        public async Task<DashboardResultModel> GetDashboardData()
        {
            DashboardResultModel result = new DashboardResultModel();
            
            var deadline = _deadline.Find(d => d.Dashboard == true).OrderBy(i => i.Id).LastOrDefault();

            if (deadline == null)
                return await Task.FromResult(result);

            result.RatedServicesReport = await GetRatedServicesReport(deadline);
            result.ReestrProjectsReport = await GetReestrProjectsReport(deadline);
            result.GovernmentOrganizations = await Task.FromResult(GetGovernmentOrganizationsReport(deadline).Result);
            result.FarmOrganizations = await Task.FromResult(GetFarmOrganizationsReport(deadline).Result);
            result.AdministrationOrganizations = await Task.FromResult(GetAdministrationOrganizationsReport(deadline).Result);
            
            return await Task.FromResult(result);
        }

        public async Task<OrgReportModel> GetGovernmentOrganizationsReport(Deadline deadline)
        {
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.GovernmentOrganizations && o.IsActive == true && o.IsIct == true).ToList();
            
            OrgReportModel result = new OrgReportModel();
            result.Category = "Davlat boshqaruvi organlari";
            result.OrganizationsReport = new List<ReportBySpheresModel>();
            
            var gSpheres = _gSphere.GetAll().ToList();
            var gFields = _gField.GetAll().ToList();
            var gSubFields = _gSubField.GetAll().ToList();
            var gRankTable = _gRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

            foreach (var o in organizations)
            {
                ReportBySpheresModel model = new ReportBySpheresModel()
                {
                    OrganizationId = o.Id, OrgName = o.ShortName, OrgNameRu = o.ShortNameRu,
                    UserServiceId = o.UserServiceId, Category = o.OrgCategory, Spheres = new List<SphereRateElement>()
                };
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
                                var ranks = gRankTable.Where(a =>
                                    a.OrganizationId == o.Id && a.SphereId == sphere.Id && a.FieldId == field.Id &&
                                    a.SubFieldId == sField.Id);
                                if(ranks.Count()>1)
                                {
                                    fieldRate += Math.Round((double)ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                                }
                                if(ranks.Count()==1)
                                {
                                    fieldRate += Math.Round((double)ranks.First().Rank, 2);
                                }
                            }
                            
                        }
                        if(!subfields.Any())
                        {
                            var fieldR = gRankTable.Where(r =>
                                r.OrganizationId == o.Id && r.SphereId == sphere.Id && r.FieldId == field.Id);
                            if (fieldR.Count() > 1)
                            {
                                fieldRate = Math.Round((double)fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                            }
                            if(fieldR.Count()==1)
                            {
                                fieldRate = Math.Round((double)fieldR.First().Rank, 2);
                            }
                        }

                        if(field.Section == "3.4")
                        {
                            sphereRate -= Math.Round(fieldRate, 2);
                        }
                        else
                        {
                            sphereRate += Math.Round(fieldRate, 2);
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
                    addElement.SphereRate = Math.Round(sphereRate, 2);
                    model.Spheres.Add(addElement);
                    reached += sphereRate;
                }
                model.RateSum = Math.Round(reached, 2);
                model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                result.Count = result.Count + 1;
                result.OrganizationsReport.Add(model);
            }
            result.OrganizationsReport = result.OrganizationsReport.OrderByDescending(o => o.RatePercent).ToList();
            return await Task.FromResult(result);
        }

        public async Task<OrgReportModel> GetFarmOrganizationsReport(Deadline deadline)
        {
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.FarmOrganizations && o.IsActive == true && o.IsIct == true).ToList();
            
            OrgReportModel result = new OrgReportModel();
            result.Category = "Xo'jalik boshqaruvi organlari";
            result.OrganizationsReport = new List<ReportBySpheresModel>();
            
            var xSpheres = _xSphere.GetAll().ToList();
            var xFields = _xField.GetAll().ToList();
            var xSubFields = _xSubField.GetAll().ToList();
            var xRankTable = _xRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

            foreach (var o in organizations)
            {
                ReportBySpheresModel model = new ReportBySpheresModel()
                {
                    OrganizationId = o.Id, OrgName = o.ShortName, OrgNameRu = o.ShortNameRu,
                    UserServiceId = o.UserServiceId, Category = o.OrgCategory, Spheres = new List<SphereRateElement>()
                };
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
                        if (subfields.Any())
                        {
                            foreach (var sfield in subfields)
                            {
                                var ranks = xRankTable.Where(a => a.OrganizationId == o.Id && a.SphereId == sphere.Id && a.FieldId == field.Id && a.SubFieldId == sfield.Id);
                                if (ranks.Count() > 1)
                                {
                                    var subfieldRankMedium = Math.Round(ranks.Select(r => r.Rank).Sum() / ranks.Count(), 2);
                                    fieldRate += subfieldRankMedium;
                                }
                                if (ranks.Count() == 1)
                                {
                                    fieldRate += Math.Round(ranks.First().Rank, 2);
                                }
                            }
                        }
                        if (!subfields.Any())
                        {
                            var fieldR = xRankTable.Where(r => r.OrganizationId == o.Id && r.SphereId == sphere.Id && r.FieldId == field.Id);
                            if (fieldR.Count() > 1)
                            {
                                var fieldRankMedium = Math.Round(fieldR.Select(f => f.Rank).Sum() / fieldR.Count(), 2);
                                fieldRate = fieldRankMedium;
                            }
                            if (fieldR.Count() == 1)
                            {
                                fieldRate = Math.Round(fieldR.First().Rank, 2);
                            }
                        }

                        sphereRate += fieldRate;
                    }
                    SphereRateElement addElement = new SphereRateElement();
                    addElement.SphereId = sphere.Id;
                    addElement.SphereName = sphere.Name;
                    addElement.SphereSection = sphere.Section;
                    addElement.SphereRate = Math.Round(sphereRate, 2);
                    model.Spheres.Add(addElement);
                    reached += sphereRate;
                }
                model.RateSum = Math.Round(reached, 2);
                model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                result.Count++;
                result.OrganizationsReport.Add(model);
            }
            result.OrganizationsReport = result.OrganizationsReport.OrderByDescending(o => o.RatePercent).ToList();
            return await Task.FromResult(result);
        }

        public async Task<OrgReportModel> GetAdministrationOrganizationsReport(Deadline deadline)
        {
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.Adminstrations && o.IsActive == true && o.IsIct == true).ToList();
            
            OrgReportModel result = new OrgReportModel();
            result.Category = "Hokimliklar";
            result.OrganizationsReport = new List<ReportBySpheresModel>();
            
            var aSpheres = _aSphere.GetAll().ToList();
            var aFields = _aField.GetAll().ToList();
            var aSubFields = _aSubField.GetAll().ToList();
            var aRankTable = _aRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

            foreach (var o in organizations)
            {
                ReportBySpheresModel model = new ReportBySpheresModel()
                {
                    OrganizationId = o.Id, OrgName = o.ShortName, OrgNameRu = o.ShortNameRu,
                    UserServiceId = o.UserServiceId, Category = o.OrgCategory, Spheres = new List<SphereRateElement>()
                };
                
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
                                    fieldRate += Math.Round(ranks.First().Rank, 2);
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
                                fieldRate = Math.Round(fieldR.First().Rank, 2);
                            }
                        }

                        sphereRate += fieldRate;
                    }
                    SphereRateElement addElement = new SphereRateElement();
                    addElement.SphereId = sphere.Id;
                    addElement.SphereName = sphere.Name;
                    addElement.SphereSection = sphere.Section;
                    addElement.SphereRate = Math.Round(sphereRate, 2);
                    model.Spheres.Add(addElement);
                    reached += sphereRate;
                }
                model.RateSum = Math.Round(reached, 2);
                model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                result.Count = result.Count + 1;
                result.OrganizationsReport.Add(model);
            }
            result.OrganizationsReport = result.OrganizationsReport.OrderByDescending(o => o.RatePercent).ToList();
            return await Task.FromResult(result);
        }

        public async Task<RatedOrganizationServices> GetRatedServicesReport(Deadline deadline)
        {
            RatedOrganizationServices result = new RatedOrganizationServices();
            
            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true).ToList();

            List<int> orgIds = organizations.Select(o=>o.Id).ToList();


            var publicServices = _organizationPublicServices.Find(s => orgIds.Any(i => i == s.OrganizationId));

            result.Count = publicServices.Count();

            var mygovServices = publicServices.Where(s => s.MyGovService == true && s.MyGovServiceExpert == true);

            result.MyGovServices = mygovServices.Count();

            var otherAppServices = publicServices.Where(s => s.OtherApps == true && s.OtherAppsExpert == true);

            result.OtherAppServices = otherAppServices.Count();

            return await Task.FromResult(result);
        }

        public async Task<RatedReestrProjects> GetReestrProjectsReport(Deadline deadline)
        {
            RatedReestrProjects result = new RatedReestrProjects();
            var reestrExceptions = _reestrProjectException.GetAll().Select(p => p.ReestrProjectId).ToList();

            var reestrProjectPositions = _reestrProjectPosition
                .Find(p => reestrExceptions.Any(i => i != p.ReestrProjectId));

            List<int> reestrIds = reestrProjectPositions.Select(p => p.ReestrProjectId).ToList();
            
            result.Count = reestrIds.Count;

            result.WorkingStage = reestrProjectPositions.Count(p => p.ProjectStatus == ReestrProjectStatusInNis.WorkingStage && p.ExpertExcept == true);

            var reestrProjectExpertDecisions =
                _reestrProjectExpertDecision.Find(p => reestrIds.Any(i => i == p.ReestrProjectId));

            result.ExpertDecision = reestrProjectExpertDecisions.Count(p => p.Exist == true && p.ExpertExcept==true);
            
            foreach (int id in reestrIds)
            {
                SecondRequestQuery query = new SecondRequestQuery()
                {
                    Id = id
                };
                SecondRequestQueryResult reestrProjectStatus = await _reesterService.SecondRequest(query);

                if (reestrProjectStatus.PassportStatus == ReesterProjectStatus.CONFIRMED)
                    result.ConfirmedProjectPassports++;
            }

            
            
            return await Task.FromResult(result);
        }
    }
}