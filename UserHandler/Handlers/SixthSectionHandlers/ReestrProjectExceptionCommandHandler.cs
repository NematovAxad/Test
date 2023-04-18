using DocumentFormat.OpenXml.EMMA;
using Domain;
using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Models.Ranking.Administrations;
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
        private readonly IRepository<ARankTable, int> _aRank;
        private readonly IRepository<GRankTable, int> _gRank;
        private readonly IRepository<XRankTable, int> _xRank;

        public ReestrProjectExceptionCommandHandler(IRepository<Organizations, int> organizations, IRepository<ReestrProjectException, int> projectExceptions, IRepository<ARankTable, int> aRank, IRepository<GRankTable, int> gRank, IRepository<XRankTable, int> xRank)
        {
            _organizations = organizations;
            _projectExceptions = projectExceptions;
            _aRank = aRank;
            _gRank = gRank;
            _xRank = xRank;
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
                
                if(organization.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
                {
                    var ranks = _aRank.Find(r => r.OrganizationId == request.OrganizationId && r.ElementId == request.ReestrProjectId).ToList();

                    _aRank.RemoveRange(ranks);
                }
                if(organization.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
                {
                    var ranks = _xRank.Find(r => r.OrganizationId == request.OrganizationId && r.ElementId == request.ReestrProjectId).ToList();

                    _xRank.RemoveRange(ranks);
                }
                if (organization.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
                {
                    var ranks = _gRank.Find(r => r.OrganizationId == request.OrganizationId && r.ElementId == request.ReestrProjectId).ToList();

                    _gRank.RemoveRange(ranks);
                }
            }
            if(exeption != null && request.IsException == false)
            {
                _projectExceptions.Remove(exeption);
            }

            return new ReestrProjectExceptionCommandResult() { IsSuccess = true };
        }
    }
}
