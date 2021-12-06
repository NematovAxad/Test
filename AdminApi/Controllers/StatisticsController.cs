﻿using AdminHandler.Commands.Organization;
using AdminHandler.Querys.Organization;
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
    public class Statistics : Controller
    {
        IMediator _mediator;
        public Statistics(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<SubOrgStatisticsQueryResult>> Get([FromQuery] int orgId)
        {
            try
            {
                SubOrgStatisticsQuery model = new SubOrgStatisticsQuery()
                {
                    OrganizationId = orgId
                };

                var result = await _mediator.Send<SubOrgStatisticsQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<SubOrgStatisticsCommandResult>> AddOrg([FromBody] SubOrgStatisticsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;

                var result = await _mediator.Send<SubOrgStatisticsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<SubOrgStatisticsCommandResult>> PutOrg([FromBody] SubOrgStatisticsCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;

                var result = await _mediator.Send<SubOrgStatisticsCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<SubOrgStatisticsCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                SubOrgStatisticsCommand model = new SubOrgStatisticsCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
