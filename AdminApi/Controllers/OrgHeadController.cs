using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class OrgHead : Controller
    {
        IMediator _mediator;
        public OrgHead(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ResponseCore<OrgHeadCommandResult>> Add([FromQuery] OrgHeadCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;

                var result = await _mediator.Send<OrgHeadCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgHeadCommandResult>> Put([FromQuery] OrgHeadCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;

                var result = await _mediator.Send<OrgHeadCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgHeadCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgHeadCommand model = new OrgHeadCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
