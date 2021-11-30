using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Organization
{
    public class SubOrgCommandHandler : IRequestHandler<SubOrgCommand, SubOrgCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<SubOrganizations, int> _subOrganizations;

        public SubOrgCommandHandler(IRepository<Organizations, int> organizations, IRepository<SubOrganizations, int> subOrganizations)
        {
            _organizations = organizations;
            _subOrganizations = subOrganizations;
        }

        public async Task<SubOrgCommandResult> Handle(SubOrgCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new SubOrgCommandResult() { IsSuccess = true };
        }
        public void Add(SubOrgCommand model)
        {
            var subOrg = _subOrganizations.Find(o => o.Name == model.Name).FirstOrDefault();
            if (subOrg != null)
                throw ErrorStates.NotAllowed(model.Name);
            var org = _organizations.Find(o => o.Id == model.ParentId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());
            SubOrganizations addModel = new SubOrganizations()
            {
                OrganizationId = model.ParentId,
                Name = model.Name,
                DirectorFirstName = model.DirectorFirstName,
                DirectorLastName = model.DirectorLastName,
                DirectorMidName = model.DirectorMidName,
                OwnerType = model.OwnerType,
                OfficialSite = model.OfficialSite,
                Contacts = model.Contacts
            };
            _subOrganizations.Add(addModel);
        }
        public void Update(SubOrgCommand model)
        {
            var subOrg = _subOrganizations.Find(o => o.Id == model.Id).FirstOrDefault();
            if (subOrg == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            SubOrganizations updateModel = new SubOrganizations()
            {
                Id = model.Id,
                OrganizationId = subOrg.OrganizationId,
                Name = model.Name,
                DirectorFirstName = model.DirectorFirstName,
                DirectorLastName = model.DirectorLastName,
                DirectorMidName = model.DirectorMidName,
                OwnerType = model.OwnerType,
                OfficialSite = model.OfficialSite,
                Contacts = model.Contacts
            };

            _subOrganizations.Update(updateModel);
        }
        public void Delete(SubOrgCommand model)
        {
            var subOrg = _subOrganizations.Find(o => o.Id == model.Id).FirstOrDefault();
            if (subOrg == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _subOrganizations.Remove(model.Id);
        }
    }
}
