using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.EigthSectionCommand;
using UserHandler.Queries.EigthSectionQuery;
using UserHandler.Results.EigthSectionResult;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class OrganizationFinanceReport : Controller
    {
        IMediator _mediator;
        public OrganizationFinanceReport(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<OrgFinanceReportQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                OrgFinanceReportQuery model = new OrgFinanceReportQuery()
                {
                    OrganizationId = organizationId
                };

                var result = await _mediator.Send<OrgFinanceReportQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrgFinanceReportCommandResult>> Add([FromBody] OrgFinanceReportCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgFinanceReportCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgFinanceReportCommandResult>> Put([FromBody] OrgFinanceReportCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrgFinanceReportCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgFinanceReportCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgFinanceReportCommand model = new OrgFinanceReportCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
