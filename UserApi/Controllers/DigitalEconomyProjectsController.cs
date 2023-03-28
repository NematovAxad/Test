using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.SecondSectionCommand;
using UserHandler.Queries.SecondSectionQuery;
using UserHandler.Results.SecondSectionCommandResult;
using UserHandler.Results.SecondSectionQueryResult;
using UserHandler.Results.SixthSectionResults;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Commands.SixthSectionCommands;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class DigitalEconomyProjects : Controller
    {
        IMediator _mediator;
        public DigitalEconomyProjects(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                OrganizationDigitalEconomyProjectsQuery model = new OrganizationDigitalEconomyProjectsQuery()
                {
                    OrganizationId = organizationId,
                };

                var result = await _mediator.Send<OrganizationDigitalEconomyProjectsQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsCommandResult>> Add([FromBody] OrganizationDigitalEconomyProjectsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrganizationDigitalEconomyProjectsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsCommandResult>> Put([FromBody] OrganizationDigitalEconomyProjectsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrganizationDigitalEconomyProjectsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrganizationDigitalEconomyProjectsCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrganizationDigitalEconomyProjectsCommand model = new OrganizationDigitalEconomyProjectsCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
