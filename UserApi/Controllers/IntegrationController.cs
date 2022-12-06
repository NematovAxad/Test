using CoreResult.ResponseCores;
using Domain.OpenDataModels;
using Domain.ReesterModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class Integration : Controller
    {
        IMediator _mediator;
        public Integration(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<OpenDataQueryResult>> OpenData([FromQuery] int orgId)
        {
            try
            {
                OpenDataQuery model = new OpenDataQuery()
                {
                   OrgId = orgId
                };

                var result = await _mediator.Send<OpenDataQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpGet]
        public async Task<ResponseCore<FirstRequestQueryResult>> ReesterProjects([FromQuery] int orgId, int page, int limit)
        {
            try
            {
                FirstRequestQuery model = new FirstRequestQuery()
                {
                    OrgId = orgId,
                    Page = page,
                    Limit = limit
                };

                var result = await _mediator.Send<FirstRequestQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpGet]
        public async Task<ResponseCore<SecondRequestQueryResult>> ReesterProjectDetails([FromQuery] int id)
        {
            try
            {
                SecondRequestQuery model = new SecondRequestQuery()
                {
                    Id = id
                };

                var result = await _mediator.Send<SecondRequestQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
