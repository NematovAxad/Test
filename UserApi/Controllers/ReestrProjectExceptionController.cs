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
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;
using UserHandler.Commands.SixthSectionCommands;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class ReestrProjectException : Controller
    {
        IMediator _mediator;
        public ReestrProjectException(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<ReestrProjectExceptionQueryResult>> Get([FromQuery] int organizationId)
        {
            try
            {
                ReestrProjectExceptionQuery model = new ReestrProjectExceptionQuery()
                {
                    OrganizationId = organizationId
                };

                var result = await _mediator.Send<ReestrProjectExceptionQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<ReestrProjectExceptionCommandResult>> SetException([FromBody] ReestrProjectExceptionCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserPinfl = this.UserPinfl();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ReestrProjectExceptionCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
