using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Results.ReestrProjectClassificationCommandResult;
using UserHandler.Results.ReestrProjectAuthorizationResult;
using UserHandler.Commands.ReestrProjectAuthorizationCommand;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectAuthorizationItems : Controller
    {
        IMediator _mediator;
        public ReestrProjectAuthorizationItems(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseCore<AuthorizationCommandResult>> Add([FromBody] AuthorizationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<AuthorizationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPut]
        public async Task<ResponseCore<AuthorizationCommandResult>> Put([FromBody] AuthorizationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<AuthorizationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<AuthorizationCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                AuthorizationCommand model = new AuthorizationCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
