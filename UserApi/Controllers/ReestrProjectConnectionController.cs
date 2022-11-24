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
    public class ReestrProjectConnection : Controller
    {
        IMediator _mediator;
        public ReestrProjectConnection(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<ProjectConnectionQueryResult>> Get([FromQuery] int organizationId, int reestrProjectId)
        {
            try
            {
                ProjectConnectionQuery model = new ProjectConnectionQuery()
                {
                    OrgId = organizationId,
                    ReestrProjectId = reestrProjectId
                };

                var result = await _mediator.Send<ProjectConnectionQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<ProjectConnectionResult>> Add([FromBody] ProjectConnectionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectConnectionResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ProjectConnectionResult>> Put([FromBody] ProjectConnectionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectConnectionResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<ProjectConnectionResult>> Delete([FromQuery] int id)
        {
            try
            {
                ProjectConnectionCommand model = new ProjectConnectionCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
