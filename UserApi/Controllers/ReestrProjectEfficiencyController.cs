using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Queries.ReestrProjectClassificationQuery;
using UserHandler.Results.ReestrProjectClassificationResult;
using UserHandler.Queries.ReestrProjectEfficiencyQuery;
using UserHandler.Results.ReestrProjectEfficiencyResult;
using UserHandler.Commands.ReestrProjectEfficiencyCommand;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectEfficiency : Controller
    {
        IMediator _mediator;
        public ReestrProjectEfficiency(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<ReestrProjectEfficiencyQueryResult>> Get([FromQuery] int organizationId, int reestrProjectId)
        {
            try
            {
                ReestrProjectEfficiencyQuery model = new ReestrProjectEfficiencyQuery()
                {
                    OrgId = organizationId,
                    ReestrProjectId = reestrProjectId
                };

                var result = await _mediator.Send<ReestrProjectEfficiencyQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<ReestrProjectEfficiencyCommandResult>> Add([FromBody] ReestrProjectEfficiencyCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectEfficiencyCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ReestrProjectEfficiencyCommandResult>> Put([FromBody] ReestrProjectEfficiencyCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectEfficiencyCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<ReestrProjectEfficiencyCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ReestrProjectEfficiencyCommand model = new ReestrProjectEfficiencyCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
