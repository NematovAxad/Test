using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
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
    public class ContentManager : Controller
    {
        IMediator _mediator;
        public ContentManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<ContentManagerQueryResult>> Get([FromQuery] int id, int organizationId)
        {
            try
            {
                ContentManagerQuery model = new ContentManagerQuery()
                {
                    Id = id,
                    OrganizationId = organizationId
                };

                var result = await _mediator.Send<ContentManagerQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<ContentManagerCommandResult>> Add([FromQuery] ContentManagerCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;

                var result = await _mediator.Send<ContentManagerCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ContentManagerCommandResult>> Put([FromQuery] ContentManagerCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;

                var result = await _mediator.Send<ContentManagerCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<ContentManagerCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ContentManagerCommand model = new ContentManagerCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
