using Autofac.Core;
using CoreResult.ResponseCores;
using Domain.CyberSecurityModels;
using Domain.MibModels;
using Domain.Models.Organization;
using Domain.MyGovModels;
using Domain.OpenDataModels;
using Domain.ReesterModels;
using MainInfrastructures.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MainInfrastructures.Services.MyGovService;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class Integration : Controller
    {
        IMediator _mediator;
        ICyberSecurityService _cyberSecurityService;
        IOrganizationService _organizationService;
        private IOpenDataService _openDataService;
        private IReesterService _reesterService;
        IMyGovService _myGovServices;
        IMibService _mibService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="cyberSecurityService"></param>
        /// <param name="organizationService"></param>
        /// <param name="myGovServices"></param>
        /// <param name="mibService"></param>
        /// <param name="openDataService"></param>
        /// <param name="reesterService"></param>
        public Integration(IMediator mediator, ICyberSecurityService cyberSecurityService, 
            IOrganizationService organizationService, 
            IMyGovService myGovServices, 
            IMibService mibService, 
            IOpenDataService openDataService, 
            IReesterService reesterService)
        {
            _mediator = mediator;
            _cyberSecurityService = cyberSecurityService;
            _organizationService = organizationService;
            _myGovServices = myGovServices;
            _mibService = mibService;
            _openDataService = openDataService;
            _reesterService = reesterService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateOpenData()
        {
            return await _openDataService.UpdateOpenDataTable();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateReestrTables()
        {
            return await _reesterService.UpdateReestrTable();
        }
        
        [HttpGet]
        public async Task<ResponseCore<OpenDataQueryResult>> OpenData([FromQuery] int orgId)
        {
            try
            {
                OpenDataQuery model = new OpenDataQuery()
                {
                   OrgId = orgId
                };

                var result = await _mediator.Send<OpenDataQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        /*[HttpGet]
        public async Task<ResponseCore<FirstRequestQueryResult>> ReesterProjects([FromQuery] int orgId, int page, int limit)
        {
            try
            {
                FirstRequestQuery model = new FirstRequestQuery()
                {
                    OrgId = orgId,
                    Page = page,
                    Limit = limit
                };

                var result = await _mediator.Send<FirstRequestQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }*/
        [HttpGet]
        public async Task<ResponseCore<FirstRequestQueryResult>> ReesterProjects([FromQuery] int orgId, int page, int limit)
        {
            try
            {
                FirstRequestQuery model = new FirstRequestQuery()
                {
                    OrgId = orgId,
                    Page = page,
                    Limit = limit
                };

                return await _reesterService.FirstRequestTest(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        /*[HttpGet]
        public async Task<ResponseCore<SecondRequestQueryResult>> ReesterProjectDetails([FromQuery] int id)
        {
            try
            {
                SecondRequestQuery model = new SecondRequestQuery()
                {
                    Id = id
                };

                var result = await _mediator.Send<SecondRequestQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }*/
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseCore<SecondRequestQueryResult>> ReesterProjectDetails([FromQuery] int id)
        {
            try
            {
                SecondRequestQuery model = new SecondRequestQuery()
                {
                    Id = id
                };

                return await _reesterService.SecondRequestTest(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        
        [HttpGet]
        public async Task<bool> GetCyberSecurityRank([FromQuery] int deadlineId)
        {
                GetOrgRanksQuery model = new GetOrgRanksQuery()
                {
                    DeadlineId = deadlineId
                };

                var result = await _cyberSecurityService.GetOrgRank(model);
            return (bool)result;
        }

        [HttpGet]
        public async Task<ResponseCore<OrgServiceReportResult>> MyGovServices([FromQuery] int orgId)
        {
            try
            {
                

                var result = await _myGovServices.MygovServiceReport(orgId);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpGet]
        public async Task<ResponseCore<OrgServiceReportDetailResult>> MyGovServiceDetails([FromQuery] int serviceId, int orgId)
        {
            try
            {


                var result = await _myGovServices.MygovReportsDetails(serviceId, orgId);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<bool> UpdateMyGovReport([FromQuery] int deadlineId)
        {

            var result = await _myGovServices.UpdateMyGovReport(deadlineId);
            return result;
           
        }
        [HttpPost]
        public async Task<bool> UpdateMyGovReportDetail([FromQuery] int deadlineId)
        {

            var result = await _myGovServices.UpdateMyGovReportDetails(deadlineId);
            return result;

        }

        [HttpPost]
        public async Task<bool> UpdateOrgsName()
        {
            var result = await _organizationService.UpdateOrgsName();
            return (bool)result;
        }

        [HttpPost]
        public async Task<bool> UploadOrgServices(IFormFile file)
        {
            var result = await _organizationService.UploadOrgServices(file);
            return (bool)result;
        }
        [HttpPost]
        public async Task<bool> MibReportUpdate([FromQuery] DateTime startTime, DateTime endTime)
        {

            var result = await _mibService.MibReport(startTime, endTime);
            return result;

        }
        [HttpGet]
        public async Task<ResponseCore<MibReportResult>> OrgMibReport([FromQuery] int orgId)
        {
            try
            {
                var result = await _mibService.OrgMibReport(orgId);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
