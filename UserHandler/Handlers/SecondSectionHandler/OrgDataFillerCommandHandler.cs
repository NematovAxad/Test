using Domain.Models;
using Domain.Models.Ranking;
using Domain.Models.SecondSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SecondSectionCommand;
using UserHandler.Results.SecondSectionCommandResult;

namespace UserHandler.Handlers.SecondSectionHandler
{
    public class OrgDataFillerCommandHandler : IRequestHandler<OrgDataFillerCommand, OrgDataFillerCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrgDataFiller, int> _orgDataFiller;

        public OrgDataFillerCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrgDataFiller, int> orgDataFiller)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgDataFiller = orgDataFiller;
        }

        public async Task<OrgDataFillerCommandResult> Handle(OrgDataFillerCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgDataFillerCommandResult() { IsSuccess = true };
        }
        public void Add(OrgDataFillerCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var dataFiller = _orgDataFiller.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (dataFiller != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            OrgDataFiller addModel = new OrgDataFiller()
            {
                OrganizationId = model.OrganizationId,
                FullName = model.FullName,
                Position = model.Position,
                Contacts = model.Contacts
            };
            _orgDataFiller.Add(addModel);
        }
        public void Update(OrgDataFillerCommand model)
        {
            var orgDataFiller = _orgDataFiller.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgDataFiller == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgDataFiller.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            orgDataFiller.FullName = model.FullName;
            orgDataFiller.Position = model.Position;
            orgDataFiller.Contacts = model.Contacts;

            _orgDataFiller.Update(orgDataFiller);
        }
        public void Delete(OrgDataFillerCommand model)
        {
            var orgDataFiller = _orgDataFiller.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgDataFiller == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgDataFiller.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _orgDataFiller.Remove(orgDataFiller);
        }
    }
}
