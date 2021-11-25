using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using CoreResult.ResponseCores;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("apiTurn/[controller]/[action]")]
    public class Organization : Controller
    {
        IMediator _mediator;
        public Organization(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseCore<OrgCommandResult>> AddOrg([FromBody] OrgCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;

                var result = await _mediator.Send<OrgCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<OrgCommandResult>> PutOrg([FromBody] OrgCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;

                var result = await _mediator.Send<OrgCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrgCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrgCommand model = new OrgCommand() { EventType = Domain.Enums.EventType.Delete, Organization = new Organizations() { Id = id } };
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
