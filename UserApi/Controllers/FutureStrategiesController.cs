using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHandler.Commands.ThirdSection;
using UserHandler.Queries.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class FutureStrategies : Controller
    {
        IMediator _mediator;
        public FutureStrategies(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<FutureYearsStrategiesQueryResult>> Get([FromQuery] int id, int organizationId)
        {
            try
            {
                FutureYearsStrategiesQuery model = new FutureYearsStrategiesQuery()
                {
                    OrganizationId = organizationId,
                    Id = id
                };

                var result = await _mediator.Send<FutureYearsStrategiesQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<FutureYearsStrategiesCommandResult>> Add([FromQuery] FutureYearsStrategiesCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<FutureYearsStrategiesCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<FutureYearsStrategiesCommandResult>> Put([FromQuery] FutureYearsStrategiesCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<FutureYearsStrategiesCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<FutureYearsStrategiesCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                FutureYearsStrategiesCommand model = new FutureYearsStrategiesCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
