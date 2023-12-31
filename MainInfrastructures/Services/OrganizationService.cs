﻿using Domain.Models.Ranking.Administrations;
using Domain.Models.Ranking;
using Domain.Models;
using EntityRepository;
using JohaRepository;
using MainInfrastructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.States;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.IntegrationLinks;
using Domain.ReesterModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Domain.AuthModels;
using System.Threading;
using Domain;
using SB.Common.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using Domain.Models.FirstSection;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Domain.Models.ThirdSection;
using Domain.Models.Organization;
using Domain.MyGovModels;
using MainInfrastructures.Migrations;
using Domain.Models.MibModels;
using Domain.Permission;
using System.Xml.Linq;
using Domain.Enums;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.SixthSection;
using Domain.OpenDataModels;
using Domain.Models.SecondSection;

namespace MainInfrastructures.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<GSphere, int> _gSphere;
        private readonly IRepository<GField, int> _gField;
        private readonly IRepository<GSubField, int> _gSubField;
        private readonly IRepository<GRankTable, int> _gRankTable;
        private readonly IRepository<XSphere, int> _xSphere;
        private readonly IRepository<XField, int> _xField;
        private readonly IRepository<XSubField, int> _xSubField;
        private readonly IRepository<XRankTable, int> _xRankTable;
        private readonly IRepository<ASphere, int> _aSphere;
        private readonly IRepository<AField, int> _aField;
        private readonly IRepository<ASubField, int> _aSubField;
        private readonly IRepository<ARankTable, int> _aRankTable;
        private readonly IRepository<WebSiteAvailability, int> _websiteAvailability;
        private readonly IRepository<ReestrProjectPassport, int> _reestrProjectPassport;
        private readonly IRepository<ReestrProjectException, int> _reestrException;
        private readonly IRepository<ReestrProjectPosition, int> _reestrProjectPosition;
        private readonly IRepository<ReestrProjectConnection, int> _reestrProjectConnection;
        private readonly IRepository<ReestrProjectClassifications, int> _reestrClassifications;
        private readonly IRepository<ReestrProjectIdentities, int> _reestrIdentities;
        private readonly IRepository<ReestrProjectExpertDecision, int> _reestrProjectExpertDecision;
        private readonly IRepository<ReestrProjectCyberSecurityExpertDecision, int> _reestrProjectCyberSecurityExpertDecision;
        private readonly IRepository<ReestrProjectAuthorizations, int> _reestrProjectAuthorization;
        private readonly IRepository<ReestrProjectAutomatedServices, int> _reestrProjectAutomatedServices;
        private readonly IRepository<ReestrProjectEfficiency, int> _reestrProjectEfficiency;
        private readonly IRepository<OrganizationSocials, int> _orgSocialSites;
        private readonly IRepository<OpenDataTable, int> _openDataTable;
        private readonly IRepository<OrgHelpline, int> _orgHelpline;
        private readonly IRepository<HelplineInfo, int> _orgHelplineInfo;
        private readonly IRepository<OrganizationIctSpecialForces, int> _orgSpecialForces;
        private readonly IRepository<OrganizationServices, int> _orgServices;
        private readonly IRepository<OrganizationPublicServices, int> _orgPublicServices;
        private readonly IRepository<MibReport, int> _mibReport;
        private readonly IRepository<MygovReports, int> _myGovReports;

        private readonly IDataContext _db;
        private readonly IReesterService _reesterService;
        private int rankExcelStartIndex;
        private int organizationsPingReportIndex;
        public OrganizationService(IRepository<Deadline, int> deadline,
                                    IRepository<Organizations, int> organization, 
                                    IRepository<GSphere, int> gSphere, 
                                    IRepository<GField, int> gField, 
                                    IRepository<GSubField, int> gSubField,
                                    IRepository<GRankTable, int> gRankTable,
                                    IRepository<XSphere, int> xSphere, 
                                    IRepository<XField, int> xField, 
                                    IRepository<XSubField, int> xSubField,
                                    IRepository<XRankTable, int> xRankTable,
                                    IRepository<ASphere, int> aSphere, 
                                    IRepository<AField, int> aField, 
                                    IRepository<ASubField, int> aSubField,
                                    IRepository<ARankTable, int> aRankTable,
                                    IDataContext db,
                                    IReesterService reesterService, 
                                    IRepository<WebSiteAvailability, int> websiteAvailability, 
                                    IRepository<ReestrProjectPassport, int> reestrProjectPassport, 
                                    IRepository<ReestrProjectException, int> reestrException,
                                    IRepository<ReestrProjectPosition, int> reestrProjectPosition,
                                    IRepository<ReestrProjectConnection, int> reestrProjectConnection, 
                                    IRepository<ReestrProjectClassifications, int> reestrClassifications,
                                    IRepository<ReestrProjectIdentities, int> reestrIdentities,
                                    IRepository<ReestrProjectExpertDecision, int> reestrProjectExpertDecision,
                                    IRepository<ReestrProjectCyberSecurityExpertDecision, int> reestrProjectCyberSecurityExpertDecision,
                                    IRepository<ReestrProjectAuthorizations, int> reestrProjectAuthorization,
                                    IRepository<ReestrProjectAutomatedServices, int> reestrProjectAutomatedServices,
                                    IRepository<ReestrProjectEfficiency, int> reestrProjectEfficiency,
                                    IRepository<OrganizationSocials, int> orgSocialSites,
                                    IRepository<OpenDataTable, int> openDataTable,
                                    IRepository<OrgHelpline, int> orgHelpline,
                                    IRepository<HelplineInfo, int> orgHelplineInfo,
                                    IRepository<OrganizationIctSpecialForces, int> orgSpecialForces,
                                    IRepository<OrganizationServices, int> orgServices,
                                    IRepository<OrganizationPublicServices, int> orgPublicServices,
                                    IRepository<MibReport, int> mibReport,
                                    IRepository<MygovReports, int> myGovReports)
        {
            _deadline = deadline;
            _organization = organization;
            _gSphere = gSphere;
            _gField = gField;
            _gSubField = gSubField;
            _gRankTable = gRankTable;
            _xSphere = xSphere;
            _xField = xField;
            _xSubField = xSubField;
            _xRankTable = xRankTable;
            _aSphere = aSphere;
            _aField = aField;
            _aSubField = aSubField;
            _aRankTable = aRankTable;
            _db = db;
            _reesterService = reesterService;
            _websiteAvailability = websiteAvailability;
            _reestrProjectPassport = reestrProjectPassport;
            _reestrException = reestrException;
            _reestrProjectPosition = reestrProjectPosition;
            _reestrProjectConnection = reestrProjectConnection;
            _reestrClassifications = reestrClassifications;
            _reestrIdentities = reestrIdentities;
            _reestrProjectExpertDecision = reestrProjectExpertDecision;
            _reestrProjectCyberSecurityExpertDecision = reestrProjectCyberSecurityExpertDecision;
            _reestrProjectAuthorization = reestrProjectAuthorization;
            _reestrProjectAutomatedServices = reestrProjectAutomatedServices;
            _reestrProjectEfficiency = reestrProjectEfficiency;
            _orgSocialSites = orgSocialSites;
            _openDataTable = openDataTable;
            _orgHelpline = orgHelpline;
            _orgHelplineInfo = orgHelplineInfo;
            _orgSpecialForces = orgSpecialForces;
            _orgServices = orgServices;
            _orgPublicServices = orgPublicServices;
            _mibReport = mibReport;
            _myGovReports = myGovReports;
        }

        public async Task<RankingStruct> GetStruct(int orgId)
        {
            RankingStruct result = new RankingStruct() { Spheres = new List<Sphere>() };

            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(orgId.ToString());
            
            if(org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                var spheres = _aSphere.GetAll().Include(mbox => mbox.AFields).ThenInclude(mbox => mbox.ASubFields);

                foreach(var sphere in spheres)
                {
                    Sphere s = new Sphere
                    {
                        Id = sphere.Id,
                        Name = sphere.Name,
                        Section = sphere.Section,
                        MaxRate = sphere.MaxRate,
                        Fields = new List<Fields>()
                    };

                    s.Fields = sphere.AFields.Select(f => new Fields
                    {
                        Id = f.Id,
                        Name = f.Name,
                        MaxRate = f.MaxRate,
                        Section = f.Section,
                        SubFields = f.ASubFields.Select(sf => new SubFields
                        {
                            Id = sf.Id,
                            Name = sf.Name,
                            MaxRate = sf.MaxRate,
                            Section = sf.Section,
                        }).ToList()
                    }).ToList();

                    

                    result.Spheres.Add(s);
                }

            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                var spheres = _gSphere.GetAll().Include(mbox => mbox.GFields).ThenInclude(mbox => mbox.GSubFields);

                foreach (var sphere in spheres)
                {
                    Sphere s = new Sphere
                    {
                        Id = sphere.Id,
                        Name = sphere.Name,
                        Section = sphere.Section,
                        MaxRate = sphere.MaxRate,
                        Fields = new List<Fields>()
                    };

                    s.Fields = sphere.GFields.Select(f => new Fields
                    {
                        Id = f.Id,
                        Name = f.Name,
                        MaxRate = f.MaxRate,
                        Section = f.Section,
                        SubFields = f.GSubFields.Select(sf => new SubFields
                        {
                            Id = sf.Id,
                            Name = sf.Name,
                            MaxRate = sf.MaxRate,
                            Section = sf.Section,
                        }).ToList()
                    }).ToList();



                    result.Spheres.Add(s);
                }

            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                var spheres = _xSphere.GetAll().Include(mbox => mbox.XFields).ThenInclude(mbox => mbox.XSubFields);

                foreach (var sphere in spheres)
                {
                    Sphere s = new Sphere
                    {
                        Id = sphere.Id,
                        Name = sphere.Name,
                        Section = sphere.Section,
                        MaxRate = sphere.MaxRate,
                        Fields = new List<Fields>()
                    };

                    s.Fields = sphere.XFields.Select(f => new Fields
                    {
                        Id = f.Id,
                        Name = f.Name,
                        MaxRate = f.MaxRate,
                        Section = f.Section,
                        SubFields = f.XSubFields.Select(sf => new SubFields
                        {
                            Id = sf.Id,
                            Name = sf.Name,
                            MaxRate = sf.MaxRate,
                            Section = sf.Section,
                        }).ToList()
                    }).ToList();



                    result.Spheres.Add(s);
                }

            }
            return result;
        }

        public async Task<bool> UpdateOrgsName()
        {
            var orgs = _organization.GetAll().ToList();

            foreach(Organizations org in orgs)
            {
                if(org.UserServiceId != 0)
                {
                    var result = new AuthOrggetQueryResult();
                    var cts = new CancellationTokenSource();
                    try
                    {

                        HttpClientHandler clientHandler = new HttpClientHandler();
                        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                        HttpClient client = new HttpClient(clientHandler);

                        var byteArray = Encoding.ASCII.GetBytes("single:123456");
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                        var url = Links.AuthOrgGetUrl;

                        url = url + "?organizationId=" + org.UserServiceId +"&limit=1";

                        var response = await client.GetAsync(url).ConfigureAwait(false);
                        if (response != null || response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            var obj = JObject.Parse(jsonString);
                            result = JsonConvert.DeserializeObject<AuthOrggetQueryResult>(jsonString);
                        }
                        else
                        {
                            throw ErrorStates.NotResponding();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine("Error: " + ex);
                        throw ex;
                    }
                    catch (TaskCanceledException ex)
                    {
                        if (ex.CancellationToken == cts.Token)
                        {
                            throw ErrorStates.Error(UIErrors.OrganizationNotFound);
                        }
                    }
                    if(!String.IsNullOrEmpty(result.Result.Data.First().Name))
                    {
                        org.FullName = result.Result.Data.First().Name;
                        org.ShortName = result.Result.Data.First().Name;
                        org.FullNameRu = result.Result.Data.First().NameRu;
                        org.ShortNameRu = result.Result.Data.First().NameRu;
                    }
                    
                    _organization.Update(org);
                }
            }

            return true;
        }

        public async Task<bool> UploadOrgServices(IFormFile file)
        {
            List<OrganizationPublicServices> addList = new List<OrganizationPublicServices>();

            

            using (MemoryStream memory = new MemoryStream())
            {
                file.CopyTo(memory);
                using var package = new ExcelPackage(memory);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var workSheet = package.Workbook.Worksheets.First();
                var rowCount = workSheet.Dimension.End.Row;
                for (int row = 5; row <= rowCount; row++)
                {
                    try
                    {

                        if (workSheet.Cells[row, 2].Value?.ToString()?.Length > 1)
                        {
                            OrganizationPublicServices addModel = new OrganizationPublicServices();
                            
                            addModel.OrganizationId = Convert.ToInt32(workSheet.Cells[row, 3].Value?.ToString());
                            addModel.ServiceNameUz = workSheet.Cells[row, 2].Value?.ToString();

                            if (workSheet.Cells[row, 6].Value?.ToString() == "Jismoniy va yuridik shaxslar")
                                addModel.PaidFor = Domain.Enums.OrganizationServiceConsumers.ForAll;

                            if (workSheet.Cells[row, 6].Value?.ToString() == "Yuridik shaxs")
                                addModel.PaidFor = Domain.Enums.OrganizationServiceConsumers.Legals;

                            if (workSheet.Cells[row, 6].Value?.ToString() == "Jismoniy shaxs")
                                addModel.PaidFor = Domain.Enums.OrganizationServiceConsumers.Phsicals;

                           addList.Add(addModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ErrorStates.Error(UIErrors.DataToChangeNotFound);
                    }
                }
            }

            _db.Context.Set<OrganizationPublicServices>().AddRange(addList);

            _db.Context.SaveChanges();

            return true;
        }
        
        public async Task<bool> UploadDigitalEconomyProjects(IFormFile file)
        {
            List<OrganizationDigitalEconomyProjectsDetail> addList = new List<OrganizationDigitalEconomyProjectsDetail>();

            

            using (MemoryStream memory = new MemoryStream())
            {
                file.CopyTo(memory);
                using var package = new ExcelPackage(memory);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var workSheet = package.Workbook.Worksheets.First();
                var rowCount = workSheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {

                        if (workSheet.Cells[row, 2].Value?.ToString()?.Length > 1)
                        {
                            OrganizationDigitalEconomyProjectsDetail addModel = new OrganizationDigitalEconomyProjectsDetail();
                            
                            addModel.OrganizationId = Convert.ToInt32(workSheet.Cells[row, 1].Value?.ToString());
                            addModel.NormativeDocumentNumber = workSheet.Cells[row, 2].Value?.ToString();
                            addModel.ApplicationNumber = workSheet.Cells[row, 3].Value?.ToString();
                            addModel.ProjectIndex = workSheet.Cells[row, 4].Value?.ToString();
                            addModel.Responsibles = workSheet.Cells[row, 5].Value?.ToString();
                            addModel.Actions = workSheet.Cells[row, 6].Value?.ToString();

                            if (workSheet.Cells[row, 7].Value?.ToString() == "1")
                                addModel.Status = ProjectStatus.Done;

                            if (workSheet.Cells[row, 7].Value?.ToString() == "2")
                                addModel.Status = ProjectStatus.InProccess;

                            if (workSheet.Cells[row, 7].Value?.ToString() == "3")
                                addModel.Status = ProjectStatus.Failed;

                            addList.Add(addModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ErrorStates.Error(UIErrors.DataToChangeNotFound);
                    }
                }
            }

            _db.Context.Set<OrganizationDigitalEconomyProjectsDetail>().AddRange(addList);

            _db.Context.SaveChanges();

            return true;
        }
        public async Task<decimal> SubFieldMaxRate(int orgId, string fieldSection, string subFieldSection)
        {
            decimal rate = 0;

            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                var fields = _aField.GetAll().Include(mbox => mbox.ASubFields);
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                var subField = _aSubField.Find(s =>s.FieldId == field.Id && s.Section == subFieldSection).FirstOrDefault();

                if (subField == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = (decimal)subField.MaxRate;
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                var fields = _gField.GetAll();
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                var subField = _gSubField.Find(s =>s.FieldId == field.Id && s.Section == subFieldSection).FirstOrDefault();

                if (subField == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = (decimal)subField.MaxRate;
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                var fields = _xField.GetAll();
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                var subField = _xSubField.Find(s =>s.FieldId == field.Id && s.Section == subFieldSection).FirstOrDefault();

                if (subField == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = (decimal)subField.MaxRate;
            }

            return rate;
        }
        public async Task<decimal> FieldMaxRate(int orgId, string fieldSection)
        {
            decimal rate = 0;

            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                var fields = _aField.GetAll().Include(mbox => mbox.ASubFields);
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = (decimal)field.MaxRate;
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                var fields = _gField.GetAll();
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);


                rate = (decimal)field.MaxRate;
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                var fields = _xField.GetAll();
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = (decimal)field.MaxRate;
            }

            return rate;
        }

        public async Task<OrgExceptionPercentResultModel> GetOrganizationExceptionPercent(int orgId)
        {

            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            double rate = 0;

            if (org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                #region calculate rate

                double maxRate = 0;
                double gotRate = 0;

                var allRanks = _gRankTable.Find(r => r.OrganizationId == org.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

                foreach (var rank in allRanks)
                {
                    gotRate += rank.Rank;

                    if (rank.SubFieldId != 0)
                    {
                        var subField = _gSubField.Find(s => s.Id == rank.SubFieldId).FirstOrDefault();

                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        maxRate += subField.MaxRate;
                    }
                    else
                    {
                        var f = _gField.Find(f => f.Id == rank.FieldId).FirstOrDefault();

                        maxRate += f.MaxRate;
                    }
                }

                if(maxRate!=0)
                    rate = Math.Round((gotRate / maxRate) * 100, 2);


                if (rate == 0)
                    rate = 1;

                #endregion
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                #region calculate rate

                double maxRate = 0;
                double gotRate = 0;

                var allRanks = _xRankTable.Find(r => r.OrganizationId == org.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

                foreach (var rank in allRanks)
                {
                    gotRate += rank.Rank;

                    if (rank.SubFieldId != 0)
                    {
                        var subField = _xSubField.Find(s => s.Id == rank.SubFieldId).FirstOrDefault();

                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        maxRate += subField.MaxRate;
                    }
                    else
                    {
                        var f = _xField.Find(f => f.Id == rank.FieldId).FirstOrDefault();

                        maxRate += f.MaxRate;
                    }
                }

                if(maxRate != 0)
                    rate = Math.Round((gotRate / maxRate) * 100, 2);

                if (rate == 0)
                    rate = 1;

                #endregion
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                #region calculate rate

                double maxRate = 0;
                double gotRate = 0;

                var allRanks = _aRankTable.Find(r => r.OrganizationId == org.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

                foreach (var rank in allRanks)
                {
                    gotRate += rank.Rank;

                    if (rank.SubFieldId != 0)
                    {
                        var subField = _aSubField.Find(s => s.Id == rank.SubFieldId).FirstOrDefault();

                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        maxRate += subField.MaxRate;
                    }
                    else
                    {
                        var f = _aField.Find(f => f.Id == rank.FieldId).FirstOrDefault();

                        maxRate += f.MaxRate;
                    }
                }
                if (maxRate != 0)
                    rate = Math.Round((gotRate / maxRate) * 100, 2);


                if (rate == 0)
                    rate = 1;

                #endregion
            }

            OrgExceptionPercentResultModel result = new OrgExceptionPercentResultModel();

            result.Percentage = rate;
            result.OrganizationId = org.Id;

            return result;

        }

        #region DownloadOrgPingReport 2.3
        public async Task<MemoryStream> DownloadOrgPingReport(List<string> userRights)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);
            
            
            string fileName = "OrgPingReport";
            var memoryStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                worksheet.Columns[3].Width = 25;
                worksheet.Columns[4].Width = 25;
                worksheet.Columns[5].Width = 25;
                worksheet.Columns[6].Width = 25;
 
                worksheet.DefaultRowHeight = 25;
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "RASMIY VEB-SAYTNING MAVJUDLIGI VA INTERNET ORQALI FOYDALANA OLISH";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Websayt havolasi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Jami yuborilgan so'rov;ar soni";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Muvofaqqiyatsiz so'rovlar soni";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Muvofaqqiyatsiz so'rovlar ulushi(%)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                #endregion

                organizationsPingReportIndex = 6;
                
                await SetGovernmentPingReport(worksheet, deadline);
                await SetFarmPingReport(worksheet, deadline);
                await SetAdministrationPingReport(worksheet, deadline);
                
                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        private async Task SetGovernmentPingReport(ExcelWorksheet worksheet, Deadline deadline)
        {
            var report = _websiteAvailability.Find(a => a.DeadlineId == deadline.Id).Include(mbox => mbox.Organization)
                .Where(r => r.Organization.IsActive == true && r.Organization.IsIct == true &&
                            r.Organization.OrgCategory == OrgCategory.GovernmentOrganizations).ToList();
            
            if(report.Count > 0)
            {
                foreach(WebSiteAvailability r in report) 
                {
                    worksheet.Cells[organizationsPingReportIndex, 1].Value = r.Organization.ShortName;
                    worksheet.Cells[organizationsPingReportIndex, 2].Value = "Davlat boshqaruvi";
                    worksheet.Cells[organizationsPingReportIndex, 3].Value = r.Website;
                    worksheet.Cells[organizationsPingReportIndex, 4].Value = (r.FailedPing + r.SuccessfulPing).ToString();
                    worksheet.Cells[organizationsPingReportIndex, 5].Value = r.FailedPing.ToString();
                    worksheet.Cells[organizationsPingReportIndex, 6].Value = Math
                        .Round(((decimal)r.FailedPing / (r.SuccessfulPing + r.FailedPing)) * 100, 2)
                        .ToString(CultureInfo.InvariantCulture);

                    organizationsPingReportIndex += 1;
                }
            }
        }
        private async Task SetFarmPingReport(ExcelWorksheet worksheet, Deadline deadline)
        {
            var report = _websiteAvailability.Find(a => a.DeadlineId == deadline.Id).Include(mbox => mbox.Organization)
                .Where(r => r.Organization.IsActive == true && r.Organization.IsIct == true &&
                            r.Organization.OrgCategory == OrgCategory.FarmOrganizations).ToList();

            if (report.Count > 0)
            {
                foreach (WebSiteAvailability r in report)
                {
                    worksheet.Cells[organizationsPingReportIndex, 1].Value = r.Organization.ShortName;
                    worksheet.Cells[organizationsPingReportIndex, 2].Value = "Xo'jalik boshqaruvi";
                    worksheet.Cells[organizationsPingReportIndex, 3].Value = r.Website;
                    worksheet.Cells[organizationsPingReportIndex, 4].Value = (r.FailedPing + r.SuccessfulPing).ToString();
                    worksheet.Cells[organizationsPingReportIndex, 5].Value = r.FailedPing.ToString();
                    worksheet.Cells[organizationsPingReportIndex, 6].Value = Math
                        .Round(((decimal)r.FailedPing / (r.SuccessfulPing + r.FailedPing)) * 100, 2)
                        .ToString(CultureInfo.InvariantCulture);

                    organizationsPingReportIndex += 1;
                }
            }
        }
        private async Task SetAdministrationPingReport(ExcelWorksheet worksheet, Deadline deadline)
        {
            var report = _websiteAvailability.Find(a => a.DeadlineId == deadline.Id).Include(mbox => mbox.Organization)
                .Where(r => r.Organization.IsActive == true && r.Organization.IsIct == true &&
                            r.Organization.OrgCategory == OrgCategory.Adminstrations).ToList();

            if (report.Count > 0)
            {
                foreach (WebSiteAvailability r in report)
                {
                    worksheet.Cells[organizationsPingReportIndex, 1].Value = r.Organization.ShortName;
                    worksheet.Cells[organizationsPingReportIndex, 2].Value = "Hokimliklar";
                    worksheet.Cells[organizationsPingReportIndex, 3].Value = r.Website;
                    worksheet.Cells[organizationsPingReportIndex, 4].Value = (r.FailedPing + r.SuccessfulPing).ToString();
                    worksheet.Cells[organizationsPingReportIndex, 5].Value = r.FailedPing.ToString();
                    worksheet.Cells[organizationsPingReportIndex, 6].Value = Math
                        .Round(((decimal)r.FailedPing / (r.SuccessfulPing + r.FailedPing)) * 100, 2)
                        .ToString(CultureInfo.InvariantCulture);

                    organizationsPingReportIndex += 1;
                }
            }
        }
        #endregion

        #region DownloadOrgSocialSitesReport 2.5

        public async Task<MemoryStream> DownloadOrgSocialSitesReport(List<string> userRights)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true)
                .OrderBy(o => o.OrgCategory).ToList();

            var orgSocialSites = _orgSocialSites.GetAll().ToList();
            
            string fileName = "OrgPingReport";
            var memoryStream = new MemoryStream();
            
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "IJTIMOIY TARMOQLARDAGI SAHIFALAR HAVOLALARI";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Ijtimoiy tarmoq havolasi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Asosiymi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Jamoatchilik bilan ochiq muloqot o'tkazganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Tashkkilotning to'liq nomi ko'rsatilganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "Rasmiy web-sayt ko'rsatilganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 8, 5, 8])
                {
                    range.Value = "Ishonch telefoni raqami ko'rsatilganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 9, 5, 9])
                {
                    range.Value = "Yuridik manzili ko'rsatilganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 10, 5, 10])
                {
                    range.Value = "Elektron pochtasi ko'rsatilganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 11, 5, 11])
                {
                    range.Value = "Boshqa ijtimoiy tarmoq va messenjerlardagi sahifalarga xavolalar ko'rsatilganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 12, 5, 12])
                {
                    range.Value = "Tasdiqlanganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 13, 5, 13])
                {
                    range.Value = "Postlar sinxronizatsiyasi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                #endregion

                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var orgSocials = orgSocialSites.Where(s => s.OrganizationId == organization.Id)
                        .OrderByDescending(s => s.IsMain).ToList();

                    foreach (var social in orgSocials)
                    {
                        worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                        worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                        {
                            OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                            OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                            OrgCategory.Adminstrations => "Hokimliklar",
                            _ => worksheet.Cells[excelIndex, 2].Value
                        };
                        worksheet.Cells[excelIndex, 3].Value = social.MessengerLink;
                        worksheet.Cells[excelIndex, 4].Value = social.IsMain == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 5].Value = social.PoolExceptExpert == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 6].Value = social.OrgFullName == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 7].Value = social.OrgLegalSite == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 8].Value = social.OrgPhone == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 9].Value = social.OrgLegalAddress == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 10].Value = social.OrgEmail == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 11].Value = social.LinksToOtherSocials == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 12].Value = social.Verified == true ? "Ha" : "Yo'q";

                        var poolCount = 5;
                        if (social.IsMain == false && social.Post1 == false)
                            poolCount--;
                        if (social.IsMain == false && social.Post1 == false)
                            poolCount--;
                        if (social.IsMain == false && social.Post1 == false)
                            poolCount--;
                        if (social.IsMain == false && social.Post1 == false)
                            poolCount--;
                        if (social.IsMain == false && social.Post1 == false)
                            poolCount--;
                        
                        worksheet.Cells[excelIndex, 13].Value = poolCount.ToString();
                        
                        excelIndex++;
                    }
                    
                }

                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        #endregion

        #region DownloadOrgOpenDataReport 2.6

        public async Task<MemoryStream> DownloadOrgOpenDataReport(List<string> userRights)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true)
                .OrderBy(o => o.OrgCategory).ToList();

            var openDataTable = _openDataTable.GetAll().ToList();
            
            string fileName = "OrgPingReport";
            var memoryStream = new MemoryStream();
            
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "OCHIQ MA'LUMOTLAR PORTALIDA OCHIQ MA'LUMOTLAR TO'PLAMLARI MAVJUDLIGI";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Ochiq ma'lumotlar nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Identifikatsiya raqami";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "So'ngi yangilangan sana";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Holati";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "(URL)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                
                #endregion

                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var orgOpenData = openDataTable.Where(s => s.OrganizationId == organization.Id)
                        .OrderBy(s => s.Status).ToList();

                    foreach (var table in orgOpenData)
                    {
                        worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                        worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                        {
                            OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                            OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                            OrgCategory.Adminstrations => "Hokimliklar",
                            _ => worksheet.Cells[excelIndex, 2].Value
                        };
                        worksheet.Cells[excelIndex, 3].Value = table.TableName;
                        worksheet.Cells[excelIndex, 4].Value = table.TableId;
                        worksheet.Cells[excelIndex, 5].Value = table.UpdateDate.ToString();
                        worksheet.Cells[excelIndex, 6].Value = table.Status switch
                        {
                            OpenDataTableStatus.Custom => "Yangi",
                            OpenDataTableStatus.Updated => "Yangilangan",
                            OpenDataTableStatus.Rejected => "Rad qilingan",
                            OpenDataTableStatus.Checked => "Tasdiqlangan",
                            OpenDataTableStatus.Old => "Eskirgan",
                            _ => worksheet.Cells[excelIndex, 6].Value
                        };
                        worksheet.Cells[excelIndex, 7].Value = table.Link;
                        
                        excelIndex++;
                    }
                }
                
                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        #endregion

        #region DownloadOrgHelplineReport 2.7

        public async Task<MemoryStream> DownloadOrgHelplineReport(List<string> userRights)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true)
                .OrderBy(o => o.OrgCategory).ToList();
            
            var orgHelpline = _orgHelpline.GetAll().ToList();
            var orgHelplineInfo = _orgHelplineInfo.GetAll().ToList();
            
            string fileName = "OrgHelplineReport";
            var memoryStream = new MemoryStream();
            
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "ISHONCH TELEFONI ISH SIFATI";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Ishonch telefoni raqami";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Ishonch telefoni reglamentda ko'rsatilganligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Ishonch telefoni ish holatidaligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Ishonch telefonini baholash imkoniyati";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                
                #endregion

                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var helpline = orgHelpline.FirstOrDefault(s => s.OrganizationId == organization.Id);
                    var helplineInfo = orgHelplineInfo.FirstOrDefault(s => s.OrganizationId == organization.Id);

                    worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                    worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                    {
                        OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                        OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                        OrgCategory.Adminstrations => "Hokimliklar",
                        _ => worksheet.Cells[excelIndex, 2].Value
                    };
                    if (helpline != null)
                    {
                        worksheet.Cells[excelIndex, 3].Value = helpline.HelplineNumber;
                    }

                    if (helplineInfo != null)
                    {
                        worksheet.Cells[excelIndex, 4].Value = helplineInfo.RegulationShowsPhone == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 5].Value = helplineInfo.HelplinePhoneWorkStatus == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 6].Value =
                            helplineInfo.HelplinePhoneRatingOption == true ? "Ha" : "Yo'q";
                    }
                    
                    excelIndex++;
                    
                }
                
                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        #endregion

        #region DownloadOrgServicesReport  3.1

        public async Task<MemoryStream> DownloadOrgServicesReport(List<string> userRights)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true)
                .OrderBy(o => o.OrgCategory).ToList();

            var orgServicesList = _orgServices.GetAll().Include(mbox => mbox.Rates).ToList();

            string fileName = "OrgHelplineReport";
            var memoryStream = new MemoryStream();
            
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "ELEKTRON XIZMATLAR SIFATINI JAMOATCHILIK TOMONIDAN BAHOLASH";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Xizmat nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Ariza raqami";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Ariza berishda muammolar";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Muammo izohi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "Muammo ekspert tomonidan tasdiqlanishi (Xa/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 8, 5, 8])
                {
                    range.Value = "Ekspert muammoni tasdiqlamasligi izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 9, 5, 9])
                {
                    range.Value = "YIDXPni tavsiya qilish (Xa/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 10, 5, 10])
                {
                    range.Value = "YIDXPni tavsiya qilish - Yo'q bo'lganda izoh (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 11, 5, 11])
                {
                    range.Value = "Natijaning qoniqarliligi (Xa/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 12, 5, 12])
                {
                    range.Value = "Natija qoniqarsizligi izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 13, 5, 13])
                {
                    range.Value = "Natija qoniqarsizligi ekspert tomonidan tasdiqlanishi (Xa/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 14, 5, 14])
                {
                    range.Value = "Ekspert natija qoniqarsizligini tasdiqlamasligi izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 15, 5, 15])
                {
                    range.Value = "Xizmat yuzasidan taklif";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 16, 5, 16])
                {
                    range.Value = "Taklif izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 17, 5, 17])
                {
                    range.Value = "Xizmatga e'tiroz ";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 18, 5, 18])
                {
                    range.Value = "E'tiroz izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 19, 5, 19])
                {
                    range.Value = "Taklif / E'tiroz ekspert tomonidan tasdiqlanishi (Xa/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 20, 5, 20])
                {
                    range.Value = "Taklif / E'tiroz tasdiqlamasligi izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "Foydalanuvchi bahosi (1-5)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                #endregion

                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var orgServices = orgServicesList.Where(s => s.OrganizationId == organization.Id).ToList();
                    foreach (var service in orgServices)
                    {
                        foreach (var rate in service.Rates)
                        {
                            worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                            worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                            {
                                OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                                OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                                OrgCategory.Adminstrations => "Hokimliklar",
                                _ => worksheet.Cells[excelIndex, 2].Value
                            };
                            worksheet.Cells[excelIndex, 3].Value = service.ServiceNameUz;
                            worksheet.Cells[excelIndex, 4].Value = rate.ApplicationNumber;
                            worksheet.Cells[excelIndex, 5].Value = rate.HasApplicationProblem == true ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 6].Value = rate.ApplicationProblemText;
                            worksheet.Cells[excelIndex, 7].Value =
                                rate.ApplicationProblemConfirmde == true ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 8].Value = rate.ApplicationProblemTextExspert;
                            worksheet.Cells[excelIndex, 9].Value = rate.RecommendService == true ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 10].Value = rate.NotRecommendationComment;
                            worksheet.Cells[excelIndex, 11].Value = rate.ServiceSatisfactive == true ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 12].Value = rate.ServiceDissatisfactionReason;
                            worksheet.Cells[excelIndex, 13].Value =
                                rate.ServiceDissatisfactionConfirmed == true ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 14].Value = rate.ServiceDissatisfactionConfirmedExspert;
                            worksheet.Cells[excelIndex, 15].Value = rate.ServiceCommentType== CommentType.Suggestion ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 16].Value = rate.ServiceCommentType == CommentType.Suggestion
                                ? rate.ServiceComment
                                : "";
                            worksheet.Cells[excelIndex, 17].Value = rate.ServiceCommentType == CommentType.Objection ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 18].Value = rate.ServiceCommentType == CommentType.Objection ? rate.ServiceComment : "";
                            worksheet.Cells[excelIndex, 19].Value = rate.ServiceCommentConfirmed == true ? "Ha" : "Yo'q";
                            worksheet.Cells[excelIndex, 20].Value = rate.ServiceCommentConfirmedExspert;
                            worksheet.Cells[excelIndex, 21].Value = rate.ServiceRate switch
                            {
                                Rate.Worse => "0",
                                Rate.VeryBad => "0",
                                Rate.Bad => "0",
                                Rate.Satisfactive => "0",
                                Rate.Good => "0",
                                Rate.Excelent=>"5",
                                _ => worksheet.Cells[excelIndex, 21].Value
                            };

                            excelIndex++;
                        }
                    }
                }
                
                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        #endregion

        #region DownloadOrgPublicServicesReport  3.2

        public async Task<MemoryStream> DownloadOrgPublicServicesReport(List<string> userRights, int userOrgId)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS) && !userRights.Contains(Permissions.ORGANIZATION_EMPLOYEE))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true)
                .OrderBy(o => o.OrgCategory).ToList();
            
            if (userRights.Contains(Permissions.ORGANIZATION_EMPLOYEE))
                organizations = organizations.Where(o => o.Id == userOrgId).ToList();

            var orgPublicServicesList = _orgPublicServices.GetAll().ToList();
            
            
            string fileName = "OrgPublicServicesReport";
            var memoryStream = new MemoryStream();
            
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "ELEKTRON DAVLAT XIZMATLARI ULUSHI";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Xizmat nomi (o'zbek tilida)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Xizmat nomi (rus tilida)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Xizmat joriy qilinishi asosi: Hujjat turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Xizmat joriy qilinishi asosi: Hujjat nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "Xizmat joriy qilinishi asosi: Hujjat raqami";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 8, 5, 8])
                {
                    range.Value = "Xizmat joriy qilinishi asosi: Hujjat sanasi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 9, 5, 9])
                {
                    range.Value = "Xizmat narxi turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 10, 5, 10])
                {
                    range.Value = "Xizmat narxi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 11, 5, 11])
                {
                    range.Value = "Xizmat ko'rsatish muddati (avtomatik/ish kunlari/boshqa)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 12, 5, 12])
                {
                    range.Value = "Xizmat ko'rsatish muddati";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 13, 5, 13])
                {
                    range.Value = "Xizmat shakli (A'nanaviy/Elektron)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 14, 5, 14])
                {
                    range.Value = "URL (Xizmat shakli elektron/an'anaviy-elektron bo'lganda)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 15, 5, 15])
                {
                    range.Value = "Screenshot (manzili, text) (Xizmat shakli elektron/an'anaviy-elektron bo'lganda)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 16, 5, 16])
                {
                    range.Value = "Xizmat subyekti (Jismoniy / Yuridik / Barcha)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 17, 5, 17])
                {
                    range.Value = "Xizmat reglamenti (Bor/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 18, 5, 18])
                {
                    range.Value = "Xizmat reglamenti(Bor bo'lganda fayl manzili)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 19, 5, 19])
                {
                    range.Value = "Xizmat reglamenti yo'qlik sababi Izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 20, 5, 20])
                {
                    range.Value = "Xizmatning yangilangan reglamenti mavjudligi (Bor/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "Xizmatning yangilangan reglamenti (Bor bo'lganda fayl manzili)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "Xizmatning yangilangan reglamenti yo'qlik sababi Izohi (text)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "YIDXPda mavjudligi (Bor/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "URL (YIDXPda mavjud bo'lganda)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "Screenshot (YIDXPda mavjud bo'lganda)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "YIDXP yoki boshqa mobil ilovada mavjudligi (Bor/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "Ilova nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "URL (Mobil ilovani yuklab olish uchun havola)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 21, 5, 21])
                {
                    range.Value = "Screenshot (YIDXP yoki boshqa mobil ilovada mavjud)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                #endregion

                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var orgPublicServices = orgPublicServicesList.Where(s => s.OrganizationId == organization.Id).ToList();
                    foreach (var service in orgPublicServices)
                    {
                        worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                        worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                        {
                            OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                            OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                            OrgCategory.Adminstrations => "Hokimliklar",
                            _ => worksheet.Cells[excelIndex, 2].Value
                        };
                        worksheet.Cells[excelIndex, 3].Value = service.ServiceNameUz;
                        worksheet.Cells[excelIndex, 4].Value = service.ServiceNameRu;
                        worksheet.Cells[excelIndex, 5].Value = service.ServiceBasedDocumentType switch
                        {
                            CommanderOrgs.President => "Prezident qarori",
                            CommanderOrgs.CabinetMinisters => "Vazirlar mahkamasi",
                            CommanderOrgs.PresidentialAdministration => "Prezident administratsiyasi",
                            CommanderOrgs.UpperHouse => "Yuqori palata",
                            CommanderOrgs.Other => "Boshqa",
                            _ => worksheet.Cells[excelIndex, 5].Value
                        };
                        worksheet.Cells[excelIndex, 6].Value = service.ServiceBasedDocumentName;
                        worksheet.Cells[excelIndex, 7].Value = service.ServiceBasedDocumentNumber;
                        worksheet.Cells[excelIndex, 8].Value = service.ServiceBasedDocumentNumber;
                        worksheet.Cells[excelIndex, 9].Value = service.PaidFor switch
                        {
                            OrganizationServiceConsumers.ForAll => "Hammaga",
                            OrganizationServiceConsumers.Legals => "Yuridik",
                            OrganizationServiceConsumers.Phsicals => "Jismoniy",
                            _ => worksheet.Cells[excelIndex, 9].Value
                        };
                        worksheet.Cells[excelIndex, 10].Value = service.ServicePrice.ToString();
                        worksheet.Cells[excelIndex, 11].Value = service.ServiceCompletePeriodType switch
                        {
                            ServiceCompletePeriodType.Automatic => "Avtomatik",
                            ServiceCompletePeriodType.WorkingDays => "Ish kunlarida",
                            ServiceCompletePeriodType.Others => "Boshqa",
                            _ => worksheet.Cells[excelIndex, 11].Value
                        };
                        worksheet.Cells[excelIndex, 12].Value = service.ServiceCompletePeriod.ToString();
                        worksheet.Cells[excelIndex, 13].Value = service.ServiceType switch
                        {
                            OrganizationServiceType.Electronic => "Elektronik",
                            OrganizationServiceType.National => "An'anaviy",
                            OrganizationServiceType.NationalElectronic => "An'anaviy elektron",
                            _ => worksheet.Cells[excelIndex, 13].Value
                        };
                        worksheet.Cells[excelIndex, 14].Value = service.ServiceLink;
                        worksheet.Cells[excelIndex, 15].Value = service.ServiceScreenshotLink;
                        worksheet.Cells[excelIndex, 16].Value = service.ServiceSubjects switch
                        {
                            OrganizationServiceConsumers.Legals => "Yuridik",
                            OrganizationServiceConsumers.Phsicals => "Jismoniy",
                            OrganizationServiceConsumers.ForAll => "Hamma uchun",
                            _ => worksheet.Cells[excelIndex, 16].Value
                        };
                        worksheet.Cells[excelIndex, 17].Value = service.ServiceHasReglament == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 18].Value = service.ServiceReglamentPath;
                        worksheet.Cells[excelIndex, 19].Value = service.ServiceReglamentComment;
                        worksheet.Cells[excelIndex, 20].Value =
                            service.ServiceHasUpdateReglament == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 21].Value = service.ServiceUpdateReglamentPath;
                        worksheet.Cells[excelIndex, 22].Value = service.ServiceUpdateReglamentComment;
                        worksheet.Cells[excelIndex, 23].Value = service.MyGovService == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 24].Value = service.MyGovLink;
                        worksheet.Cells[excelIndex, 24].Value = service.MyGovScreenshotLink;
                        worksheet.Cells[excelIndex, 24].Value = service.OtherApps == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 24].Value = service.AppName;
                        worksheet.Cells[excelIndex, 24].Value = service.AppLink;
                        worksheet.Cells[excelIndex, 24].Value = service.AppScreenshot;
                        
                        excelIndex++;
                        
                    }
                }
                
                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        #endregion

        #region DownloadOrgMygovServicesReport 3.3

        public async Task<MemoryStream> DownloadOrgMygovServicesReport(List<string> userRights)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true).Include(o=>o.SubOrganizations)
                .OrderBy(o => o.OrgCategory).ToList();
            

            var orgMygovServicesList = _myGovReports.GetAll().ToList();
            
            
            string fileName = "OrgMygovReport";
            var memoryStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;

                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "DAVLAT XIZMATLARIDAN FOYDALANA OLISH IMKONYATI MAVJUDLIGI";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }

                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }

                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Service Id ";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Service nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Muvoffaqiyatli so'rovlar";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Muvoffaqiyatsiz so'rovlar";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "Barcha so'rovlar";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                #endregion
                
                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    List<MygovReportModel> services = new List<MygovReportModel>();
                    
                    var orgMygovServices = orgMygovServicesList.Where(s =>
                        s.OrganizationId == organization.Id).ToList();
                    
                    foreach (var item in orgMygovServices)
                    {
                        var s = services.FirstOrDefault(s => s.ServiceId == item.ServiceId && s.Organization.Id == item.OrganizationId);
                        if (s != null)
                        {
                            s.AllRequest += item.AllRequests;
                            s.LateRequest += item.LateRequests;
                        }
                        else
                        {
                            services.Add(new MygovReportModel
                            {
                                Organization = organization,
                                ServiceId = item.ServiceId,
                                ServiceName = item.ServiceName,
                                AllRequest = item.AllRequests,
                                LateRequest = item.LateRequests
                            });
                        }
                    }
                    
                    foreach (var service in services)
                    {
                        worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                        worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                        {
                            OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                            OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                            OrgCategory.Adminstrations => "Hokimliklar",
                            _ => worksheet.Cells[excelIndex, 2].Value
                        };
                        worksheet.Cells[excelIndex, 3].Value = service.ServiceId.ToString();
                        worksheet.Cells[excelIndex, 4].Value = service.ServiceName;
                        worksheet.Cells[excelIndex, 5].Value = (service.AllRequest - service.LateRequest).ToString();
                        worksheet.Cells[excelIndex, 6].Value = service.LateRequest.ToString();
                        worksheet.Cells[excelIndex, 7].Value = service.AllRequest.ToString();
                        
                        excelIndex++;
                        
                    }
                }
                
                package.Save();
            }
            memoryStream.Flush();
            memoryStream.Position = 0;
            
            return memoryStream;
        }

        #endregion

        #region DownloadOrgMibServicesReport  3.4

        public async Task<MemoryStream> DownloadOrgMibServicesReport(List<string> userRights)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true).Include(o=>o.SubOrganizations)
                .OrderBy(o => o.OrgCategory).ToList();
            

            var orgMibServicesList = _mibReport.GetAll().Where(s=>!string.IsNullOrEmpty(s.OwnerInn)).ToList();
            
            
            string fileName = "OrgHelplineReport";
            var memoryStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;

                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "DAVLAT XIZMATLARIDAN FOYDALANA OLISH IMKONYATI MAVJUDLIGI";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }

                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }

                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "API version";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }

                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Xizmat nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }

                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Muvoffaqiyatli so'rovlar";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Muvoffaqiyatsiz so'rovlar";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "Barcha so'rovlar";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                #endregion
                
                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var orgMibServices = orgMibServicesList.Where(s =>
                        s.OwnerInn == organization.OrgInn ||
                        organization.SubOrganizations.Any(subOrg => subOrg.Inn == s.OwnerInn)).ToList();
                    foreach (var service in orgMibServices)
                    {
                        worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                        worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                        {
                            OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                            OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                            OrgCategory.Adminstrations => "Hokimliklar",
                            _ => worksheet.Cells[excelIndex, 2].Value
                        };
                        worksheet.Cells[excelIndex, 3].Value = service.ApiVersion;
                        worksheet.Cells[excelIndex, 4].Value = service.ApiName;
                        worksheet.Cells[excelIndex, 5].Value = service.SuccessCount.ToString();
                        worksheet.Cells[excelIndex, 6].Value = service.FailCount.ToString();
                        worksheet.Cells[excelIndex, 7].Value = service.Overall.ToString();
                        
                        excelIndex++;
                        
                    }
                }
                
                package.Save();
            }
            memoryStream.Flush();
            memoryStream.Position = 0;
            
            return memoryStream;
        }

        #endregion

        #region JustToGetClassifications

        public async Task<MemoryStream> DownloadOrgClassifications()
        {
            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true)
                .OrderBy(o => o.OrgCategory).ToList();

            
            
            string fileName = "OrgClassifications";
            var memoryStream = new MemoryStream();
            
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "Classificator va Identifikatorlar";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Sistema nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Sistema reestrdagi id raqami";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Classification/Identification";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "Url";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }

                #endregion

                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var reestrProjects = _reestrProjectPassport.Find(s => s.OrganizationId == organization.Id)
                        .OrderBy(s => s.ReestrProjectId).ToList();

                    foreach (var project in reestrProjects)
                    {
                        var projectClassifications = _reestrClassifications
                            .Find(c => c.ReestrProjectId == project.ReestrProjectId).Include(mbox=>mbox.Classifications).FirstOrDefault();

                        var projectIdentifications = _reestrIdentities
                            .Find(i => i.ReestrProjectId == project.ReestrProjectId).Include(mbox => mbox.Identities)
                            .FirstOrDefault();

                        if (projectClassifications != null)
                        {
                            foreach (var classification in projectClassifications.Classifications)
                            {
                                worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                                worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                                {
                                    OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                                    OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                                    OrgCategory.Adminstrations => "Hokimliklar",
                                    _ => worksheet.Cells[excelIndex, 2].Value
                                };
                                worksheet.Cells[excelIndex, 3].Value = project.FullName;
                                worksheet.Cells[excelIndex, 4].Value = project.ReestrProjectId;
                                worksheet.Cells[excelIndex, 5].Value = "Classification";
                                worksheet.Cells[excelIndex, 6].Value = classification.ClassificationType switch
                                {
                                    ReestrProjectClassificationType.ClassificationEgov => "cs.egov.uz",
                                    ReestrProjectClassificationType.OwnClassifications =>
                                        "Shaxsiy tizimlarda mavjud klassifikatorlar",
                                    ReestrProjectClassificationType.ExternalSystemClassifications =>
                                        "Tashqi tizimlardan yuklanuvchi klassificatorlar",
                                    _ => worksheet.Cells[excelIndex, 6].Value
                                };
                                worksheet.Cells[excelIndex, 7].Value = classification.ClassificationUri;

                                excelIndex++;
                            }
                        }

                        if (projectIdentifications != null)
                        {
                            foreach (var identity in projectIdentifications.Identities)
                            {
                                worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                                worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                                {
                                    OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                                    OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                                    OrgCategory.Adminstrations => "Hokimliklar",
                                    _ => worksheet.Cells[excelIndex, 2].Value
                                };
                                worksheet.Cells[excelIndex, 3].Value = project.FullName;
                                worksheet.Cells[excelIndex, 4].Value = project.ReestrProjectId;
                                worksheet.Cells[excelIndex, 5].Value = "Identifikatsiya";
                                worksheet.Cells[excelIndex, 6].Value = identity.IdentitiyType switch
                                {
                                    ReestrProjectIdentityType.Inn => "INN",
                                    ReestrProjectIdentityType.PersonalId => "JSHIR",
                                    ReestrProjectIdentityType.CadastralCode => "Kadastr raqami",
                                    ReestrProjectIdentityType.CarNumber => "Avtotransport raqami",
                                    ReestrProjectIdentityType.GeographicalCode => "Geografik kodi",
                                    _ => worksheet.Cells[excelIndex, 6].Value
                                };
                                worksheet.Cells[excelIndex, 7].Value = identity.IdentityUrl;
                                excelIndex++;
                            }
                        }
                    }
                }
                
                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        public async Task<bool> ActivateDeactivateOrganizations(List<string> userRights, int orgId, bool activation)
        {
            if (userRights.All(p => p != Permissions.SITE_CONTENT_FILLER))
                throw ErrorStates.NotAllowed("permission");
            
            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org != null)
            {
                throw ErrorStates.NotFound(orgId.ToString());
            }

            switch (activation)
            {
                case true:
                    org.IsActive = true;
                    org.IsIct = true;
                    org.IsMonitoring = true;
                    break;
                case false:
                    org.IsActive = false;
                    org.IsIct = false;
                    org.IsMonitoring = false;
                    break;
            }
            _organization.Update(org);

            return true;
        }

        #endregion
        
        #region DownloadOrgData 1.1
        public async Task<MemoryStream> DownloadOrgData(int orgId)
        {
            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();

            if(org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);
             

            var replaceOrgHead = GetReplacerOrgHead(org.Id);

            string fileName = $"{org.ShortName} _details";
            var memoryStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 50;
                worksheet.Columns[2].Width = 40;
                worksheet.DefaultRowHeight = 25;
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;


                #region First basic data about organization

                using (var range = worksheet.Cells[1, 1, 1, 2])
                {
                    range.Value = org.FullName;
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 12;
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                   
                }
                using (var range = worksheet.Cells[2, 1, 2, 2])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                worksheet.Cells[3, 1].Value = "Tashkilot to'liq nomi";
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                worksheet.Cells[3, 2].Value = org.FullName;

                worksheet.Cells[4, 1].Value = "INN";
                worksheet.Cells[4, 2].Value = org.OrgInn;

                worksheet.Cells[5, 1].Value = "Yuridik manzil";
                worksheet.Cells[5, 2].Value = org.AddressProvince + ", " + org.AddressDistrict + ", " + org.AddressStreet + ", " + org.AddressHomeNo;

                worksheet.Cells[6, 1].Value = "Websayt";
                worksheet.Cells[6, 2].Value = org.WebSite;
                
                using(var range = worksheet.Cells[4, 1, 6, 1])
                {
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                }
                using (var range = worksheet.Cells[3, 2, 6, 2])
                {
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }


                worksheet.Cells[8, 1].Value = "Tashkilot rahbari";
                worksheet.Cells[8, 1].Style.Font.Bold = true;
                worksheet.Cells[8, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                worksheet.Cells[8, 2].Value = org.DirectorLastName + " " + org.DirectorMidName + " " + org.DirectorFirstName;

                worksheet.Cells[9, 1].Value = "Lavozimi";
                worksheet.Cells[9, 2].Value = org.DirectorPosition;

                worksheet.Cells[10, 1].Value = "Telefon";
                worksheet.Cells[10, 2].Value = org.PhoneNumber;

                worksheet.Cells[11, 1].Value = "Elektron pochta manzili";
                worksheet.Cells[11, 2].Value = org.DirectorMail;

                using (var range = worksheet.Cells[9, 1, 11, 1])
                {
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                }
                using (var range = worksheet.Cells[8, 2, 11, 2])
                {
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }


                worksheet.Cells[13, 1].Value = "Chief Digital Officer";
                worksheet.Cells[13, 1].Style.Font.Bold = true;
                worksheet.Cells[13, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                worksheet.Cells[13, 2].Value = replaceOrgHead.LastName + " " + replaceOrgHead.MidName + " " + replaceOrgHead.FirstName;

                worksheet.Cells[14, 1].Value = "Lavozimi";
                worksheet.Cells[14, 2].Value = replaceOrgHead.Position;

                worksheet.Cells[15, 1].Value = "Telefon";
                worksheet.Cells[15, 2].Value = replaceOrgHead.Phone;

                worksheet.Cells[16, 1].Value = "Elektron pochta manzili";
                worksheet.Cells[16, 2].Value = replaceOrgHead.Email;

                using (var range = worksheet.Cells[14, 1, 16, 1])
                {
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                }
                using (var range = worksheet.Cells[13, 2, 16, 2])
                {
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }

                #endregion

                var task1 = SetICTDepartmentDetaills(worksheet, org.Id);
                var task2 = SetReestrData(worksheet, orgId);
                var task3 = SetOrgProjectsReport(worksheet, orgId);
                var task4 = SetOrgServicesReport(worksheet, orgId);
                var task5 = SetRanks(worksheet, deadline, org);
                
                await Task.WhenAll(task1, task2, task3, task4, task5);

                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }
        public ReplacerOrgHead GetReplacerOrgHead(int orgId)
        {
            ReplacerOrgHead result = new ReplacerOrgHead();

            var replaceOrgHead = _db.Context.Set<ReplacerOrgHead>().Where(r => r.OrganizationId == orgId).FirstOrDefault();

            if (replaceOrgHead != null)
                result = replaceOrgHead;

            return result;
        }
        private async Task SetICTDepartmentDetaills(ExcelWorksheet worksheet, int orgId)
        {
            OrganizationIctSpecialForces orgSpecialForces = new OrganizationIctSpecialForces();

            var result = _db.Context.Set<OrganizationIctSpecialForces>()
                .FirstOrDefault(r => r.OrganizationId == orgId);

            if (result != null)
                orgSpecialForces = result;

            worksheet.Cells[18, 1].Value = "AKT Bo'linma";
            worksheet.Cells[18, 1].Style.Font.Bold = true;
            worksheet.Cells[18, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            worksheet.Cells[18, 2].Value = orgSpecialForces.MinistryAgreedHead == true ? "ha" : "yo'q";

            worksheet.Cells[19, 1].Value = "AKT bo'linma rahbari";
            worksheet.Cells[19, 2].Value = orgSpecialForces.FullNameDirector;

            worksheet.Cells[20, 1].Value = "Lavozim";
            worksheet.Cells[20, 2].Value = orgSpecialForces.HeadPosition;

            worksheet.Cells[21, 1].Value = "Telefon";
            worksheet.Cells[21, 2].Value = orgSpecialForces.MobilePhone;

            worksheet.Cells[22, 1].Value = "Elektron pochta manzili";
            worksheet.Cells[22, 2].Value = orgSpecialForces.Email;

            worksheet.Cells[23, 1].Value = "Maxsus tarkibiy bo‘linma xodimlarining umumiy soni (jami tizim bo‘yicha)";
            worksheet.Cells[23, 2].Value = orgSpecialForces.EmployeesSum;

            worksheet.Cells[24, 1].Value = "Markaziy boshqaruv apparatida";
            worksheet.Cells[24, 2].Value = orgSpecialForces;

            worksheet.Cells[25, 1].Value = "Hududiy boshqarmalarda";
            worksheet.Cells[25, 2].Value = orgSpecialForces;

            using (var range = worksheet.Cells[19, 1, 25, 1])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            using (var range = worksheet.Cells[18, 2, 25, 2])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }

        }
        private async Task SetReestrData(ExcelWorksheet worksheet, int orgId)
        {
            FirstRequestQuery model = new FirstRequestQuery
            {
                OrgId = orgId,
                Page = 1,
                Limit = 1000000
            };

            FirstRequestQueryResultNew reestrResult = new FirstRequestQueryResultNew();

            reestrResult = await _reesterService.FirstRequestNew(model);

            worksheet.Cells[27, 1].Value = "Axborot tizimlari soni";
            worksheet.Cells[27, 1].Style.Font.Bold = true;
            worksheet.Cells[27, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            worksheet.Cells[27, 2].Value = reestrResult.Items.Count.ToString(CultureInfo.InvariantCulture) + " ta";

            worksheet.Cells[28, 1].Value = "Ekspert xulosalari olinganligi";
            worksheet.Cells[28, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

            worksheet.Cells[28, 2].Value = reestrResult.Items.Where(i => i.HasDigitalTechnologyMinistryExpertise == true).ToList().Count.ToString(CultureInfo.InvariantCulture) + " ta";

            using (var range = worksheet.Cells[27, 2, 28, 2])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }

        }
        private async Task SetOrgProjectsReport(ExcelWorksheet worksheet, int orgId)
        {
            OrganizationDigitalEconomyProjectsReport orgProjects = new OrganizationDigitalEconomyProjectsReport();

            var result = _db.Context
                .Set<OrganizationDigitalEconomyProjectsReport>().FirstOrDefault(r => r.OrganizationId == orgId);
            if (result != null)
                orgProjects = result;
            
            worksheet.Cells[29, 1].Value = "Jami AKT loyihalarining soni";
            worksheet.Cells[29, 1].Style.Font.Bold = true;
            worksheet.Cells[29, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            worksheet.Cells[29, 2].Value = orgProjects.ProjectsCount.ToString(CultureInfo.InvariantCulture) + " ta";

            worksheet.Cells[30, 1].Value = "Bajarilgan";
            worksheet.Cells[30, 2].Value = orgProjects.CompletedProjects.ToString(CultureInfo.InvariantCulture) + " ta";

            worksheet.Cells[31, 1].Value = "Bajarilmoqda";
            worksheet.Cells[31, 2].Value = orgProjects.OngoingProjects.ToString(CultureInfo.InvariantCulture) + " ta";
            
            worksheet.Cells[32, 1].Value = "Bajarilmagan";
            worksheet.Cells[32, 2].Value = orgProjects.NotFinishedProjects.ToString(CultureInfo.InvariantCulture) + " ta";
            
            using (var range = worksheet.Cells[30, 1, 32, 1])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            using (var range = worksheet.Cells[29, 2, 32, 2])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }
        }
        private async Task SetOrgServicesReport(ExcelWorksheet worksheet, int orgId)
        {
            var result = _db.Context
                .Set<OrganizationPublicServices>().Where(r => r.OrganizationId == orgId);
            
            worksheet.Cells[33, 1].Value = "Davlat xizmatlari soni";
            worksheet.Cells[33, 1].Style.Font.Bold = true;
            worksheet.Cells[33, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            worksheet.Cells[33, 2].Value = result.Count().ToString(CultureInfo.InvariantCulture) + " ta";
            
            worksheet.Cells[34, 1].Value = "An'anaviy xizmatlar";
            worksheet.Cells[34, 2].Value = result.Count(s => s.ServiceType == OrganizationServiceType.National && s.ServiceTypeExpert == true)
                .ToString(CultureInfo.InvariantCulture) + " ta";
            
            worksheet.Cells[35, 1].Value = "Elektron xizmatlar";
            worksheet.Cells[35, 2].Value = result.Count(s => s.ServiceType == OrganizationServiceType.Electronic && s.ServiceTypeExpert == true)
                .ToString(CultureInfo.InvariantCulture) + " ta";
            
            worksheet.Cells[36, 1].Value = "Elektron xizmatlar (YIDXP orqali)";
            worksheet.Cells[36, 2].Value = result.Count(s => s.MyGovService == true && s.MyGovServiceExpert == true)
                .ToString(CultureInfo.InvariantCulture) + " ta";
            
            worksheet.Cells[37, 1].Value = "Elektron xizmatlar (Muqobil xizmatlar orqali)";
            worksheet.Cells[37, 2].Value = result.Count(s => s.OtherApps == true && s.OtherAppsExpert == true)
                .ToString(CultureInfo.InvariantCulture) + " ta";
            
            using (var range = worksheet.Cells[34, 1, 37, 1])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            using (var range = worksheet.Cells[33, 2, 37, 2])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }
        }
        private async Task SetRanks(ExcelWorksheet worksheet, Deadline deadline, Organizations organization)
        {
            using (var range = worksheet.Cells[39, 1, 39, 2])
            {
                range.Value = "REYTING NATIJALARI";
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 12;
                range.Merge = true;
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                   
            }

            var excelIndex = 39;
            
            switch (organization.OrgCategory)
            {
                case Domain.Enums.OrgCategory.GovernmentOrganizations:
                {
                    var spheres = _gSphere.GetAll().Include(mbox => mbox.GFields).OrderBy(s => s.Section).ToList();
                    foreach(var sphere in spheres)
                    {
                        excelIndex += 2;
                        double sphereRate = 0;
                        foreach (var field in sphere.GFields)
                        {
                            var fieldRank = GetFieldRank(deadline, organization, field.Id).Result;
                            sphereRate += fieldRank;

                            worksheet.Cells[excelIndex, 1].Value = $"{field.Section} {field.Name}";
                            worksheet.Cells[excelIndex, 2].Value = fieldRank;

                            excelIndex++;
                        }

                        worksheet.Cells[excelIndex - (sphere.GFields.Count() + 1), 1].Value =
                            $"{sphere.Section} {sphere.Name}";
                        worksheet.Cells[excelIndex - (sphere.GFields.Count() + 1), 1].Style.Font.Bold = true;
                        worksheet.Cells[excelIndex - (sphere.GFields.Count() + 1), 2].Value = sphereRate;
                    }
                    
                    break;
                }
                    
                case Domain.Enums.OrgCategory.FarmOrganizations:
                {
                    var spheres = _xSphere.GetAll().Include(mbox => mbox.XFields).OrderBy(s => s.Section);
                    foreach(var sphere in spheres)
                    {
                        excelIndex += 2;
                        double sphereRate = 0;
                        foreach (var field in sphere.XFields)
                        {
                            var fieldRank = GetFieldRank(deadline, organization, field.Id).Result;
                            sphereRate += fieldRank;

                            worksheet.Cells[excelIndex, 1].Value = $"{field.Section} {field.Name}";
                            worksheet.Cells[excelIndex, 2].Value =  fieldRank;

                            excelIndex++;
                        }

                        worksheet.Cells[excelIndex - (sphere.XFields.Count() + 1), 1].Value =
                            $"{sphere.Section} {sphere.Name}";
                        worksheet.Cells[excelIndex - (sphere.XFields.Count() + 1), 1].Style.Font.Bold = true;
                        worksheet.Cells[excelIndex - (sphere.XFields.Count() + 1), 2].Value = sphereRate;
                    }
                    
                    break;
                }
                    
                case Domain.Enums.OrgCategory.Adminstrations:
                {
                    var spheres = _aSphere.GetAll().Include(mbox => mbox.AFields).OrderBy(s => s.Section);
                    foreach(var sphere in spheres)
                    {
                        excelIndex += 2;
                        double sphereRate = 0;
                        foreach (var field in sphere.AFields)
                        {
                            var fieldRank = GetFieldRank(deadline, organization, field.Id).Result;
                            sphereRate += fieldRank;

                            worksheet.Cells[excelIndex, 1].Value = $"{field.Section} {field.Name}";
                            worksheet.Cells[excelIndex, 2].Value = fieldRank;

                            excelIndex++;
                        }

                        worksheet.Cells[excelIndex - (sphere.AFields.Count() + 1), 1].Value =
                            $"{sphere.Section} {sphere.Name}";
                        worksheet.Cells[excelIndex - (sphere.AFields.Count() + 1), 1].Style.Font.Bold = true;
                        worksheet.Cells[excelIndex - (sphere.AFields.Count() + 1), 2].Value = sphereRate;
                    }
                    
                    break;
                }
                default:
                    throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            }
            using (var range = worksheet.Cells[40, 1, excelIndex, 1])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            }
            using (var range = worksheet.Cells[40, 2, excelIndex, 2])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            }
        }
        #endregion

        #region DownloadOrgReestrProjectsReport 5.1

        public async Task<MemoryStream> DownloadOrganizationsReestrReport(List<string> userRights, int userOrgId)
        {
            #region CheckRequestData

            var organizations = _organization.Find(o => o.IsActive == true & o.IsIct == true).OrderBy(o=>o.OrgCategory).ThenBy(o=>o.Id).ToList();

            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS))
            {
                organizations = organizations.Where(o => o.UserServiceId == userOrgId).ToList();
            }

            if (organizations.Count == 0)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            #endregion

            int excelIndex = 0;
            string fileName = "Axborot_tizimlari";
            var memoryStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 40;
                worksheet.Columns[2].Width = 20;
                worksheet.Columns[3].Width = 40;
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;


                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 1, 4])
                {
                    range.Value = "Axborot tizimlari haqida ma'lumot";
                    range.Style.Font.Size = 12;
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                   
                }
                using (var range = worksheet.Cells[2, 1, 2, 4])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                using (var range = worksheet.Cells[3, 1, 3, 50])
                {
                    range.Style.Font.Size = 12;
                    range.Style.WrapText = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                }

                worksheet.Cells[3, 1].Value = "Tashkilot to'liq nomi";
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 2].Value = "Tashkilot turi";
                worksheet.Cells[3, 2].Style.Font.Bold = true;
                worksheet.Cells[3, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 3].Value = "Axborot tizimi nomi";
                worksheet.Cells[3, 3].Style.Font.Bold = true;
                worksheet.Cells[3, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 4].Value = "Id raqami";
                worksheet.Cells[3, 4].Style.Font.Bold = true;
                worksheet.Cells[3, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 5].Value = "Istisno (ha/yo'q)";
                worksheet.Cells[3, 5].Style.Font.Bold = true;
                worksheet.Cells[3, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 6].Value = " Passport (tasdiqlangan/tasdiqlanmagan)";
                worksheet.Cells[3, 6].Style.Font.Bold = true;
                worksheet.Cells[3, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 7].Value = "Axborot tizimi uchun qo'yilgan baho";
                worksheet.Cells[3, 7].Style.Font.Bold = true;
                worksheet.Cells[3, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 8].Value = "Joriy holati";
                worksheet.Cells[3, 8].Style.Font.Bold = true;
                worksheet.Cells[3, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 9].Value = "Tashqi tizimlar bilan bog‘langanligi (Tashkilot ko'rsatgan son)";
                worksheet.Cells[3, 9].Style.Font.Bold = true;
                worksheet.Cells[3, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 10].Value = "Tashqi tizimlar bilan bog‘langanligi (Ekspert tasdiqlagan son)";
                worksheet.Cells[3, 10].Style.Font.Bold = true;
                worksheet.Cells[3, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 11].Value = "Ma’lumotnomalar va klassifikatorlardan foydalanganligi (Tashkilot ko'rsatgan son)";
                worksheet.Cells[3, 11].Style.Font.Bold = true;
                worksheet.Cells[3, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 12].Value = "Ma’lumotnomalar va klassifikatorlardan foydalanganligi (Ekspert tasdiqlagan son)";
                worksheet.Cells[3, 12].Style.Font.Bold = true;
                worksheet.Cells[3, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 13].Value = "Yagona identifikatorlardan foydalanganligi (Tashkilot ko'rsatgan son)";
                worksheet.Cells[3, 13].Style.Font.Bold = true;
                worksheet.Cells[3, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 14].Value = "Yagona identifikatorlardan foydalanganligi (Ekspert tasdiqlagan son)";
                worksheet.Cells[3, 14].Style.Font.Bold = true;
                worksheet.Cells[3, 14].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 15].Value = "Raqamli texnologiyalar vazirligi ekspert xulosasi mavjudligi";
                worksheet.Cells[3, 15].Style.Font.Bold = true;
                worksheet.Cells[3, 15].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 16].Value = "Izoh";
                worksheet.Cells[3, 16].Style.Font.Bold = true;
                worksheet.Cells[3, 16].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 17].Value = "“Kiberxavfsizlik Markazi” DUK Ekspert xulosasi mavjudligi";
                worksheet.Cells[3, 17].Style.Font.Bold = true;
                worksheet.Cells[3, 17].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 18].Value = "Izoh";
                worksheet.Cells[3, 18].Style.Font.Bold = true;
                worksheet.Cells[3, 18].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 19].Value = "Tashqi foydalanuvchilar uchun avtorizasiyaning avtomatlashtirilganligi (Tashkilot ko'rsatgan son)";
                worksheet.Cells[3, 19].Style.Font.Bold = true;
                worksheet.Cells[3, 19].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 20].Value = "Tashqi foydalanuvchilar uchun avtorizasiyaning avtomatlashtirilganligi (Ekspert tasdiqlagan son)";
                worksheet.Cells[3, 20].Style.Font.Bold = true;
                worksheet.Cells[3, 20].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 21].Value = "Avtomatlashtirilgan xizmatlar va funksiyalari (Tashkilot ko'rsatgan son)";
                worksheet.Cells[3, 21].Style.Font.Bold = true;
                worksheet.Cells[3, 21].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 22].Value = "Avtomatlashtirilgan xizmatlar va funksiyalari (Ekspert tasdiqlagan son)";
                worksheet.Cells[3, 22].Style.Font.Bold = true;
                worksheet.Cells[3, 22].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 23].Value = "Avtomatlashtirilgan xizmatlar va funksiyalar bo'yicha izoh";
                worksheet.Cells[3, 23].Style.Font.Bold = true;
                worksheet.Cells[3, 23].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 24].Value = "Axborot tizimlari samaradorligi bo'yicha olgan bahosi (Tashkilot ko'rsatgan son)";
                worksheet.Cells[3, 24].Style.Font.Bold = true;
                worksheet.Cells[3, 24].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 25].Value = "Axborot tizimlari samaradorligi bo'yicha olgan bahosi (Ekspert tasdiqlagan son)";
                worksheet.Cells[3, 25].Style.Font.Bold = true;
                worksheet.Cells[3, 25].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                worksheet.Cells[3, 26].Value = "Izoh";
                worksheet.Cells[3, 26].Style.Font.Bold = true;
                worksheet.Cells[3, 26].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                
                #endregion

                #region TemplateDataToSendToFunctions

                var exceptions = _reestrException.GetAll().ToList();
                List<int> exceptionPassportsId = exceptions.Select(e => e.Id).ToList();

                var projectPositions = _reestrProjectPosition.GetAll().ToList();
                projectPositions = projectPositions.Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();

                var reestrProjectConnection = _reestrProjectConnection.GetAll().ToList();
                reestrProjectConnection = reestrProjectConnection.Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();

                var reestrProjectClassifications = _reestrClassifications.GetAll().ToList();
                reestrProjectClassifications = reestrProjectClassifications
                    .Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();

                var reestrProjectIdentities = _reestrIdentities.GetAll().ToList();
                reestrProjectIdentities = reestrProjectIdentities
                    .Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();

                var reestrProjectExpertDecision = _reestrProjectExpertDecision.GetAll().ToList();
                reestrProjectExpertDecision = reestrProjectExpertDecision
                    .Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();
                
                var reestrProjectCyberSecurityExpertDecision = _reestrProjectCyberSecurityExpertDecision.GetAll().ToList();
                reestrProjectCyberSecurityExpertDecision = reestrProjectCyberSecurityExpertDecision
                    .Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();
                
                var reestrProjectAuthorizations = _reestrProjectAuthorization.GetAll().ToList();
                reestrProjectAuthorizations = reestrProjectAuthorizations
                    .Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();
                
                var reestrProjectAutomatedServices = _reestrProjectAutomatedServices.GetAll().ToList();
                reestrProjectAutomatedServices = reestrProjectAutomatedServices
                    .Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();

                var reestrProjectEfficiencies = _reestrProjectEfficiency.GetAll().ToList();
                reestrProjectEfficiencies = reestrProjectEfficiencies
                    .Where(p => exceptionPassportsId.Any(i => i != p.ReestrProjectId)).ToList();

                #endregion

                excelIndex = 4;
                
                foreach (var org in organizations)
                {
                    var passports = _reestrProjectPassport.Find(p => p.OrganizationId == org.Id).ToList();

                    foreach (var passport in passports)
                    {

                        await SetPassportBasics(worksheet, passport, excelIndex, org, exceptions, projectPositions);
                        await SetProjectConnections(worksheet, passport, excelIndex, reestrProjectConnection);
                        await SetProjectClassifications(worksheet, passport, excelIndex, reestrProjectClassifications);
                        await SetProjectIdentities(worksheet, passport, excelIndex, reestrProjectIdentities);
                        await SetProjectIdentities(worksheet, passport, excelIndex, reestrProjectIdentities);
                        await SetProjectExpertDecisions(worksheet, passport, excelIndex, reestrProjectExpertDecision,
                            reestrProjectCyberSecurityExpertDecision);
                        await SetProjectAuthorization(worksheet, passport, excelIndex, reestrProjectAuthorizations);
                        await SetProjectAutomatedServices(worksheet, passport, excelIndex, reestrProjectAutomatedServices);
                        await SetProjectEfficiency(worksheet, passport, excelIndex, reestrProjectEfficiencies);
                        excelIndex++;
                    }
                }
                

                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        private async Task SetPassportBasics(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex, Organizations org, 
            List<ReestrProjectException> exceptions, List<ReestrProjectPosition> projectPositions)
        {

            var projectPosition = projectPositions.FirstOrDefault(p => p.ReestrProjectId == passport.ReestrProjectId);
            
            worksheet.Cells[rowIndex, 1].Value = org.ShortName;
            switch (org.OrgCategory)
            {
                case OrgCategory.GovernmentOrganizations:
                    worksheet.Cells[rowIndex, 2].Value = "Davlat boshqaruvi";
                    break;
                case OrgCategory.FarmOrganizations:
                    worksheet.Cells[rowIndex, 2].Value = "Xo'jalik boshqaruvi";
                    break;
                case OrgCategory.Adminstrations:
                    worksheet.Cells[rowIndex, 2].Value = "Hokimlik";
                    break;
                default:
                    worksheet.Cells[rowIndex, 2].Value = "";
                    break;
            }
            worksheet.Cells[rowIndex, 3].Value = passport.ShortName;
            worksheet.Cells[rowIndex, 4].Value = passport.ReestrProjectId.ToString();
            worksheet.Cells[rowIndex, 5].Value =
                exceptions.Any(e => e.ReestrProjectId == passport.ReestrProjectId) ? "ha" : "yo'q";
            worksheet.Cells[rowIndex, 6].Value = passport.PassportStatus == ReesterProjectStatus.CONFIRMED ? "ha" : "yo'q";

            if (projectPosition != null)
            {
                switch (projectPosition.ProjectStatus)
                {
                    case ReestrProjectStatusInNis.WorkingStage:
                        worksheet.Cells[rowIndex, 8].Value = "Ish holatida";
                        break;
                    case ReestrProjectStatusInNis.DevelopmentStage:
                        worksheet.Cells[rowIndex, 8].Value = "Ishlab chiqish jarayonida";
                        break;
                    case ReestrProjectStatusInNis.TestStage:
                        worksheet.Cells[rowIndex, 8].Value = "Test holatida";
                        break;
                    case ReestrProjectStatusInNis.ModerationStage:
                        worksheet.Cells[rowIndex, 8].Value = "Moderatsiya jarayonida";
                        break;
                    case ReestrProjectStatusInNis.Decommissioned:
                        worksheet.Cells[rowIndex, 8].Value = "Ishlatilmaydi";
                        break;
                }
            }
        }

        private async Task SetProjectConnections(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex,
            List<ReestrProjectConnection> connections)
        {
            var connection = connections.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (connection != null)
            {
                worksheet.Cells[rowIndex, 9].Value = connection.AllItems.ToString();
                worksheet.Cells[rowIndex, 10].Value = connection.ExceptedItems.ToString();
            }
        }
        private async Task SetProjectClassifications(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex,
            List<ReestrProjectClassifications> classifications)
        {
            var classification = classifications.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (classification != null)
            {
                worksheet.Cells[rowIndex, 11].Value = classification.AllItems.ToString();
                worksheet.Cells[rowIndex, 12].Value = classification.ExceptedItems.ToString();
            }
        }
        private async Task SetProjectIdentities(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex,
            List<ReestrProjectIdentities> identities)
        {
            var identity = identities.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (identity != null)
            {
                worksheet.Cells[rowIndex, 13].Value = identity.AllItems.ToString();
                worksheet.Cells[rowIndex, 14].Value = identity.ExceptedItems.ToString();
            }
        }
        private async Task SetProjectExpertDecisions(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex,
            List<ReestrProjectExpertDecision> expertDecisions, List<ReestrProjectCyberSecurityExpertDecision> cyberSecurityExpertDecisions)
        {
            var expertDecision = expertDecisions.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (expertDecision != null)
            {
                worksheet.Cells[rowIndex, 15].Value = expertDecision.Exist == true ? "ha" : "yo'q";
                worksheet.Cells[rowIndex, 16].Value = expertDecision.ExpertComment;
            }
            var cyberSecurityExpertDecision = cyberSecurityExpertDecisions.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (cyberSecurityExpertDecision != null)
            {
                worksheet.Cells[rowIndex, 17].Value = cyberSecurityExpertDecision.Exist == true ? "ha" : "yo'q";
                worksheet.Cells[rowIndex, 18].Value = cyberSecurityExpertDecision.ExpertComment;
            }
        }
        
        private async Task SetProjectAuthorization(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex,
            List<ReestrProjectAuthorizations> authorizations)
        {
            var authorization = authorizations.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (authorization != null)
            {
                worksheet.Cells[rowIndex, 19].Value = authorization.AllItems.ToString();
                worksheet.Cells[rowIndex, 20].Value = authorization.ExceptedItems.ToString();
            }
        }
        
        private async Task SetProjectAutomatedServices(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex,
            List<ReestrProjectAutomatedServices> automatedServices)
        {
            var automatedService = automatedServices.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (automatedService != null)
            {
                worksheet.Cells[rowIndex, 21].Value = automatedService.AllItems.ToString();
                worksheet.Cells[rowIndex, 22].Value = automatedService.ExceptedItems.ToString();
                worksheet.Cells[rowIndex, 23].Value = automatedService.ExpertComment;
            }
        }
        private async Task SetProjectEfficiency(ExcelWorksheet worksheet, ReestrProjectPassport passport, int rowIndex,
            List<ReestrProjectEfficiency> efficiencies)
        {
            var efficiency = efficiencies.FirstOrDefault(c => c.ReestrProjectId == passport.ReestrProjectId);
            if (efficiency != null)
            {
                worksheet.Cells[rowIndex, 24].Value = efficiency.AllItems.ToString();
                worksheet.Cells[rowIndex, 25].Value = efficiency.ExceptedItems.ToString();
                worksheet.Cells[rowIndex, 26].Value = efficiency.ExpertComment;
            }
        }
        #endregion

        #region DownloadITDepartmentReport 6.2

        public async Task<MemoryStream> DownloadITDepartmentReport(List<string> userRights, int userOrgId)
        {
            if (!userRights.Contains(Permissions.OPERATOR_RIGHTS) && !userRights.Contains(Permissions.ORGANIZATION_EMPLOYEE))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            var deadline = _deadline.Find(d => d.IsActive == true && d.PingService == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var organizations = _organization.Find(o => o.IsActive == true && o.IsIct == true)
                .OrderBy(o => o.OrgCategory).ToList();

            if (userRights.Contains(Permissions.ORGANIZATION_EMPLOYEE))
                organizations = organizations.Where(o => o.Id == userOrgId).ToList();
            
            var specialForces = _orgSpecialForces.GetAll().ToList();
            
            string fileName = "OrgHelplineReport";
            var memoryStream = new MemoryStream();
            
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 60;
                worksheet.Columns[2].Width = 25;
                
                worksheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;

                #region SetHeader

                using (var range = worksheet.Cells[1, 1, 2, 6])
                {
                    range.Value = "AKT BO'LINMA RAHBARIGA NOMZODNING KELISHILGANLIGI";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Font.Size = 12;
                }

                using (var range = worksheet.Cells[3, 1, 3, 6])
                {
                    range.Value = deadline.Year + "- yil " + (int)deadline.Quarter + " - yarim yilligi";
                    range.Merge = true;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.Font.Size = 10;
                }

                using (var range = worksheet.Cells[4, 1, 5, 1])
                {
                    range.Value = "Tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 2, 5, 2])
                {
                    range.Value = "Tashkilot turi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 3, 5, 3])
                {
                    range.Value = "Maxsus tarkibiy boʻlinma mavjudligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 4, 5, 4])
                {
                    range.Value = "Maxsus tarkibiy bo‘linma yoki autsorsing shartnomasi tuzilgan tashqi tashkilot nomi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 5, 5, 5])
                {
                    range.Value = "Tashkilot shakli";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 6, 5, 6])
                {
                    range.Value = "Maxsus tarkibiy bo‘linma rahbari F.I.Sh";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 7, 5, 7])
                {
                    range.Value = "Lavozim";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 8, 5, 8])
                {
                    range.Value = "Rahbarning tayinlanishi Raqamli texnologiyalar vazirligi bilan kelishilgan(Ha/yo'q)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 9, 5, 9])
                {
                    range.Value = "Maxsus tarkibiy bo‘linma hodimlarining umumiy soni(jami tizim bo‘yicha)";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 10, 5, 10])
                {
                    range.Value = "Markaziy boshqaruv apparatida";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 11, 5, 11])
                {
                    range.Value = "Viloyat, shahar va tumanlardagi hududiy boshqarmalarda";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 12, 5, 12])
                {
                    range.Value = "Idoraviy mansub tashkilotlarda";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 13, 5, 13])
                {
                    range.Value = "Shulardan AX bo‘yicha mutaxassislar soni";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 14, 5, 14])
                {
                    range.Value = "Shulardan axborot tizimlari va ma'lumotlar bazasi bo‘yicha mutaxassislar soni";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 15, 5, 15])
                {
                    range.Value = "Maxsus tarkibiy bo‘linmaning ish rejasi mavjudligi";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 16, 5, 16])
                {
                    range.Value = "O‘tgan yil davomida AKT uchun ajratilgan o‘zlashtirilgan mablag‘lar miqdori, mln. so‘m";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 17, 5, 17])
                {
                    range.Value = "Hisobot yilida maxsus AKT bo‘linmalarini ta'minlash uchun ajratilgan mablag‘lar miqdori mln. so‘m";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 18, 5, 18])
                {
                    range.Value = "Hisobot yilida AKT uchun o‘zlashtirilgna mablag‘lar miqdori";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 19, 5, 19])
                {
                    range.Value = "Keyingi yilda AKT maxsus bo‘linmasini ta'minlash uchun rejalashtirilgan mablag‘lar miqdori, mln. so‘m";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                using (var range = worksheet.Cells[4, 20, 5, 20])
                {
                    range.Value = "Hisobot yilida AKT bo‘yicha ishlarni bajarish uchun autsorsing kompaniyasiga ajratilgan mablag‘lar miqdori, mln. so‘m";
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range.Style.WrapText = true;
                    range.Style.Font.Size = 11;
                    range.Merge = true;
                }
                #endregion

                int excelIndex = 6;

                foreach (var organization in organizations)
                {
                    var orgItDepartment = specialForces.FirstOrDefault(s => s.OrganizationId == organization.Id);

                    worksheet.Cells[excelIndex, 1].Value = organization.ShortName;
                    worksheet.Cells[excelIndex, 2].Value = organization.OrgCategory switch
                    {
                        OrgCategory.GovernmentOrganizations => "Davlat boshqaruvi",
                        OrgCategory.FarmOrganizations => "Xo'jalik boshqaruvi",
                        OrgCategory.Adminstrations => "Hokimliklar",
                        _ => worksheet.Cells[excelIndex, 2].Value
                    };
                    if (orgItDepartment != null)
                    {
                        worksheet.Cells[excelIndex, 3].Value = orgItDepartment.HasSpecialForces == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 4].Value = orgItDepartment.SpecialForcesName;
                        worksheet.Cells[excelIndex, 5].Value = orgItDepartment.FormOfSpecialForces;
                        worksheet.Cells[excelIndex, 6].Value = orgItDepartment.FullNameDirector;
                        worksheet.Cells[excelIndex, 7].Value = orgItDepartment.HeadPosition;
                        worksheet.Cells[excelIndex, 8].Value = orgItDepartment.MinistryAgreedHead == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 9].Value = orgItDepartment.EmployeesSum.ToString();
                        worksheet.Cells[excelIndex, 10].Value = orgItDepartment.CentralofficeEmployees.ToString();
                        worksheet.Cells[excelIndex, 11].Value = orgItDepartment.RegionalEmployees.ToString();
                        worksheet.Cells[excelIndex, 12].Value = orgItDepartment.SubordinateEmployees.ToString();
                        worksheet.Cells[excelIndex, 13].Value = orgItDepartment.InformationSecurityEmployees.ToString();
                        worksheet.Cells[excelIndex, 14].Value = orgItDepartment.InformationSystemDatabaseEmployees.ToString();
                        worksheet.Cells[excelIndex, 15].Value =
                            orgItDepartment.HasWorkPlanOfSpecialForces == true ? "Ha" : "Yo'q";
                        worksheet.Cells[excelIndex, 16].Value = orgItDepartment.LastYearAmountOfFunds.ToString();
                        worksheet.Cells[excelIndex, 17].Value = orgItDepartment.AmountOfFunds.ToString();
                        worksheet.Cells[excelIndex, 18].Value = orgItDepartment.AmountOfSpentFund.ToString();
                        worksheet.Cells[excelIndex, 19].Value = orgItDepartment.NextYearFundForKeepingForces.ToString();
                        worksheet.Cells[excelIndex, 20].Value = orgItDepartment.OutsourcingSpentFund.ToString();   
                    }
                    excelIndex++;
                    
                }
                
                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        #endregion
        
        #region DownloadOrganizationsRateReport
        public async Task<MemoryStream> DownloadOrganizationsRateReport(OrgCategory category, int deadlineId)
        {
            rankExcelStartIndex = 1;
            
            var deadline = _deadline.Find(d => d.Id == deadlineId).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);
            
            string fileName = "Rank_details";
            var memoryStream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.Add(fileName);
                worksheet.Columns[1].Width = 50;
                worksheet.DefaultRowHeight = 20;
                worksheet.DefaultColWidth = 15;
                
                if (category != 0)
                {
                    switch (category)
                    {
                        case OrgCategory.Adminstrations:
                            await SetAdministrationRateReport(worksheet, deadline, rankExcelStartIndex);
                            break;
                        case OrgCategory.GovernmentOrganizations:
                            await SetGovernmentRateReport(worksheet, deadline, rankExcelStartIndex);
                            break;
                        case OrgCategory.FarmOrganizations:
                            await SetFarmRateReport(worksheet, deadline, rankExcelStartIndex);
                            break;
                    }
                }
                package.Save();
            }
            memoryStream.Flush();
            memoryStream.Position = 0;


            return await Task.FromResult(memoryStream);
        }

        private async Task SetGovernmentRateReport(ExcelWorksheet worksheet, Deadline deadline, int excelStartIndex)
        {
            var gRankTable = _gRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();
            rankExcelStartIndex = excelStartIndex;
            int columnIndex = 1;
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.GovernmentOrganizations && o.IsActive == true && o.IsIct == true).ToList();

            var spheres = _gSphere.GetAll().Include(mbox => mbox.GFields).ThenInclude(mbox => mbox.GSubFields)
                .OrderBy(s => s.Section).ToList();

            worksheet.Cells[rankExcelStartIndex, columnIndex].Value = "Organizations";
            worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = "Maximal Ball";
            columnIndex++;
            foreach (var sphere in spheres)
            {
                foreach (var field in sphere.GFields)
                {
                    if (field.Section != "2.3")
                    {
                        worksheet.Cells[rankExcelStartIndex, columnIndex].Value =
                            $"{field.Section} {field.Name}";
                        worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = $"{field.MaxRate.ToString(CultureInfo.InvariantCulture)}";
                        columnIndex++; 
                    }
                }

                worksheet.Cells[rankExcelStartIndex, columnIndex].Value =
                    $"{sphere.Section} {sphere.Name}";
                worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = $"{sphere.MaxRate.ToString(CultureInfo.InvariantCulture)}";
                columnIndex++;
            }
            worksheet.Rows[rankExcelStartIndex].Height = 100;
            worksheet.Rows[rankExcelStartIndex+1].Height = 30;

            using (var range = worksheet.Cells[rankExcelStartIndex, 1, rankExcelStartIndex+1, columnIndex])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                range.Style.WrapText = true;
                range.Style.Font.Bold = true;
            }

            rankExcelStartIndex += 2;
            
            foreach (var org in organizations)
            {
                columnIndex = 1;
                worksheet.Cells[rankExcelStartIndex, columnIndex].Value = $"{org.ShortName}";
                columnIndex++;    
                foreach(var sphere in spheres)
                {
                    double sphereRate = 0;
                    foreach (var field in sphere.GFields)
                    {
                        if (field.Section != "2.3")
                        {
                            var fieldRank = await GetFieldRankGovernment(gRankTable, org, field);
                            worksheet.Cells[rankExcelStartIndex, columnIndex].Value = fieldRank;
                            columnIndex++;
                            sphereRate += Math.Round(fieldRank, 2); 
                        }
                    }
                    worksheet.Cells[rankExcelStartIndex, columnIndex].Value = sphereRate;
                    columnIndex++;
                }
                rankExcelStartIndex++;
            }

            rankExcelStartIndex += 3;
        }
        private async Task SetFarmRateReport(ExcelWorksheet worksheet, Deadline deadline, int excelStartIndex)
        {

            var xRankTable = _xRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();
            rankExcelStartIndex = excelStartIndex;
            int columnIndex = 1;
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.FarmOrganizations && o.IsActive == true && o.IsIct == true).ToList();

            var spheres = _xSphere.GetAll().Include(mbox => mbox.XFields).ThenInclude(mbox => mbox.XSubFields)
                .OrderBy(s => s.Section).ToList();

            worksheet.Cells[rankExcelStartIndex, columnIndex].Value = "Organizations";
            worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = "Maximal Ball";
            columnIndex++;
            foreach (var sphere in spheres)
            {
                foreach (var field in sphere.XFields)
                {
                    if (field.Section != "2.3")
                    {
                        worksheet.Cells[rankExcelStartIndex, columnIndex].Value =
                            $"{field.Section} {field.Name}";
                        worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = $"{field.MaxRate.ToString(CultureInfo.InvariantCulture)}";
                        columnIndex++;
                    }
                }

                worksheet.Cells[rankExcelStartIndex, columnIndex].Value =
                    $"{sphere.Section} {sphere.Name}";
                worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = $"{sphere.MaxRate.ToString(CultureInfo.InvariantCulture)}";
                columnIndex++;
            }
            worksheet.Rows[rankExcelStartIndex].Height = 100;
            worksheet.Rows[rankExcelStartIndex + 1].Height = 30;

            using (var range = worksheet.Cells[rankExcelStartIndex, 1, rankExcelStartIndex + 1, columnIndex])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                range.Style.WrapText = true;
                range.Style.Font.Bold = true;
            }

            rankExcelStartIndex += 2;

            foreach (var org in organizations)
            {
                columnIndex = 1;
                worksheet.Cells[rankExcelStartIndex, columnIndex].Value = $"{org.ShortName}";
                columnIndex++;
                foreach (var sphere in spheres)
                {
                    double sphereRate = 0;
                    foreach (var field in sphere.XFields)
                    {
                        if (field.Section != "2.3")
                        {
                            var fieldRank = await GetFieldRankFarm(xRankTable, org, field);
                            worksheet.Cells[rankExcelStartIndex, columnIndex].Value = fieldRank;
                            columnIndex++;
                            sphereRate += Math.Round(fieldRank, 2);
                        }
                    }
                    worksheet.Cells[rankExcelStartIndex, columnIndex].Value = sphereRate;
                    columnIndex++;
                }
                rankExcelStartIndex++;
            }

            rankExcelStartIndex += 3;
        }
        private async Task SetAdministrationRateReport(ExcelWorksheet worksheet, Deadline deadline, int excelStartIndex)
        {
            var aRankTable = _aRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();
            rankExcelStartIndex = excelStartIndex;
            int columnIndex = 1;
            var organizations = _organization.Find(o =>
                o.OrgCategory == OrgCategory.Adminstrations && o.IsActive == true && o.IsIct == true).ToList();

            var spheres = _aSphere.GetAll().Include(mbox => mbox.AFields).ThenInclude(mbox => mbox.ASubFields)
                .OrderBy(s => s.Section).ToList();

            worksheet.Cells[rankExcelStartIndex, columnIndex].Value = "Organizations";
            worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = "Maximal Ball";
            columnIndex++;
            foreach (var sphere in spheres)
            {
                foreach (var field in sphere.AFields)
                {
                    if (field.Section != "2.3")
                    {
                        worksheet.Cells[rankExcelStartIndex, columnIndex].Value =
                            $"{field.Section} {field.Name}";
                        worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = $"{field.MaxRate.ToString(CultureInfo.InvariantCulture)}";
                        columnIndex++;
                    }
                }

                worksheet.Cells[rankExcelStartIndex, columnIndex].Value =
                    $"{sphere.Section} {sphere.Name}";
                worksheet.Cells[rankExcelStartIndex + 1, columnIndex].Value = $"{sphere.MaxRate.ToString(CultureInfo.InvariantCulture)}";
                columnIndex++;
            }
            worksheet.Rows[rankExcelStartIndex].Height = 100;
            worksheet.Rows[rankExcelStartIndex + 1].Height = 30;

            using (var range = worksheet.Cells[rankExcelStartIndex, 1, rankExcelStartIndex + 1, columnIndex])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                range.Style.WrapText = true;
                range.Style.Font.Bold = true;
            }

            rankExcelStartIndex += 2;

            foreach (var org in organizations)
            {
                columnIndex = 1;
                worksheet.Cells[rankExcelStartIndex, columnIndex].Value = $"{org.ShortName}";
                columnIndex++;
                foreach (var sphere in spheres)
                {
                    double sphereRate = 0;
                    foreach (var field in sphere.AFields)
                    {
                        if (field.Section != "2.3")
                        {
                            var fieldRank = await GetFieldRankAdministration(aRankTable, org, field);
                            worksheet.Cells[rankExcelStartIndex, columnIndex].Value = fieldRank;
                            columnIndex++;
                            sphereRate += Math.Round(fieldRank, 2);
                        }
                    }
                    worksheet.Cells[rankExcelStartIndex, columnIndex].Value = sphereRate;
                    columnIndex++;
                }
                rankExcelStartIndex++;
            }

            rankExcelStartIndex += 3;
        }
        #endregion

        #region GetFieldRankByOrgTypehelpers
        private async Task<double> GetFieldRank(Deadline deadline, Organizations organization, int fieldId)
        {
            double fieldRank = 0;

            switch (organization.OrgCategory)
            {
                case Domain.Enums.OrgCategory.GovernmentOrganizations:
                {
                    var field = _gField.Find(f => f.Id == fieldId).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                    var rank = _gRankTable.Find(r =>
                        r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter &&
                        r.SphereId == field.SphereId && r.FieldId == field.Id).ToList();
                    if (rank.Count > 0)
                    {
                        var subField = _gSubField.Find(s => s.FieldId == field.Id).ToList();
                        if (subField.Any())
                        {
                            foreach (var sField in subField)
                            {
                                var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                                if (subFieldRankWithElements.Any())
                                {
                                    var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                                    fieldRank += subfieldRankMedium;
                                }
                            
                                var subFieldRankWithoutElements = rank.FirstOrDefault(r => r.SubFieldId == sField.Id && r.ElementId == 0);
                                if (subFieldRankWithoutElements != null)
                                {
                                    fieldRank += Math.Round(subFieldRankWithoutElements.Rank, 2);
                                }
                            }
                            return await Task.FromResult<double>(fieldRank);
                        }
                        else
                        {
                            var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                            if (rankWithElements.Count > 0)
                            {
                                fieldRank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                            }
                        
                            var rankWithouthElements = rank.FirstOrDefault(r => r.SubFieldId == 0 && r.ElementId == 0);
                            if (rankWithouthElements != null)
                            {
                                fieldRank = Math.Round(rankWithouthElements.Rank, 2);
                            }
                            return await Task.FromResult<double>(fieldRank);
                        }
                    }
                    else
                    {
                        return await Task.FromResult<double>(0);
                    }
                }
                case Domain.Enums.OrgCategory.FarmOrganizations:
                {
                    var field = _xField.Find(f => f.Id == fieldId).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                    var rank = _xRankTable.Find(r =>
                        r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter &&
                        r.SphereId == field.SphereId && r.FieldId == field.Id).ToList();
                    if (rank.Count > 0)
                    {
                        var subField = _xSubField.Find(s => s.FieldId == field.Id).ToList();
                        if (subField.Any())
                        {
                            foreach (var sField in subField)
                            {
                                var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                                if (subFieldRankWithElements.Any())
                                {
                                    var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                                    fieldRank += subfieldRankMedium;
                                }
                            
                                var subFieldRankWithoutElements = rank.FirstOrDefault(r => r.SubFieldId == sField.Id && r.ElementId == 0);
                                if (subFieldRankWithoutElements != null)
                                {
                                    fieldRank += Math.Round(subFieldRankWithoutElements.Rank, 2);
                                }
                            }
                            return await Task.FromResult<double>(fieldRank);
                        }
                        else
                        {
                            var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                            if (rankWithElements.Count > 0)
                            {
                                fieldRank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                            }
                        
                            var rankWithouthElements = rank.FirstOrDefault(r => r.SubFieldId == 0 && r.ElementId == 0);
                            if (rankWithouthElements != null)
                            {
                                fieldRank = Math.Round(rankWithouthElements.Rank, 2);
                            }
                            return await Task.FromResult<double>(fieldRank);
                        }
                    }
                    else
                    {
                        return await Task.FromResult<double>(0);
                    }
                }
                case Domain.Enums.OrgCategory.Adminstrations:
                {
                    var field = _aField.Find(f => f.Id == fieldId).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                    var rank = _aRankTable.Find(r =>
                        r.OrganizationId == organization.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter &&
                        r.SphereId == field.SphereId && r.FieldId == field.Id).ToList();
                    if (rank.Count > 0)
                    {
                        var subField = _aSubField.Find(s => s.FieldId == field.Id).ToList();
                        if (subField.Any())
                        {
                            foreach (var sField in subField)
                            {
                                var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                                if (subFieldRankWithElements.Any())
                                {
                                    var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                                    fieldRank += subfieldRankMedium;
                                }
                                
                                var subFieldRankWithoutElements = rank.FirstOrDefault(r => r.SubFieldId == sField.Id && r.ElementId == 0);
                                if (subFieldRankWithoutElements != null)
                                {
                                    fieldRank += Math.Round(subFieldRankWithoutElements.Rank, 2);
                                }
                            }
                            return await Task.FromResult<double>(fieldRank);
                        }
                        else
                        {
                            var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                            if (rankWithElements.Count > 0)
                            {
                                fieldRank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                            }
                            
                            var rankWithouthElements = rank.FirstOrDefault(r => r.SubFieldId == 0 && r.ElementId == 0);
                            if (rankWithouthElements != null)
                            {
                                fieldRank = Math.Round(rankWithouthElements.Rank, 2);
                            }
                            return await Task.FromResult<double>(fieldRank);
                        }
                    }
                    else
                    {
                        return await Task.FromResult<double>(0);
                    }
                }
                default:
                    return await Task.FromResult<double>(0);
            }
        }

        private async Task<double> GetFieldRankGovernment(List<GRankTable> rankTable, Organizations organization, GField field)
        {
            double fieldRank = 0;

            var rank = rankTable.Where(r =>
                r.OrganizationId == organization.Id &&
                r.SphereId == field.SphereId && r.FieldId == field.Id).ToList();

            if (rank.Count > 0)
            {
                //var subField = _gSubField.Find(s => s.FieldId == field.Id).ToList();
                if (field.GSubFields.Any())
                {
                    foreach (var sField in field.GSubFields)
                    {
                        var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                        if (subFieldRankWithElements.Any())
                        {
                            var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                            fieldRank += subfieldRankMedium;
                        }

                        var subFieldRankWithoutElements = rank.FirstOrDefault(r => r.SubFieldId == sField.Id && r.ElementId == 0);
                        if (subFieldRankWithoutElements != null)
                        {
                            fieldRank += Math.Round(subFieldRankWithoutElements.Rank, 2);
                        }
                    }
                    return await Task.FromResult<double>(fieldRank);
                }
                else
                {
                    var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                    if (rankWithElements.Count > 0)
                    {
                        fieldRank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                    }

                    var rankWithouthElements = rank.FirstOrDefault(r => r.SubFieldId == 0 && r.ElementId == 0);
                    if (rankWithouthElements != null)
                    {
                        fieldRank = Math.Round(rankWithouthElements.Rank, 2);
                    }
                    return await Task.FromResult<double>(fieldRank);
                }
            }
            else
            {
                return await Task.FromResult<double>(0);
            }
        }
        private async Task<double> GetFieldRankFarm(List<XRankTable> rankTable, Organizations organization, XField field)
        {
            double fieldRank = 0;

            var rank = rankTable.Where(r =>
                r.OrganizationId == organization.Id &&
                r.SphereId == field.SphereId && r.FieldId == field.Id).ToList();

            if (rank.Count > 0)
            {
                //var subField = _gSubField.Find(s => s.FieldId == field.Id).ToList();
                if (field.XSubFields.Any())
                {
                    foreach (var sField in field.XSubFields)
                    {
                        var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                        if (subFieldRankWithElements.Any())
                        {
                            var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                            fieldRank += subfieldRankMedium;
                        }

                        var subFieldRankWithoutElements = rank.FirstOrDefault(r => r.SubFieldId == sField.Id && r.ElementId == 0);
                        if (subFieldRankWithoutElements != null)
                        {
                            fieldRank += Math.Round(subFieldRankWithoutElements.Rank, 2);
                        }
                    }
                    return await Task.FromResult<double>(fieldRank);
                }
                else
                {
                    var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                    if (rankWithElements.Count > 0)
                    {
                        fieldRank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                    }

                    var rankWithouthElements = rank.FirstOrDefault(r => r.SubFieldId == 0 && r.ElementId == 0);
                    if (rankWithouthElements != null)
                    {
                        fieldRank = Math.Round(rankWithouthElements.Rank, 2);
                    }
                    return await Task.FromResult<double>(fieldRank);
                }
            }
            else
            {
                return await Task.FromResult<double>(0);
            }
        }
        private async Task<double> GetFieldRankAdministration(List<ARankTable> rankTable, Organizations organization, AField field)
        {
            double fieldRank = 0;

            var rank = rankTable.Where(r =>
                r.OrganizationId == organization.Id &&
                r.SphereId == field.SphereId && r.FieldId == field.Id).ToList();

            if (rank.Count > 0)
            {
                //var subField = _gSubField.Find(s => s.FieldId == field.Id).ToList();
                if (field.ASubFields.Any())
                {
                    foreach (var sField in field.ASubFields)
                    {
                        var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                        if (subFieldRankWithElements.Any())
                        {
                            var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                            fieldRank += subfieldRankMedium;
                        }

                        var subFieldRankWithoutElements = rank.FirstOrDefault(r => r.SubFieldId == sField.Id && r.ElementId == 0);
                        if (subFieldRankWithoutElements != null)
                        {
                            fieldRank += Math.Round(subFieldRankWithoutElements.Rank, 2);
                        }
                    }
                    return await Task.FromResult<double>(fieldRank);
                }
                else
                {
                    var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                    if (rankWithElements.Count > 0)
                    {
                        fieldRank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                    }

                    var rankWithouthElements = rank.FirstOrDefault(r => r.SubFieldId == 0 && r.ElementId == 0);
                    if (rankWithouthElements != null)
                    {
                        fieldRank = Math.Round(rankWithouthElements.Rank, 2);
                    }
                    return await Task.FromResult<double>(fieldRank);
                }
            }
            else
            {
                return await Task.FromResult<double>(0);
            }
        }
        #endregion
    }
}
