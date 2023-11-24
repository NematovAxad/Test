using System.Collections.Generic;
using System.Threading.Tasks;
using ApiConfigs;
using Domain.Models.DashboardModels;
using Domain.Models.FirstSection;
using MainInfrastructures.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("apiAdmin/[controller]/[action]")]
    public class News : Controller
    {
        private readonly IMediator _mediator;
        private readonly IDashboardService _dashboardService;
        
        public News(IMediator mediator, IDashboardService dashboardService)
        {
            _mediator = mediator;
            _dashboardService = dashboardService;
        }
        
        [HttpGet]
        public async Task<List<NewsOnDashboard>> AddNews([FromQuery] int id)
        {
            var result = await _dashboardService.GetNews(id);
            return result;
        }
        
        [HttpPost]
        public async Task<bool> AddNews([FromBody] AddNewsRequest request)
        {
            request.UserPinfl = this.UserPinfl();
            request.UserPermissions = this.UserRights();
            
            var result = await _dashboardService.AddNews(request);
            return result;
        }
        
        [HttpPut]
        public async Task<NewsOnDashboard> UpdateNews([FromBody] UpdateNewsRequest request)
        {
            request.UserPinfl = this.UserPinfl();
            request.UserPermissions = this.UserRights();
            
            var result = await _dashboardService.UpdateNews(request);
            return result;
        }
        
        [HttpDelete]
        public async Task<bool> DeleteNews([FromQuery] int id)
        {
            var result = await _dashboardService.DeleteNews(this.UserRights(), id);
            return result;
        }
    }
}