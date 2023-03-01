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
using UserHandler.Results.SeventhSection;
using UserHandler.Queries.SeventhSection;
using UserHandler.Commands.SeventhSection;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class OrganizationFinance : Controller
    {
        IMediator _mediator;
        public OrganizationFinance(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<OrgFinanceQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                OrgFinanceQuery model = new OrgFinanceQuery()
                {
                    OrganizationId = organizationId
                };

                var result = await _mediator.Send<OrgFinanceQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrgFinanceCommandResult>> Add([FromBody] OrgFinanceCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgFinanceCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgFinanceCommandResult>> Put([FromBody] OrgFinanceCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgFinanceCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgFinanceCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgFinanceCommand model = new OrgFinanceCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
