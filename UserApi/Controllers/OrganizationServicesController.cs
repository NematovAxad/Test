using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.ThirdSection;
using UserHandler.Queries.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class OrganizationServices : Controller
    {
        IMediator _mediator;
        public OrganizationServices(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseCore<OrganizationServicesQueryResult>> Get([FromQuery] int organizationId, int serviceId)
        {
            try
            {
                OrganizationServicesQuery model = new OrganizationServicesQuery()
                {
                    OrganizationId = organizationId,
                    ServiceId = serviceId
                };

                var result = await _mediator.Send<OrganizationServicesQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrganizationServicesCommandResult>> Add([FromBody] OrganizationServicesCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                var result = await _mediator.Send<OrganizationServicesCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        
        [HttpDelete]
        public async Task<ResponseCore<OrganizationServicesCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrganizationServicesCommand model = new OrganizationServicesCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
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
