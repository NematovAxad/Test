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
    public class DigitalEconomyProjectsReport : Controller
    {
        IMediator _mediator;
        public DigitalEconomyProjectsReport(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsReportQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                OrganizationDigitalEconomyProjectsReportQuery model = new OrganizationDigitalEconomyProjectsReportQuery()
                {
                    OrganizationId = organizationId,
                };

                var result = await _mediator.Send<OrganizationDigitalEconomyProjectsReportQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsReportCommandResult>> Add([FromBody] OrganizationDigitalEconomyProjectsReportCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrganizationDigitalEconomyProjectsReportCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsReportCommandResult>> Put([FromBody] OrganizationDigitalEconomyProjectsReportCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrganizationDigitalEconomyProjectsReportCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsReportCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrganizationDigitalEconomyProjectsReportCommand model = new OrganizationDigitalEconomyProjectsReportCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
