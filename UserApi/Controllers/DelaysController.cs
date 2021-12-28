using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserHandler.Commands.ThirdSection;
using UserHandler.Queries.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserApi.Controllers
{
 [Route("apiUser/[controller]/[action]")]
    public class Delays : Controller
    {
        IMediator _mediator;
        public Delays(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<ProjectsDelayQueryResult>> Get([FromQuery] int id, int organizationId)
        {
            try
            {
                ProjectsDelayQuery model = new ProjectsDelayQuery()
                {
                    OrganizationId = organizationId,
                    Id = id
                };

                var result = await _mediator.Send<ProjectsDelayQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<ProjectsDelayCommandResult>> Add([FromQuery] ProjectsDelayCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectsDelayCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<ProjectsDelayCommandResult>> Put([FromQuery] ProjectsDelayCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<ProjectsDelayCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<ProjectsDelayCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                ProjectsDelayCommand model = new ProjectsDelayCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
