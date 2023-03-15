using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain;
using Domain.Models.FirstSection;
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

namespace AdminHandler.Handlers.Organization
{
    public class OrgDocsControlCommandHandler : IRequestHandler<OrgDocsControlCommand, OrgDocsControlCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationDocuments, int> _orgDocuments;

        public OrgDocsControlCommandHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationDocuments, int> orgDocuments)
        {
            _organizations = organizations;
            _orgDocuments = orgDocuments;
        }

        public async Task<OrgDocsControlCommandResult> Handle(OrgDocsControlCommand request, CancellationToken cancellationToken)
        {
            var org = _organizations.Find(o => o.Id == request.OrganizationId).FirstOrDefault();

            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            if (!request.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((request.UserOrgId == org.UserServiceId) && (request.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            if(request.OrgHasDocs == false)
            {
                var documents = _orgDocuments.Find(d=>d.OrganizationId == request.OrganizationId);

                _orgDocuments.RemoveRange(documents);
            }
            
            org.HasOrgDocuments = request.OrgHasDocs;

            _organizations.Update(org);

            return new OrgDocsControlCommandResult() { Id = org.Id, IsSuccess = true };
        }
    }
}
