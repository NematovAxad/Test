using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHandler.Commands.SecondSectionCommand;
using UserHandler.Queries.SecondSectionQuery;
using UserHandler.Results.SecondSectionCommandResult;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class HelplineInfo : Controller
    {
        IMediator _mediator;
        public HelplineInfo(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<HelplineInfoQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                HelplineInfoQuery model = new HelplineInfoQuery()
                {
                    OrganizationId = organizationId,
                };

                var result = await _mediator.Send<HelplineInfoQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<HelplineInfoCommandResult>> Add([FromBody] HelplineInfoCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<HelplineInfoCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<HelplineInfoCommandResult>> Put([FromBody] HelplineInfoCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<HelplineInfoCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<HelplineInfoCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                HelplineInfoCommand model = new HelplineInfoCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
