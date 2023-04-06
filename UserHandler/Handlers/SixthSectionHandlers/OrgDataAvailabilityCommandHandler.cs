using Domain.Models.FirstSection;
using Domain.Models.SixthSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Results.SixthSectionResults;
using Domain.States;
using Domain;
using MainInfrastructures.Migrations;
using System.Linq;
using Domain.Permission;
using Domain.IntegrationLinks;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrgDataAvailabilityCommandHandler : IRequestHandler<OrgDataAvailabilityCommand, OrgDataAvailabilityCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationDataAvailability, int> _orgDataAvailability;

        public OrgDataAvailabilityCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationDataAvailability, int> orgDataAvailability)
        {
            _organization = organization;
            _deadline = deadline;
            _orgDataAvailability = orgDataAvailability;
        }

        public async Task<OrgDataAvailabilityCommandResult> Handle(OrgDataAvailabilityCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add:
                    id = Add(request);
                    break;
                case Domain.Enums.EventType.Update:
                    id = Update(request);
                    break;
                case Domain.Enums.EventType.Delete:
                    id = Delete(request);
                    break;
            }
            return new OrgDataAvailabilityCommandResult() { Id = id, IsSuccess = false };
        }

        public int Add(OrgDataAvailabilityCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.SixthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            if (!Links.Sections.Contains(model.Section))
                throw ErrorStates.Error(UIErrors.IncorrectSection);


            var orgData = _orgDataAvailability.Find(p => p.OrganizationId == model.OrganizationId && p.Section == model.Section).FirstOrDefault();
            if (orgData != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            OrganizationDataAvailability addModel = new OrganizationDataAvailability();

            addModel.OrganizationId = model.OrganizationId;
            addModel.DeadlineId = deadline.Id;
            addModel.Section = model.Section;
            addModel.Sphere = model.Sphere;
            addModel.DataAvailability = model.DataAvailability;
            addModel.DataRelevance = model.DataRelevance;
            addModel.SetDate = DateTime.Now;
            addModel.UpdateDate = DateTime.Now;
            addModel.ExpertPinfl = model.UserPinfl;
            addModel.ExpertComment = model.ExpertComment;

            _orgDataAvailability.Add(addModel);

            return addModel.Id;
        }

        public int Update(OrgDataAvailabilityCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.SixthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            var orgData = _orgDataAvailability.Find(p => p.Id == model.Id).FirstOrDefault();
            if (orgData == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            orgData.DataAvailability = model.DataAvailability;
            orgData.DataRelevance = model.DataRelevance;
            orgData.UpdateDate = DateTime.Now;
            orgData.ExpertPinfl = model.UserPinfl;
            orgData.ExpertComment = model.ExpertComment;


            _orgDataAvailability.Update(orgData);

            return orgData.Id;
        }

        public int Delete(OrgDataAvailabilityCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.SixthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            var orgData = _orgDataAvailability.Find(p => p.Id == model.Id).FirstOrDefault();
            if (orgData == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            _orgDataAvailability.Remove(orgData);

            return orgData.Id;
        }
    }
}
