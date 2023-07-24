using Domain.Models.Ranking.Administrations;
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
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Domain.Models.ThirdSection;
using Domain.Models.Organization;
using Domain.MyGovModels;
using MainInfrastructures.Migrations;
using Domain.Models.MibModels;
using Domain.Permission;
using System.Xml.Linq;
using Domain.Models.SixthSection;

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
        private readonly IDataContext _db;
        private readonly IReesterService _reesterService;
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
                                    IReesterService reesterService)
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
                long neighborhoodid = 0;
                var rowCount = workSheet.Dimension.End.Row;
                for (int row = 5; row <= rowCount; row++)
                {
                    try
                    {

                        if (workSheet.Cells[row, 2].Value?.ToString().Length > 1)
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

                rate = Math.Round((gotRate / maxRate)*100, 2);

                if (rate == 0)
                    rate = 1;

                #endregion
            }

            OrgExceptionPercentResultModel result = new OrgExceptionPercentResultModel();

            result.Percentage = rate;
            result.OrganizationId = org.Id;

            return result;

        }

        public ReplacerOrgHead GetReplacerOrgHead(int orgId)
        {
            ReplacerOrgHead result = new ReplacerOrgHead();

            var replaceOrgHead = _db.Context.Set<ReplacerOrgHead>().Where(r => r.OrganizationId == orgId).FirstOrDefault();

            if (replaceOrgHead != null)
                result = replaceOrgHead;

            return result;
        }

        public async Task<MemoryStream> DownloadOrgData(int orgId)
        {
            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();

            if(org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);
             

            var replaceOrgHead = GetReplacerOrgHead(org.Id);

            string fileName = org.ShortName + " _details";
            var memoryStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet;
                worksheet = package.Workbook.Worksheets.Add(fileName);

                worksheet.Name = fileName;
                worksheet.Columns[1].Width = 30;
                worksheet.Columns[2].Width = 50;
                worksheet.DefaultRowHeight = 20;
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
                
                await Task.WhenAll(task1, task2, task3);

                package.Save();
            }

            memoryStream.Flush();
            memoryStream.Position = 0;


            return memoryStream;
        }

        private async Task SetICTDepartmentDetaills(ExcelWorksheet worksheet, int orgId)
        {
            OrganizationIctSpecialForces orgSpecialForces = new OrganizationIctSpecialForces();

            var result = _db.Context.Set<OrganizationIctSpecialForces>().Where(r => r.OrganizationId == orgId).FirstOrDefault();

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
            worksheet.Cells[23, 2].Value = orgSpecialForces.EmployeesSum.ToString(CultureInfo.InvariantCulture);

            worksheet.Cells[24, 1].Value = "Markaziy boshqaruv apparatida";
            worksheet.Cells[24, 2].Value = orgSpecialForces.CentralofficeEmployees.ToString(CultureInfo.InvariantCulture);

            worksheet.Cells[25, 1].Value = "Hududiy boshqarmalarda";
            worksheet.Cells[25, 2].Value = orgSpecialForces.RegionalEmployees.ToString(CultureInfo.InvariantCulture);

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

            FirstRequestQueryResult reestrResult = new FirstRequestQueryResult();

            reestrResult = await _reesterService.FirstRequest(model);

            worksheet.Cells[27, 1].Value = "Axborot tizimlari soni";
            worksheet.Cells[27, 1].Style.Font.Bold = true;
            worksheet.Cells[27, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            worksheet.Cells[27, 2].Value = reestrResult.Items.Count + " ta";

            worksheet.Cells[28, 1].Value = "Ekspert xulosalari olinganligi";
            worksheet.Cells[28, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

            worksheet.Cells[28, 2].Value = reestrResult.Items.Where(i => i.HasExpertise == true).ToList().Count + " ta";

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

            worksheet.Cells[29, 2].Value = orgProjects.ProjectsCount.ToString(CultureInfo.InvariantCulture);

            worksheet.Cells[30, 1].Value = "Bajarilgan";
            worksheet.Cells[30, 2].Value = orgProjects.CompletedProjects.ToString(CultureInfo.InvariantCulture);

            worksheet.Cells[31, 1].Value = "Bajarilmoqda";
            worksheet.Cells[31, 2].Value = orgProjects.OngoingProjects.ToString(CultureInfo.InvariantCulture);
            
            worksheet.Cells[32, 1].Value = "Bajarilmagan";
            worksheet.Cells[32, 2].Value = orgProjects.NotFinishedProjects.ToString(CultureInfo.InvariantCulture);
            
            using (var range = worksheet.Cells[30, 1, 32, 1])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
            }
            using (var range = worksheet.Cells[29, 2, 32, 2])
            {
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }
        }
    }
}
