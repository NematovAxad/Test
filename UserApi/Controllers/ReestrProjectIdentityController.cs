using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Commands.ReestrProjectIdentityCommand;
using UserHandler.Queries.ReestrPassportQuery;
using UserHandler.Queries.ReestrProjectIdentityQuery;
using UserHandler.Results.ReestrPassportResult;
using UserHandler.Results.ReestrProjectIdentityResult;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectIdentity : Controller
    {
        IMediator _mediator;
        public ReestrProjectIdentity(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<ReestrProjectIdentityQueryResult>> Get([FromQuery] int organizationId, int reestrProjectId)
        {
            try
            {
                ReestrProjectIdentityQuery model = new ReestrProjectIdentityQuery()
                {
                    OrgId = organizationId,
                    ReestrProjectId = reestrProjectId
                };

                var result = await _mediator.Send<ReestrProjectIdentityQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<ReestrProjectIdentityCommandResult>> Add([FromBody] ReestrProjectIdentityCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectIdentityCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ReestrProjectIdentityCommandResult>> Put([FromBody] ReestrProjectIdentityCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectIdentityCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<ReestrProjectIdentityCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ReestrProjectIdentityCommand model = new ReestrProjectIdentityCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
