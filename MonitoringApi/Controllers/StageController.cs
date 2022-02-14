using ApiConfigs;
using CoreResult.ResponseCores;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringHandler.Commands.StructureCommands;
using MonitoringHandler.Querys.StructureQuerys;
using MonitoringHandler.Results.StructureResults.CommandResults;
using MonitoringHandler.Results.StructureResults.QueryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringApi.Controllers
{
    [Route("apiMonitoring/[controller]/[action]")]
    public class Stage : Controller
    {
        IMediator _mediator;
        public Stage(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ResponseCore<StageQueryResult>> Get([FromQuery] int id, int projectId, StageStatus status)
        {
            try
            {
                StageQuery model = new StageQuery()
                {
                    Id = id,
                    ProjectId = projectId,
                    Status = status
                };

                var result = await _mediator.Send<StageQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<StageCommandResult>> Add([FromQuery] StageCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<StageCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<StageFileCommandResult>> AddStageFile([FromQuery] StageFileCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<StageFileCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<CommentCommandResult>> AddComment([FromQuery] CommentCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<CommentCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPut]
        public async Task<ResponseCore<StageCommandResult>> Put([FromQuery] StageCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<StageCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<StageCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                StageCommand model = new StageCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
        [HttpDelete]
        public async Task<ResponseCore<StageFileCommandResult>> DeleteStageFile([FromQuery] int id)
        {
            try
            {
                StageFileCommand model = new StageFileCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
        [HttpDelete]
        public async Task<ResponseCore<CommentCommandResult>> DeleteComment([FromQuery] int id)
        {
            try
            {
                CommentCommand model = new CommentCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
