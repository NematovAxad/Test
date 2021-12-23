using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class OrgSocialSites : Controller
    {
        IMediator _mediator;
        public OrgSocialSites(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<OrgSocialSitesQueryResult>> Get([FromQuery] int id, int orgId, int deadlineId)
        {
            try
            {
                OrgSocialSitesQuery model = new OrgSocialSitesQuery()
                {
                    Id = id,
                   OrganizationId = orgId,
                   DeadlineId = deadlineId
                };

                var result = await _mediator.Send<OrgSocialSitesQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrgSocialSitesCommandResult>> Add([FromBody] OrgSocialSitesCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgSocialSitesCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgSocialSitesCommandResult>> Put([FromBody] OrgSocialSitesCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgSocialSitesCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgSocialSitesCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgSocialSitesCommand model = new OrgSocialSitesCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
