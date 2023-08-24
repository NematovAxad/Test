using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Queries.ReestrPassportQuery;
using UserHandler.Results.ReestrPassportResult;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectCyberSecurityExpertDecision : Controller
    {
        IMediator _mediator;
        public ReestrProjectCyberSecurityExpertDecision(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<ProjectCyberSecurityExpertDecisionQueryResult>> Get([FromQuery] int organizationId, int reestrProjectId)
        {
            try
            {
                ProjectCyberSecurityExpertDecisionQuery model = new ProjectCyberSecurityExpertDecisionQuery()
                {
                    OrgId = organizationId,
                    ReestrProjectId = reestrProjectId
                };

                var result = await _mediator.Send<ProjectCyberSecurityExpertDecisionQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<ProjectCyberSecurityExpertDecisionCommandResult>> Add([FromBody] ProjectCyberSecurityExpertDecisionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectCyberSecurityExpertDecisionCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPut]
        public async Task<ResponseCore<ProjectCyberSecurityExpertDecisionCommandResult>> Put([FromBody] ProjectCyberSecurityExpertDecisionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectCyberSecurityExpertDecisionCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<ProjectCyberSecurityExpertDecisionCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ProjectCyberSecurityExpertDecisionCommand model = new ProjectCyberSecurityExpertDecisionCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
