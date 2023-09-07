using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Enums;
using Domain.IntegrationLinks;
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Domain.Models.SixthSection;
using SB.Common.Extensions;

namespace MainInfrastructures.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<OrganizationPublicServices, int> _organizationPublicServices;
        private readonly IRepository<ReestrProjectException, int> _reestrProjectException;
        private readonly IRepository<ReestrProjectPosition, int> _reestrProjectPosition;
        private readonly IRepository<ReestrProjectExpertDecision, int> _reestrProjectExpertDecision;
        private readonly IRepository<OrganizationDigitalEconomyProjectsReport, int> _digitalEconomyProjectsReport;
        private readonly IRepository<ReplacerOrgHead, int> _replacerOrgHead;
        private readonly IRepository<OrganizationIctSpecialForces, int> _orgSpecialForces;
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

        private List<int> DashboardBlackList = new List<int>() { 189, 177, 112 };

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
                                        IRepository<ReestrProjectExpertDecision, int> reestrProjectExpertDecision,
                                        IRepository<OrganizationDigitalEconomyProjectsReport, int> digitalEconomyProjectsReport,
                                        IRepository<ReplacerOrgHead, int> replacerOrgHead,
                                        IRepository<OrganizationIctSpecialForces, int> orgSpecialForces
        )
        {
            _organization = organizations;
            _organizationPublicServices = organizationPublicServices;
            _reestrProjectException = reestrProjectException;
            _reestrProjectPosition = reestrProjectPosition;
            _reestrProjectExpertDecision = reestrProjectExpertDecision;
            _digitalEconomyProjectsReport = digitalEconomyProjectsReport;
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
            _replacerOrgHead = replacerOrgHead;
            _orgSpecialForces = orgSpecialForces;
        }
        
        public async Task<DashboardResultModel> GetDashboardData(int? deadlineId)
        {
            DashboardResultModel result = new DashboardResultModel();

            var deadline = new Deadline();

            if (deadlineId != null && deadlineId > 0)
            {
                deadline = _deadline.Find(d => d.Dashboard == true && d.Id == deadlineId).FirstOrDefault();
            }
            else
            {
                deadline = _deadline.Find(d => d.Dashboard == true).OrderBy(i => i.Id).LastOrDefault();
            }
            

            if (deadline == null)
                return await Task.FromResult(result);

            result.RatedServicesReport = await GetRatedServicesReport(deadline);
            result.DigitalEconomyProjectsReport = await GetDigitalEconomyProjectsReport(deadline);
            result.ReestrProjectsReport = await GetReestrProjectsReport(deadline);
            result.GovernmentOrganizations = await Task.FromResult(GetGovernmentOrganizationsReport(deadline).Result);
            result.FarmOrganizations = await Task.FromResult(GetFarmOrganizationsReport(deadline).Result);
            result.AdministrationOrganizations = await Task.FromResult(GetAdministrationOrganizationsReport(deadline).Result);
            
            return await Task.FromResult(result);
        }

        private async Task<OrgReportModel> GetGovernmentOrganizationsReport(Deadline deadline)
        {
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.GovernmentOrganizations && o.IsActive == true && o.IsIct == true).ToList();
            organizations = organizations.Where(o=>DashboardBlackList.All(d=>d!=o.Id)).ToList();
            
            OrgReportModel result = new OrgReportModel();
            result.Category = "Davlat boshqaruvi organlari";
            result.OrganizationsReport = new List<ReportBySpheresModelDashboard>();
            
            var gSpheres = _gSphere.GetAll().ToList();
            var gFields = _gField.GetAll().ToList();
            var gSubFields = _gSubField.GetAll().ToList();
            var gRankTable = _gRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

            foreach (var o in organizations)
            {
                ReportBySpheresModelDashboard model = new ReportBySpheresModelDashboard()
                {
                    OrganizationId = o.Id, OrgName = o.ShortName, OrgNameRu = o.ShortNameRu,
                    UserServiceId = o.UserServiceId, Category = o.OrgCategory, Spheres = new List<SphereRateElement>()
                };

                #region CalculateSphereRanks

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

                #endregion

                #region ReplacerOrgHeadSet
                var replacerOrgHead = _replacerOrgHead.Find(r => r.OrganizationId == o.Id).FirstOrDefault();
                
                if (replacerOrgHead != null)
                {
                    model.OrgHeadModel = new OrgHeadModel()
                    {
                        FirstName = replacerOrgHead.FirstName,
                        LastName = replacerOrgHead.LastName,
                        MiddleName = replacerOrgHead.MidName,
                        Phone = replacerOrgHead.Phone,
                        Email = replacerOrgHead.Email,
                        PhotoLink = replacerOrgHead.PhotoPath
                    };
                }
                
                #endregion

                #region OrgWebsiteSet

                model.OrgWebsite = o.WebSite;

                #endregion

                #region ItDepartmentModelSet

                var specialForce = _orgSpecialForces.Find(s => s.OrganizationId == o.Id).FirstOrDefault();
                if (specialForce != null)
                {
                    model.ItDepartmentModel = new ItDepartmentModel()
                    {
                        DepartmentName = specialForce.SpecialForcesName,
                        FullNameDirector = specialForce.FullNameDirector,
                        DirectorPosition = specialForce.HeadPosition,
                        WorkPhone = specialForce.WorkPhone,
                        MobilePhone = specialForce.MobilePhone,
                        Email = specialForce.Email,
                        PhotoLink = specialForce.PhotoPath
                    };
                }
                
                #endregion

                #region ReestrProjectCountSet

                var reestrProjects = _reestrProjectPosition.Find(r => r.OrganizationId == o.Id).ToList();
                if (reestrProjects != null)
                    model.ReestrProjectCount = reestrProjects.Count;

                #endregion

                #region DigitalEconomyProjectsSet

                var digitalEconomyProjects =
                    _digitalEconomyProjectsReport.Find(d => d.OrganizationId == o.Id).FirstOrDefault();
                if (digitalEconomyProjects != null)
                {
                    model.DigitalProjectsModel = new DigitalProjectsModel()
                    {
                        AllProjects = digitalEconomyProjects.ProjectsCount,
                        CompletedProjects = digitalEconomyProjects.CompletedProjects,
                        OngoinProjects = digitalEconomyProjects.OngoingProjects,
                        NotCompletedProjects = digitalEconomyProjects.NotFinishedProjects
                    };
                }

                #endregion
                
                model.RateSum = Math.Round(reached, 2);
                model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                result.Count = result.Count + 1;
                result.OrganizationsReport.Add(model);
            }
            result.OrganizationsReport = result.OrganizationsReport.OrderByDescending(o => o.RatePercent).ToList();
            return await Task.FromResult(result);
        }

        private async Task<OrgReportModel> GetFarmOrganizationsReport(Deadline deadline)
        {
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.FarmOrganizations && o.IsActive == true && o.IsIct == true).ToList();
            
            organizations = organizations.Where(o=>DashboardBlackList.All(d=>d!=o.Id)).ToList();
            
            OrgReportModel result = new OrgReportModel();
            result.Category = "Xo'jalik boshqaruvi organlari";
            result.OrganizationsReport = new List<ReportBySpheresModelDashboard>();
            
            var xSpheres = _xSphere.GetAll().ToList();
            var xFields = _xField.GetAll().ToList();
            var xSubFields = _xSubField.GetAll().ToList();
            var xRankTable = _xRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

            foreach (var o in organizations)
            {
                ReportBySpheresModelDashboard model = new ReportBySpheresModelDashboard()
                {
                    OrganizationId = o.Id, OrgName = o.ShortName, OrgNameRu = o.ShortNameRu,
                    UserServiceId = o.UserServiceId, Category = o.OrgCategory, Spheres = new List<SphereRateElement>()
                };

                #region CalculateSphereRanks

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

                #endregion

                #region ReplacerOrgHeadSet
                var replacerOrgHead = _replacerOrgHead.Find(r => r.OrganizationId == o.Id).FirstOrDefault();
                
                if (replacerOrgHead != null)
                {
                    model.OrgHeadModel = new OrgHeadModel()
                    {
                        FirstName = replacerOrgHead.FirstName,
                        LastName = replacerOrgHead.LastName,
                        MiddleName = replacerOrgHead.MidName,
                        Phone = replacerOrgHead.Phone,
                        Email = replacerOrgHead.Email,
                        PhotoLink = replacerOrgHead.PhotoPath
                    };
                }
                
                #endregion
                
                #region OrgWebsiteSet

                model.OrgWebsite = o.WebSite;

                #endregion
                
                #region ItDepartmentModelSet

                var specialForce = _orgSpecialForces.Find(s => s.OrganizationId == o.Id).FirstOrDefault();
                if (specialForce != null)
                {
                    model.ItDepartmentModel = new ItDepartmentModel()
                    {
                        DepartmentName = specialForce.SpecialForcesName,
                        FullNameDirector = specialForce.FullNameDirector,
                        DirectorPosition = specialForce.HeadPosition,
                        WorkPhone = specialForce.WorkPhone,
                        MobilePhone = specialForce.MobilePhone,
                        Email = specialForce.Email,
                        PhotoLink = specialForce.PhotoPath
                    };
                }
                
                #endregion
                
                #region DigitalEconomyProjectsSet

                var digitalEconomyProjects =
                    _digitalEconomyProjectsReport.Find(d => d.OrganizationId == o.Id).FirstOrDefault();
                if (digitalEconomyProjects != null)
                {
                    model.DigitalProjectsModel = new DigitalProjectsModel()
                    {
                        AllProjects = digitalEconomyProjects.ProjectsCount,
                        CompletedProjects = digitalEconomyProjects.CompletedProjects,
                        OngoinProjects = digitalEconomyProjects.OngoingProjects,
                        NotCompletedProjects = digitalEconomyProjects.NotFinishedProjects
                    };
                }

                #endregion
                
                #region ReestrProjectCountSet

                var reestrProjects = _reestrProjectPosition.Find(r => r.OrganizationId == o.Id).ToList();
                if (reestrProjects != null)
                    model.ReestrProjectCount = reestrProjects.Count;

                #endregion
                
                model.RateSum = Math.Round(reached, 2);
                model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                result.Count++;
                result.OrganizationsReport.Add(model);
            }
            result.OrganizationsReport = result.OrganizationsReport.OrderByDescending(o => o.RatePercent).ToList();
            return await Task.FromResult(result);
        }

        private async Task<OrgReportModel> GetAdministrationOrganizationsReport(Deadline deadline)
        {
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.Adminstrations && o.IsActive == true && o.IsIct == true).ToList();
            
            organizations = organizations.Where(o=>DashboardBlackList.All(d=>d!=o.Id)).ToList();
            
            OrgReportModel result = new OrgReportModel();
            result.Category = "Hokimliklar";
            result.OrganizationsReport = new List<ReportBySpheresModelDashboard>();
            
            var aSpheres = _aSphere.GetAll().ToList();
            var aFields = _aField.GetAll().ToList();
            var aSubFields = _aSubField.GetAll().ToList();
            var aRankTable = _aRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

            foreach (var o in organizations)
            {
                ReportBySpheresModelDashboard model = new ReportBySpheresModelDashboard()
                {
                    OrganizationId = o.Id, OrgName = o.ShortName, OrgNameRu = o.ShortNameRu,
                    UserServiceId = o.UserServiceId, Category = o.OrgCategory, Spheres = new List<SphereRateElement>()
                };

                #region CalculateSphereRanks
                
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
                
                #endregion
                
                #region ReplacerOrgHeadSet
                var replacerOrgHead = _replacerOrgHead.Find(r => r.OrganizationId == o.Id).FirstOrDefault();
                
                if (replacerOrgHead != null)
                {
                    model.OrgHeadModel = new OrgHeadModel()
                    {
                        FirstName = replacerOrgHead.FirstName,
                        LastName = replacerOrgHead.LastName,
                        MiddleName = replacerOrgHead.MidName,
                        Phone = replacerOrgHead.Phone,
                        Email = replacerOrgHead.Email,
                        PhotoLink = replacerOrgHead.PhotoPath
                    };
                }
                
                #endregion
                
                #region OrgWebsiteSet

                model.OrgWebsite = o.WebSite;

                #endregion
                
                #region ItDepartmentModelSet

                var specialForce = _orgSpecialForces.Find(s => s.OrganizationId == o.Id).FirstOrDefault();
                if (specialForce != null)
                {
                    model.ItDepartmentModel = new ItDepartmentModel()
                    {
                        DepartmentName = specialForce.SpecialForcesName,
                        FullNameDirector = specialForce.FullNameDirector,
                        DirectorPosition = specialForce.HeadPosition,
                        WorkPhone = specialForce.WorkPhone,
                        MobilePhone = specialForce.MobilePhone,
                        Email = specialForce.Email,
                        PhotoLink = specialForce.PhotoPath
                    };
                }
                
                #endregion
                
                #region DigitalEconomyProjectsSet

                var digitalEconomyProjects =
                    _digitalEconomyProjectsReport.Find(d => d.OrganizationId == o.Id).FirstOrDefault();
                if (digitalEconomyProjects != null)
                {
                    model.DigitalProjectsModel = new DigitalProjectsModel()
                    {
                        AllProjects = digitalEconomyProjects.ProjectsCount,
                        CompletedProjects = digitalEconomyProjects.CompletedProjects,
                        OngoinProjects = digitalEconomyProjects.OngoingProjects,
                        NotCompletedProjects = digitalEconomyProjects.NotFinishedProjects
                    };
                }

                #endregion
                
                #region ReestrProjectCountSet

                var reestrProjects = _reestrProjectPosition.Find(r => r.OrganizationId == o.Id).ToList();
                if (reestrProjects != null)
                    model.ReestrProjectCount = reestrProjects.Count;

                #endregion
                
                model.RateSum = Math.Round(reached, 2);
                model.RatePercent = Math.Round((reached / maxRate) * 100, 2);

                result.Count = result.Count + 1;
                result.OrganizationsReport.Add(model);
            }
            result.OrganizationsReport = result.OrganizationsReport.OrderByDescending(o => o.RatePercent).ToList();
            return await Task.FromResult(result);
        }

        private async Task<RatedOrganizationServices> GetRatedServicesReport(Deadline deadline)
        {
            RatedOrganizationServices result = new RatedOrganizationServices();
            
            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true).ToList();

            organizations = organizations.Where(o=>DashboardBlackList.All(d=>d!=o.Id)).ToList();
            
            List<int> orgIds = organizations.Select(o=>o.Id).ToList();


            var publicServices = _organizationPublicServices.Find(s => orgIds.Any(i => i == s.OrganizationId));

            result.Count = publicServices.Count();

            var nationalElectronServices = publicServices.Where(s => (s.ServiceType == OrganizationServiceType.Electronic || s.ServiceType == OrganizationServiceType.NationalElectronic) && s.ServiceTypeExpert == true);

            result.ElectronicServices = nationalElectronServices.Count();

            var mygovServices = publicServices.Where(s => s.MyGovService == true && s.MyGovServiceExpert == true);

            result.MyGovServices = mygovServices.Count();

            var otherAppServices = publicServices.Where(s => s.OtherApps == true && s.OtherAppsExpert == true);

            result.OtherAppServices = otherAppServices.Count();

            return await Task.FromResult(result);
        }

        private async Task<DigitalEconomyProjectsReport> GetDigitalEconomyProjectsReport(Deadline deadline)
        {
            DigitalEconomyProjectsReport result = new DigitalEconomyProjectsReport();
            
            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true).ToList();

            organizations = organizations.Where(o=>DashboardBlackList.All(d=>d!=o.Id)).ToList();
            
            List<int> orgIds = organizations.Select(o => o.Id).ToList();

            var report = _digitalEconomyProjectsReport.Find(r => orgIds.Any(i => i == r.OrganizationId));

            result.Count = report.Sum(r=>r.ProjectsCount);
            result.Completed = report.Sum(r => r.CompletedProjects);
            result.Ongoing = report.Sum(r => r.OngoingProjects);
            result.NotFinished = report.Sum(r => r.NotFinishedProjects);

            return await Task.FromResult(result);
        }

        private async Task<RatedReestrProjects> GetReestrProjectsReport(Deadline deadline)
        {
            RatedReestrProjects result = new RatedReestrProjects();
            var reestrExceptions = _reestrProjectException.GetAll().Select(p => p.ReestrProjectId);

            var reestrProjectPositions = _reestrProjectPosition.GetAll();
            reestrProjectPositions = reestrProjectPositions.Where(p =>p.ProjectStatus!= ReestrProjectStatusInNis.Undefined && reestrExceptions.Any(i => i != p.ReestrProjectId));

            var reestrIds = reestrProjectPositions.Select(p => p.ReestrProjectId);

            var reestrProjectExpertDecisions =
                _reestrProjectExpertDecision.Find(p => reestrIds.Any(i => i == p.ReestrProjectId));

            result.Count = reestrIds.Count();
            result.ConfirmedProjectPassports = reestrIds.Count();
            result.WorkingStage = reestrProjectPositions.Count(p => p.ProjectStatus == ReestrProjectStatusInNis.WorkingStage && p.ExpertExcept == true);
            result.ExpertDecision = reestrProjectExpertDecisions.Count(p => p.Exist == true && p.ExpertExcept==true);

            return await Task.FromResult(result);
        }

        public async Task<bool> TransferRanks(int deadlineFromId, int deadlineToId, string userPinfl)
        {
            #region Check Deadline

            if (userPinfl != Links.MainAdminPinfl)
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadlineFrom = _deadline.Find(d => d.Id == deadlineFromId).FirstOrDefault();
            if (deadlineFrom == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);
            
            var deadlineTo = _deadline.Find(d => d.Id == deadlineToId).FirstOrDefault();
            if (deadlineTo == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            #endregion

            await CheckRanks(deadlineFrom, deadlineTo);

            await TransferGovernmentRanks(deadlineFrom, deadlineTo);

            await TransferFarmRanks(deadlineFrom, deadlineTo);

            await TransferAdministrationRanks(deadlineFrom, deadlineTo);
            
            return await Task.FromResult(true);
        }

        private Task CheckRanks(Deadline deadlineFrom, Deadline deadlineTo)
        {
            var gRanksToExport = _gRankTable.Find(r => r.Year == deadlineFrom.Year && r.Quarter == deadlineFrom.Quarter)
                .ToList();
            if (!gRanksToExport.Any())
                throw ErrorStates.Error(UIErrors.DataForThisPeriodNotFound);
            
            var gRanksToFill = _gRankTable.Find(r => r.Year == deadlineTo.Year && r.Quarter == deadlineTo.Quarter)
                .ToList();
            if (gRanksToFill.Any())
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist); 
            
            var xRanksToExport = _xRankTable.Find(r => r.Year == deadlineFrom.Year && r.Quarter == deadlineFrom.Quarter)
                .ToList();
            if (!xRanksToExport.Any())
                throw ErrorStates.Error(UIErrors.DataForThisPeriodNotFound);
            
            var xRanksToFill = _xRankTable.Find(r => r.Year == deadlineTo.Year && r.Quarter == deadlineTo.Quarter)
                .ToList();
            if (xRanksToFill.Any())
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);
            
            var aRanksToExport = _aRankTable.Find(r => r.Year == deadlineFrom.Year && r.Quarter == deadlineFrom.Quarter)
                .ToList();
            if (!aRanksToExport.Any())
                throw ErrorStates.Error(UIErrors.DataForThisPeriodNotFound);
            
            var aRanksToFill = _aRankTable.Find(r => r.Year == deadlineTo.Year && r.Quarter == deadlineTo.Quarter)
                .ToList();
            if (aRanksToFill.Any())
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);

            return Task.FromResult(0);
        }

        private Task TransferGovernmentRanks(Deadline deadlineFrom, Deadline deadlineTo)
        {
         
            var gRanksToExport = _gRankTable.Find(r => r.Year == deadlineFrom.Year && r.Quarter == deadlineFrom.Quarter)
                .ToList();

            List<GRankTable> addList = new List<GRankTable>();
            
            foreach (var rank in gRanksToExport)
            {
                GRankTable newRank = new GRankTable();
                
                newRank = rank;
                newRank.Id = 0;
                newRank.Year = deadlineTo.Year;
                newRank.Quarter = deadlineTo.Quarter;
                
                addList.Add(newRank);
            }
            
            _gRankTable.AddRange(addList);
            
            return Task.FromResult(0);
        }
        
        private Task TransferFarmRanks(Deadline deadlineFrom, Deadline deadlineTo)
        {
         
            var xRanksToExport = _xRankTable.Find(r => r.Year == deadlineFrom.Year && r.Quarter == deadlineFrom.Quarter)
                .ToList();

            List<XRankTable> addList = new List<XRankTable>();
            
            foreach (var rank in xRanksToExport)
            {
                XRankTable newRank = new XRankTable();
                
                newRank = rank;
                newRank.Id = 0;
                newRank.Year = deadlineTo.Year;
                newRank.Quarter = deadlineTo.Quarter;
                
                addList.Add(newRank);
            }
            
            _xRankTable.AddRange(addList);
            
            return Task.FromResult(0);
        }
        
        private Task TransferAdministrationRanks(Deadline deadlineFrom, Deadline deadlineTo)
        {
         
            var aRanksToExport = _aRankTable.Find(r => r.Year == deadlineFrom.Year && r.Quarter == deadlineFrom.Quarter)
                .ToList();

            List<ARankTable> addList = new List<ARankTable>();
            
            foreach (var rank in aRanksToExport)
            {
                ARankTable newRank = new ARankTable();
                
                newRank = rank;
                newRank.Id = 0;
                newRank.Year = deadlineTo.Year;
                newRank.Quarter = deadlineTo.Quarter;
                
                addList.Add(newRank);
            }
            
            _aRankTable.AddRange(addList);
            
            return Task.FromResult(0);
        }
    }
}