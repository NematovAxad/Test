using AdminHandler.Commands.Organization;
using AdminHandler.Results.Organization;
using Domain;
using Domain.Models;
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
    public class OrganizationDocsCommandHandler : IRequestHandler<OrganizationDocsCommand, OrganizationDocsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<OrganizationDocuments, int> _orgDocuments;

        public OrganizationDocsCommandHandler(IRepository<Organizations, int> organizations, IRepository<OrganizationDocuments, int> orgDocuments)
        {
            _organizations = organizations;
            _orgDocuments = orgDocuments;
        }

        public async Task<OrganizationDocsCommandResult> Handle(OrganizationDocsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrganizationDocsCommandResult() { IsSuccess = true };
        }

        public void Add(OrganizationDocsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            
            
            
            OrganizationDocuments addModel = new OrganizationDocuments()
            {
                OrganizationId = model.OrganizationId,
                DocumentNo = model.DocumentNo,
                DocumentDate = DateTime.Now,
                DocumentType = model.DocumentType,
                DocumentName = model.DocumentName,
                MainPurpose = model.MainPurpose,
                Path = model.FilePath
            };
            _orgDocuments.Add(addModel);
        }

        public void Update(OrganizationDocsCommand model)
        {
            var orgDoc = _orgDocuments.Find(d => d.Id == model.Id).FirstOrDefault();
            if (orgDoc == null)
                throw ErrorStates.Error(UIErrors.BasedDocNotFound);
            
            var org = _organizations.Find(o => o.Id == orgDoc.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            orgDoc.DocumentNo = model.DocumentNo;
            orgDoc.DocumentDate = DateTime.Now;
            orgDoc.DocumentType = model.DocumentType;
            orgDoc.DocumentName = model.DocumentName;
            
            orgDoc.Path = model.FilePath;
            

            _orgDocuments.Update(orgDoc);
        }

        public void Delete(OrganizationDocsCommand model)
        {
            var orgDoc = _orgDocuments.Find(d => d.Id == model.Id).FirstOrDefault();
            if (orgDoc == null)
                throw ErrorStates.Error(UIErrors.BasedDocNotFound);
            
            var org = _organizations.Find(o => o.Id == orgDoc.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            _orgDocuments.Remove(model.Id);
        }
    }
}
