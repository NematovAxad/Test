using AdminHandler.Commands.Ranking;
using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using CoreResult.ResponseCores;
using Domain.Enums;
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
    public class Deadline : Controller
    {
        IMediator _mediator;
        public Deadline(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<DeadlineQueryResult>> Get([FromQuery] int year, Quarters quarter, bool isActive)
        {
            try
            {
                DeadlineQuery model = new DeadlineQuery()
                {
                    Year = year,
                    Quarter = quarter,
                    IsActive = isActive
                };

                var result = await _mediator.Send<DeadlineQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<DeadlineCommandResult>> Add([FromQuery] DeadlineCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;

                var result = await _mediator.Send<DeadlineCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<DeadlineCommandResult>> Put([FromQuery] DeadlineCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;

                var result = await _mediator.Send<DeadlineCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<DeadlineCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                DeadlineCommand model = new DeadlineCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
