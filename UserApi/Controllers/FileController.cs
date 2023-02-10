using ApiConfigs;
using CoreResult.ResponseCores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Results.ReestrPassportResult;
using Domain;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class FileController : Controller
    {
        IMediator _mediator;
        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public ResponseCore<string> AddFile([FromForm] FileModel model)
        {
            try
            {

                var filePath = FileState.AddFile("apiUser", "commonDocs", model.File);

                return filePath;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
