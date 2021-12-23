using AdminHandler.Commands.Ranking;
using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using ApiConfigs;
using CoreResult.ResponseCores;
using Domain.Enums;
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
    public class Ranking : Controller
    {
        IMediator _mediator;
        public Ranking(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<RankingQueryResult>> Get([FromQuery] int orgId, int year, Quarters quarter, int sphereId, int fieldId)
        {
            try
            {
                RankingQuery model = new RankingQuery()
                {
                    OrganizationId = orgId,
                    Year = year,
                    Quarter = quarter,
                    SphereId = sphereId,
                    FieldId = fieldId
                };

                var result = await _mediator.Send<RankingQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<RankingCommandResult>> Add([FromQuery] RankingCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<RankingCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<RankingCommandResult>> Put([FromQuery] RankingCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<RankingCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<RankingCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                RankingCommand model = new RankingCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
