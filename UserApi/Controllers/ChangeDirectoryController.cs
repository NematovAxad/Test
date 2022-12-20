using CoreResult.ResponseCores;
using Domain;
using Domain.Models.SecondSection;
using JohaRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrPassportCommands;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeDirectory : Controller
    {
        private readonly IRepository<WebSiteRequirements, int> _requirements;
        IMediator _mediator;
        public ChangeDirectory(IMediator mediator, IRepository<WebSiteRequirements, int> requirements)
        {
            _mediator = mediator;
            _requirements = requirements;
        }


        //[HttpPost]
        //public async Task<bool> Change()
        //{
        //    var requirements = _requirements.GetAll().ToList();

        //    foreach(WebSiteRequirements requirement in requirements)
        //    {

        //        if (!String.IsNullOrEmpty(requirement.ScreenLink3))
        //            requirement.ScreenLink3 = "/" + requirement.ScreenLink3;

        //        _requirements.Update(requirement);
        //    }

        //    return true;
        //}
    }
}
