using DocumentFormat.OpenXml.EMMA;
using Domain;
using Domain.Models.FirstSection;
using Domain.Models.ThirdSection;
using JohaRepository;
using MainInfrastructures.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using SB.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class OrganizationServicesQueryHandler : IRequestHandler<OrganizationServicesQuery, OrganizationServicesQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationServices, int> _organizationServices;
        private readonly IOrganizationService _services;

        public OrganizationServicesQueryHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationServices, int> organizationServices, IOrganizationService services)
        {
            _organizations = organizations;
            _organizationServices = organizationServices;
            _services = services;
        }

        public async Task<OrganizationServicesQueryResult> Handle(OrganizationServicesQuery request, CancellationToken cancellationToken)
        {

            OrganizationServicesQueryResult result = new OrganizationServicesQueryResult() { Services = new List<ServiceReport>()};

            var services = _organizationServices.Find(s => s.OrganizationId == request.OrganizationId).Include(mbox=>mbox.Rates).ToList();

            if(request.ServiceId != 0)
            {
                services = services.Where(s => s.Id == request.ServiceId).ToList();
            }

            if(services.Count > 0)
            {
                foreach(var service in services)
                {
                    ServiceReport newreport = new ServiceReport();

                    decimal applicationWithoutProblems = service.Rates.Where(r => r.HasApplicationProblem == false || r.ApplicationProblemConfirmde == false).Count();
                    decimal recommendation = service.Rates.Where(r => r.RecommendService == true).Count();
                    decimal serviceSatisfaction = service.Rates.Where(r => r.ServiceSatisfactive == true || r.ServiceDissatisfactionConfirmed == false).Count();
                    decimal protest = service.Rates.Where(r => r.ServiceCommentType == Domain.Enums.CommentType.Objection && r.ServiceCommentConfirmed == true).Count();


                    newreport.Service = service;
                    newreport.RatesCount = service.Rates.Count;
                    if(service.Rates.Count > 0)
                    {
                        newreport.ApplicationProblems = (applicationWithoutProblems / service.Rates.Count) * _services.SubFieldMaxRate(service.OrganizationId, "3.1", "3.1.1").Result;
                        newreport.Recommendation = (recommendation / service.Rates.Count) * _services.SubFieldMaxRate(service.OrganizationId, "3.1", "3.1.2").Result;
                        newreport.ServiceSatisfaction = (serviceSatisfaction / service.Rates.Count) * _services.SubFieldMaxRate(service.OrganizationId, "3.1", "3.1.3").Result;
                        newreport.Protest = protest>0 ? 2 : 0;
                        newreport.ServiceRate = (service.Rates.Sum(s=>(int)s.ServiceRate)/25)* _services.SubFieldMaxRate(service.OrganizationId, "3.1", "3.1.4").Result;
                    }

                    result.Services.Add(newreport);
                }
            }

            return result;
        }
    }
}
