using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class OrganizationIndicatorRateController : ControllerBase
    {
        IMediator _mediator;
        public OrganizationIndicatorRateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<OrgIndicatorRateQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                OrgIndicatorRateQuery model = new OrgIndicatorRateQuery()
                {
                    OrganizationId = organizationId,
                };

                var result = await _mediator.Send<OrgIndicatorRateQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrgIndicatorRateCommandResult>> Add([FromBody] OrgIndicatorRateCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgIndicatorRateCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgIndicatorRateCommandResult>> Put([FromBody] OrgIndicatorRateCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgIndicatorRateCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgIndicatorRateCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgIndicatorRateCommand model = new OrgIndicatorRateCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
