using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Queries.ReestrProjectClassificationQuery;
using UserHandler.Results.ReestrProjectClassificationResult;
using UserHandler.Results.ReestrProjectAuthorizationResult;
using UserHandler.Queries.ReestrProjectAuthorizationQuery;
using ApiConfigs;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Commands.ReestrProjectAuthorizationCommand;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectAuthorization : Controller
    {
        IMediator _mediator;
        public ReestrProjectAuthorization(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<ReestrProjectAuthorizationQueryResult>> Get([FromQuery] int organizationId, int reestrProjectId)
        {
            try
            {
                ReestrProjectAuthorizationQuery model = new ReestrProjectAuthorizationQuery()
                {
                    OrgId = organizationId,
                    ReestrProjectId = reestrProjectId
                };

                var result = await _mediator.Send<ReestrProjectAuthorizationQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<ReestrProjectAuthorizationCommandResult>> Add([FromBody] ReestrProjectAuthorizationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectAuthorizationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ReestrProjectAuthorizationCommandResult>> Put([FromBody] ReestrProjectAuthorizationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectAuthorizationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<ReestrProjectAuthorizationCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ReestrProjectAuthorizationCommand model = new ReestrProjectAuthorizationCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
