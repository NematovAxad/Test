﻿using AdminHandler.Commands.Organization;
using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
using ApiConfigs;
using CoreResult.ResponseCores;
using Domain.Models;
using MainInfrastructures.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models.DashboardModels;
using MainInfrastructures.Services;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class Organization : Controller
    {
        IMediator _mediator;
        IOrganizationService _organizationService;
        private IDashboardService _dashboardService;
        
        public Organization(IMediator mediator, IOrganizationService organizationService, IDashboardService dashboardService)
        {
            _mediator = mediator;
            _organizationService = organizationService;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadOrgData([FromQuery] int orgId)
        {
            try
            {
                var stream = await _organizationService.DownloadOrgData(orgId);

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orgData");
            }
            catch(Exception ex)
            {
                return NoContent();
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> DownloadRankReport([FromQuery] OrgCategory category)
        {
            try
            {
                var stream = await _organizationService.DownloadOrganizationsRateReport(category);

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orgData");
            }
            catch(Exception ex)
            {
                return NoContent();
            }
        }
        
        [HttpGet]
        public async Task<ResponseCore<DashboardResultModel>> DashboardReport()
        {
            try
            {
                return await _dashboardService.GetDashboardData();
            }
            catch(Exception ex)
            {
                return ex;
            }
        }


        [HttpGet]
        public async Task<ResponseCore<OrgQueryResult>> Get([FromQuery] int id)
        {
            try
            {
                OrgQuery model = new OrgQuery()
                {
                    OrgId = id
                };

                var result = await _mediator.Send<OrgQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpGet]
        public async Task<ResponseCore<BasedDocsQueryResult>> GetBasedDocs([FromQuery] int id, int organizationId)
        {
            try
            {
                BasedDocsQuery model = new BasedDocsQuery()
                {
                    Id = id,
                    OrganizationId = organizationId
                };

                var result = await _mediator.Send<BasedDocsQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<BasedDocsCommandResult>> AddBasedDocs([FromQuery] BasedDocsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<OrgDocsControlCommandResult>> OrgDocsControl([FromBody] OrgDocsControlCommand model)
        {
            try
            {
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<OrgCommandResult>> AddOrg([FromBody] OrgCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgCommandResult>> PutOrg([FromBody] OrgCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<BasedDocsCommandResult>> PutBasedDocs([FromQuery] BasedDocsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<OrgCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgCommand model = new OrgCommand() { EventType = Domain.Enums.EventType.Delete,  Id = id };
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<BasedDocsCommandResult>> DeleteBasedDocs([FromQuery] int id)
        {
            try
            {
                BasedDocsCommand model = new BasedDocsCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
