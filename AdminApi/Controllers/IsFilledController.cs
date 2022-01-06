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
    public class IsFilled : Controller
    {
        IMediator _mediator;
        public IsFilled(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<IsFilledQueryResult>> Get([FromQuery] int orgId, int year, Quarters quarter, int sphereId, int fieldId)
        {
            try
            {
                IsFilledQuery model = new IsFilledQuery()
                {
                    OrganizationId = orgId,
                    Year = year,
                    Quarter = quarter,
                    SphereId = sphereId,
                    FieldId = fieldId
                };

                var result = await _mediator.Send<IsFilledQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<IsFilledCommandResult>> Add([FromBody] IsFilledCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<IsFilledCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<IsFilledCommandResult>> Put([FromBody] IsFilledCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<IsFilledCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<IsFilledCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                IsFilledCommand model = new IsFilledCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
