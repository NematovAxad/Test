using DocumentFormat.OpenXml.EMMA;
using Domain;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.States;
using JohaRepository;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class ReestrProjectExceptionCommandHandler : IRequestHandler<ReestrProjectExceptionCommand, ReestrProjectExceptionCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<ReestrProjectException, int> _projectExceptions;

        public ReestrProjectExceptionCommandHandler(IRepository<Organizations, int> organizations, IRepository<ReestrProjectException, int> projectExceptions)
        {
            _organizations = organizations;
            _projectExceptions = projectExceptions;
        }

        public async Task<ReestrProjectExceptionCommandResult> Handle(ReestrProjectExceptionCommand request, CancellationToken cancellationToken)
        {


            var organization = _organizations.Find(o => o.Id == request.OrganizationId).FirstOrDefault();

            if (organization == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            
            var exeption = _projectExceptions.Find(e => e.ReestrProjectId == request.ReestrProjectId && e.OrganizationId == request.OrganizationId).FirstOrDefault();


            if (exeption == null && request.IsException == true)
            {
                var addModel = new ReestrProjectException();

                addModel.OrganizationId = request.OrganizationId;
                addModel.ReestrProjectId = request.ReestrProjectId;
                addModel.Exception = true;
                addModel.ExpertPinfl = request.UserPinfl;
                
                _projectExceptions.Add(addModel);   
            }
            if(exeption != null && request.IsException == false)
            {
                _projectExceptions.Remove(exeption);
            }

            return new ReestrProjectExceptionCommandResult() { IsSuccess = true };
        }
    }
}
