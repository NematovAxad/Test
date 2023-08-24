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
using Domain.Models.SixthSection;
using Domain.OpenDataModels;

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
        private int rankExcelStartIndex;
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

        public async Task<MemoryStream> DownloadOrganizationsRateReport(OrgCategory category)
        {
            rankExcelStartIndex = 1;
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

           
            var xRankTable = _gRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();
            var aRankTable = _gRankTable.Find(r => r.Year == deadline.Year && r.Quarter == deadline.Quarter).ToList();

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

            FirstRequestQueryResult reestrResult = new FirstRequestQueryResult();

            reestrResult = await _reesterService.FirstRequestNew(model);

            worksheet.Cells[27, 1].Value = "Axborot tizimlari soni";
            worksheet.Cells[27, 1].Style.Font.Bold = true;
            worksheet.Cells[27, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            worksheet.Cells[27, 2].Value = reestrResult.Items.Count.ToString(CultureInfo.InvariantCulture) + " ta";

            worksheet.Cells[28, 1].Value = "Ekspert xulosalari olinganligi";
            worksheet.Cells[28, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

            worksheet.Cells[28, 2].Value = reestrResult.Items.Where(i => i.HasExpertise == true).ToList().Count.ToString(CultureInfo.InvariantCulture) + " ta";

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
    }
}
