﻿using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Queries.SixthSectionQueries;
using UserHandler.Results.SixthSectionResults;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class SpecialForces : Controller
    {
        IMediator _mediator;
        public SpecialForces(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<SpecialForcesQueryResult>> Get([FromQuery] int id, int organizationId)
        {
            try
            {
                SpecialForcesQuery model = new SpecialForcesQuery()
                {
                    OrganizationId = organizationId,
                    Id = id
                };

                var result = await _mediator.Send<SpecialForcesQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<SpecialForcesCommandResult>> Add([FromBody] SpecialForcesCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<SpecialForcesCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<SpecialForcesCommandResult>> Put([FromBody] SpecialForcesCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<SpecialForcesCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<SpecialForcesCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                SpecialForcesCommand model = new SpecialForcesCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
