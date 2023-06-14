using AdminHandler.Commands.Ranking;
using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using ApiConfigs;
using CoreResult.ResponseCores;
using Domain.Enums;
using Domain.Models.Ranking;
using MainInfrastructures.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class Rank : Controller
    {
        IOrganizationService _organizationService;
        IMediator _mediator;
        public Rank(IMediator mediator, IOrganizationService organizationService)
        {
            _mediator = mediator;
            _organizationService = organizationService;
        }
        [HttpGet]
        public async Task<ResponseCore<RankQueryResult>> Get([FromQuery] int orgId, int year, Quarters quarter, int sphereId, int fieldId, int subFieldId, int elementId)
        {
            try
            {
                RankQuery model = new RankQuery()
                {
                    OrganizationId = orgId,
                    Year = year,
                    Quarter = quarter,
                    SphereId = sphereId,
                    FieldId = fieldId,
                    SubFieldId = subFieldId,
                    ElementId = elementId
                };

                var result = await _mediator.Send<RankQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpGet]
        public async Task<ResponseCore<OrgExceptionPercentResultModel>> GetOrganizationExceptionPercent([FromQuery] int organizationId)
        {
            try
            {
                var result = await _organizationService.GetOrganizationExceptionPercent(organizationId);

                return result;
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<RankCommandResult>> Add([FromQuery] RankCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<RankCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        
        [HttpPut]
        public async Task<ResponseCore<RankCommandResult>> Put([FromQuery] RankCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<RankCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<RankCommandResult>> Delete([FromQuery] int id, int organizationId)
        {
            try
            {
                RankCommand model = new RankCommand() { EventType = Domain.Enums.EventType.Delete, Id = id, OrganizationId = organizationId };
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

        [HttpPost]
        public async Task<ResponseCore<FieldExceptionCommandResult>> SetFielsException([FromQuery] FieldExceptionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<FieldExceptionCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<FieldExceptionCommandResult>> RemoveFielsException([FromQuery] FieldExceptionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Delete;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<FieldExceptionCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
