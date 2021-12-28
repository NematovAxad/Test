using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHandler.Commands.SeventhSection;
using UserHandler.Queries.SeventhSection;
using UserHandler.Results.SeventhSection;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class OrgComputers : Controller
    {
        IMediator _mediator;
        public OrgComputers(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<OrgComputersQueryResult>> Get([FromQuery] int id, int organizationId)
        {
            try
            {
                OrgComputersQuery model = new OrgComputersQuery()
                {
                    OrganizationId = organizationId,
                    Id = id
                };

                var result = await _mediator.Send<OrgComputersQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrgComputersCommandResult>> Add([FromBody] OrgComputersCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgComputersCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgComputersCommandResult>> Put([FromBody] OrgComputersCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgComputersCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgComputersCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgComputersCommand model = new OrgComputersCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
