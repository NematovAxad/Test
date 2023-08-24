using ApiConfigs;
using CoreResult.ResponseCores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.ReestrProjectIdentityCommand;
using UserHandler.Results.ReestrProjectIdentityResult;
using MediatR;
using UserHandler.Results.ReestrProjectAutomatedServicesResult;
using UserHandler.Commands.ReestrProjectAutomatedServicesCommand;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectAutomatedFunctions : Controller
    {
        IMediator _mediator;

        public ReestrProjectAutomatedFunctions(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpPost]
        public async Task<ResponseCore<AutomaticFunctionsCommandResult>> Add([FromBody] AutomaticFunctionsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<AutomaticFunctionsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPut]
        public async Task<ResponseCore<AutomaticFunctionsCommandResult>> Put([FromBody] AutomaticFunctionsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<AutomaticFunctionsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<AutomaticFunctionsCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                AutomaticFunctionsCommand model = new AutomaticFunctionsCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
