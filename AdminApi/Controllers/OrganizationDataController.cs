using AdminHandler.Commands.Region;
using AdminHandler.Querys.Region;
using AdminHandler.Results.Region;
using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Results.SixthSectionResults;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Commands.SixthSectionCommands;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class OrganizationData : Controller
    {
        IMediator _mediator;
        public OrganizationData(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<OrgDataAvailabilityQueryResult>> Get([FromQuery] int orgId, string section)
        {
            try
            {
                OrgDataAvailabilityQuery model = new OrgDataAvailabilityQuery()
                {
                    OrgId = orgId,
                    Section = section
                };

                var result = await _mediator.Send<OrgDataAvailabilityQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrgDataAvailabilityCommandResult>> Add([FromBody] OrgDataAvailabilityCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                model.UserPinfl = this.UserPinfl();
                var result = await _mediator.Send<OrgDataAvailabilityCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgDataAvailabilityCommandResult>> Put([FromBody] OrgDataAvailabilityCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                model.UserPinfl = this.UserPinfl();
                var result = await _mediator.Send<OrgDataAvailabilityCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgDataAvailabilityCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgDataAvailabilityCommand model = new OrgDataAvailabilityCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                model.UserPinfl = this.UserPinfl();
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
