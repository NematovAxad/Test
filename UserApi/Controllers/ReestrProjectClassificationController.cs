using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Queries.ReestrPassportQuery;
using UserHandler.Results.ReestrPassportResult;
using UserHandler.Results.ReestrProjectIdentityResult;
using UserHandler.Results.ReestrProjectClassificationResult;
using UserHandler.Queries.ReestrProjectClassificationQuery;
using ApiConfigs;
using UserHandler.Commands.ReestrProjectIdentityCommand;
using UserHandler.Commands.ReestrProjectClassificationCommand;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectClassification : Controller
    {
        IMediator _mediator;
        public ReestrProjectClassification(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<ReestrProjectClassificationQueryResult>> Get([FromQuery] int organizationId, int reestrProjectId)
        {
            try
            {
                ReestrProjectClassificationQuery model = new ReestrProjectClassificationQuery()
                {
                    OrgId = organizationId,
                    ReestrProjectId = reestrProjectId
                };

                var result = await _mediator.Send<ReestrProjectClassificationQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<ReestrProjectClassificationCommandResult>> Add([FromBody] ReestrProjectClassificationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectClassificationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ReestrProjectClassificationCommandResult>> Put([FromBody] ReestrProjectClassificationCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectClassificationCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<ReestrProjectClassificationCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ReestrProjectClassificationCommand model = new ReestrProjectClassificationCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
