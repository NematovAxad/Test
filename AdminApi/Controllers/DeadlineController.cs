using AdminHandler.Commands.Ranking;
using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using ApiConfigs;
using CoreResult.ResponseCores;
using Domain.Enums;
using MainInfrastructures.Interfaces;
using MainInfrastructures.Services;
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
    public class Deadline : Controller
    {
        IMediator _mediator;
        private readonly IOrganizationService _orgService;
        public Deadline(IMediator mediator, IOrganizationService orgService)
        {
            _mediator = mediator;
            _orgService = orgService;
        }
        [HttpGet]
        public async Task<ResponseCore<DeadlineQueryResult>> Get([FromQuery] int year, Quarters quarter, bool isActive)
        {
            try
            {
                DeadlineQuery model = new DeadlineQuery()
                {
                    Year = year,
                    Quarter = quarter,
                    IsActive = isActive
                };

                var result = await _mediator.Send<DeadlineQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpGet]
        public async Task<ResponseCore<RankingStruct>> GetStruct([FromQuery] int orgid)
        {
            try
            {
               

                var result = await _orgService.GetStruct(orgid);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<DeadlineCommandResult>> Add([FromQuery] DeadlineCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();

                var result = await _mediator.Send<DeadlineCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<DeadlineCommandResult>> Put([FromQuery] DeadlineCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<DeadlineCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<DeadlineCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                DeadlineCommand model = new DeadlineCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
