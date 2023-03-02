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

namespace MainInfrastructures.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<GSphere, int> _gSphere;
        private readonly IRepository<GField, int> _gField;
        private readonly IRepository<GSubField, int> _gSubField;
        private readonly IRepository<XSphere, int> _xSphere;
        private readonly IRepository<XField, int> _xField;
        private readonly IRepository<XSubField, int> _xSubField;
        private readonly IRepository<ASphere, int> _aSphere;
        private readonly IRepository<AField, int> _aField;
        private readonly IRepository<ASubField, int> _aSubField;
        private readonly IDataContext _db;

        public OrganizationService(IRepository<Organizations, int> organization, 
                                    IRepository<GSphere, int> gSphere, 
                                    IRepository<GField, int> gField, 
                                    IRepository<GSubField, int> gSubField, 
                                    IRepository<XSphere, int> xSphere, 
                                    IRepository<XField, int> xField, 
                                    IRepository<XSubField, int> xSubField, 
                                    IRepository<ASphere, int> aSphere, 
                                    IRepository<AField, int> aField, 
                                    IRepository<ASubField, int> aSubField, 
                                    IDataContext db)
        {
            _organization = organization;
            _gSphere = gSphere;
            _gField = gField;
            _gSubField = gSubField;
            _xSphere = xSphere;
            _xField = xField;
            _xSubField = xSubField;
            _aSphere = aSphere;
            _aField = aField;
            _aSubField = aSubField;
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
    }
}
