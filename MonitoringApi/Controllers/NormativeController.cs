using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringHandler.Commands.StructureCommands;
using MonitoringHandler.Results.StructureResults.CommandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringApi.Controllers
{
    [Route("apiMonitoring/[controller]/[action]")]
    public class Normative : Controller
    {
        IMediator _mediator;
        public Normative(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ResponseCore<NormativeCommandResult>> Add([FromQuery] NormativeCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<NormativeCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
