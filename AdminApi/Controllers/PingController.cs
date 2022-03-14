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
    public class Ping : Controller
    {
        IMediator _mediator;
        public Ping(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<PingQueryResult>> Get([FromQuery] int orgId, int deadlineId)
        {
            try
            {
                PingQuery model = new PingQuery()
                {
                    OrganizationId = orgId,
                    DeadlineId = deadlineId
                };

                var result = await _mediator.Send<PingQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
