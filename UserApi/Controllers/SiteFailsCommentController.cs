using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Queries.ReestrProjectClassificationQuery;
using UserHandler.Results.ReestrProjectClassificationResult;
using UserHandler.Results.SecondSectionQueryResult;
using UserHandler.Queries.SecondSectionQuery;
using ApiConfigs;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Commands.SecondSectionCommand;
using Domain;
using UserHandler.Commands.ReestrPassportCommands;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class SiteFailsComment : Controller
    {
        IMediator _mediator;
        public SiteFailsComment(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ResponseCore<SiteFailCommentQueryResult>> Get([FromQuery] int organizationId, int deadlineid)
        {
            try
            {
                SiteFailCommentQuery model = new SiteFailCommentQuery()
                {
                    OrgId = organizationId,
                    DeadlineId = deadlineid
                };

                var result = await _mediator.Send<SiteFailCommentQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<SiteFailCommentCommandResult>> Add([FromBody] SiteFailCommentCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<SiteFailCommentCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<SiteFailCommentCommandResult>> Put([FromBody] SiteFailCommentCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<SiteFailCommentCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpDelete]
        public async Task<ResponseCore<SiteFailCommentCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                SiteFailCommentCommand model = new SiteFailCommentCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
