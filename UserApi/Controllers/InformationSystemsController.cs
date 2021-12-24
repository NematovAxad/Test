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
    public class InformationSystems : Controller
    {
        IMediator _mediator;
        public InformationSystems(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<OrgInformationSystemsQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                OrgInformationSystemsQuery model = new OrgInformationSystemsQuery()
                {
                    OrganizationId = organizationId
                };

                var result = await _mediator.Send<OrgInformationSystemsQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrgInformationSystemsCommandResult>> Add([FromQuery] OrgInformationSystemsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgInformationSystemsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgInformationSystemsCommandResult>> Put([FromQuery] OrgInformationSystemsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgInformationSystemsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgInformationSystemsCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgInformationSystemsCommand model = new OrgInformationSystemsCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
