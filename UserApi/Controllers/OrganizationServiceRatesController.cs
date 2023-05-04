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
    public class OrganizationServiceRates : Controller
    {
        IMediator _mediator;
        public OrganizationServiceRates(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ResponseCore<OrganizationServiceRateQueryResult>> Get([FromQuery] int serviceId, int rateId)
        {
            try
            {
                OrganizationServiceRateQuery model = new OrganizationServiceRateQuery()
                {
                    ServiceId = serviceId,
                    RateId = rateId
                };

                var result = await _mediator.Send<OrganizationServiceRateQueryResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        [HttpPost]
        public async Task<ResponseCore<OrganizationServiceRateCommandResult>> Add([FromBody] OrganizationServiceRateCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Add;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                model.UserPinfl = this.UserPinfl();
                var result = await _mediator.Send<OrganizationServiceRateCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpPut]
        public async Task<ResponseCore<OrganizationServiceRateCommandResult>> Put([FromBody] OrganizationServiceRateCommand model)
        {
            try
            {
                model.EventType = Domain.Enums.EventType.Update;
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                model.UserPinfl = this.UserPinfl();
                var result = await _mediator.Send<OrganizationServiceRateCommandResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        [HttpDelete]
        public async Task<ResponseCore<OrganizationServiceRateCommandResult>> Delete([FromQuery] int id)
        {
            try
            {
                OrganizationServiceRateCommand model = new OrganizationServiceRateCommand() { EventType = Domain.Enums.EventType.Delete, Id = id };
                model.UserId = this.UserId();
                model.UserOrgId = this.UserOrgId();
                model.UserPermissions = this.UserRights();
                model.UserPinfl = this.UserPinfl();
                return await _mediator.Send(model);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
