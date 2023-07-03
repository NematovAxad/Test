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
using System.IO;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Domain.Models.ThirdSection;
using Domain.Models.Organization;
using Domain.MyGovModels;
using MainInfrastructures.Migrations;
using Domain.Models.MibModels;
using Domain.Permission;

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
                                    IDataContext db)
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
    }
}
