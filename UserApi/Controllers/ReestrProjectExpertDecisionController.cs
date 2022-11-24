using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Queries.ReestrPassportQuery;
using UserHandler.Results.ReestrPassportResult;
using ApiConfigs;
using UserHandler.Commands.ReestrPassportCommands;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectExpertDecision : Controller
    {
        IMediator _mediator;
        public ReestrProjectExpertDecision(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<ProjectExpertDecisionQueryResult>> Get([FromQuery] int organizationId, int reestrProjectId)
        {
            try
            {
                ProjectExpertDecisionQuery model = new ProjectExpertDecisionQuery()
                {
                    OrgId = organizationId,
                    ReestrProjectId = reestrProjectId
                };

                var result = await _mediator.Send<ProjectExpertDecisionQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<ProjectExpertDecisionCommandResult>> Add([FromBody] ProjectExpertDecisionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectExpertDecisionCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPut]
        public async Task<ResponseCore<ProjectExpertDecisionCommandResult>> Put([FromBody] ProjectExpertDecisionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectExpertDecisionCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<ProjectExpertDecisionCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ProjectExpertDecisionCommand model = new ProjectExpertDecisionCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
