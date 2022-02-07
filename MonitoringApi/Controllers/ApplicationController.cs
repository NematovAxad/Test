using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringHandler.Commands.StructureCommands;
using MonitoringHandler.Querys.StructureQuerys;
using MonitoringHandler.Results.StructureResults.CommandResults;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringApi.Controllers
{
    [Route("apiMonitoring/[controller]/[action]")]
    public class Application : Controller
    {
        IMediator _mediator;
        public Application(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<ApplicationQueryResult>> Get([FromQuery] int id)
        {
            try
            {
                ApplicationQuery model = new ApplicationQuery()
                {
                    Id = id
                };

                var result = await _mediator.Send<ApplicationQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<ApplicationCommandResult>> Add([FromQuery] ApplicationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ApplicationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ApplicationCommandResult>> Put([FromQuery] ApplicationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ApplicationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<ApplicationCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ApplicationCommand model = new ApplicationCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
