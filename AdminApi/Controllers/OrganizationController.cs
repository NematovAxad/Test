using AdminHandler.Commands.Organization;
using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
using ApiConfigs;
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

        [HttpGet]
        public async Task<ResponseCore<OrgQueryResult>> Get([FromQuery] int id)
        {
            try
            {
                OrgQuery model = new OrgQuery()
                {
                    OrgId = id
                };

                var result = await _mediator.Send<OrgQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public async Task<ResponseCore<BasedDocsCommandResult>> AddBasedDocs(BasedDocsCommand model)
        {
            string ext = System.IO.Path.GetExtension(model.File.FileName);
            var filePath = FileState.AddFile("basedDocs", model.File);
            model.EventType = Domain.Enums.EventType.Add;
            model.BasedDoc.Path = filePath;
            var result = await _mediator.Send(model);
            return result;
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
