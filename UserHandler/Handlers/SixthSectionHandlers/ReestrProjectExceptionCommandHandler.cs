using DocumentFormat.OpenXml.EMMA;
using Domain;
using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Models.Ranking.Administrations;
using Domain.Permission;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MainInfrastructures.Migrations;
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
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IDataContext _db;

        public ReestrProjectExceptionCommandHandler(IRepository<Organizations, int> organizations, IRepository<ReestrProjectException, int> projectExceptions, IRepository<ARankTable, int> aRank, IRepository<GRankTable, int> gRank, IRepository<XRankTable, int> xRank, IDataContext db, IRepository<Deadline, int> deadline)
        {
            _organizations = organizations;
            _projectExceptions = projectExceptions;
            _aRank = aRank;
            _gRank = gRank;
            _xRank = xRank;
            _db = db;
            _deadline = deadline;
        }

        public async Task<ReestrProjectExceptionCommandResult> Handle(ReestrProjectExceptionCommand request, CancellationToken cancellationToken)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!(request.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || request.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (request.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || request.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

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

                _db.Context.Set<ReestrProjectException>().Add(addModel);
                
                if(organization.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
                {
                    var ranks = _aRank.Find(r => r.OrganizationId == request.OrganizationId && r.ElementId == request.ReestrProjectId).ToList();

                    _db.Context.Set<ARankTable>().RemoveRange(ranks);
                }
                if(organization.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
                {
                    var ranks = _xRank.Find(r => r.OrganizationId == request.OrganizationId && r.ElementId == request.ReestrProjectId).ToList();

                    _db.Context.Set<XRankTable>().RemoveRange(ranks);
                }
                if (organization.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
                {
                    var ranks = _gRank.Find(r => r.OrganizationId == request.OrganizationId && r.ElementId == request.ReestrProjectId).ToList();

                    _db.Context.Set<GRankTable>().RemoveRange(ranks);
                }
            }
            if(exeption != null && request.IsException == false)
            {
                _db.Context.Set<ReestrProjectException>().Remove(exeption);
            }

            _db.Context.SaveChanges();

            return new ReestrProjectExceptionCommandResult() { IsSuccess = true };
        }
    }
}
