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
    public class Performancer : Controller
    {
        IMediator _mediator;
        public Performancer(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<PerformencerQueryResult>> Get([FromQuery] int id)
        {
            try
            {
                PerformencerQuery model = new PerformencerQuery()
                {
                    Id = id
                };

                var result = await _mediator.Send<PerformencerQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<PerformencerCommandResult>> Add([FromQuery] PerformencerCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<PerformencerCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<PerformencerCommandResult>> Put([FromQuery] PerformencerCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<PerformencerCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<PerformencerCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                PerformencerCommand model = new PerformencerCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
