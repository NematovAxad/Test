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
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new OrganizationDocsCommandResult() {Id = id, IsSuccess = true };
        }

        public int Add(OrganizationDocsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            if(org.HasOrgDocuments == false)
            {
                throw ErrorStates.NotAllowed(org.Id.ToString());
            }
            
            
            OrganizationDocuments addModel = new OrganizationDocuments()
            {
                OrganizationId = model.OrganizationId,
                DocumentNo = model.DocumentNo,
                DocumentDate = model.DocumentDate,
                DocumentType = model.DocumentType,
                DocumentName = model.DocumentName,
                MainPurpose = model.MainPurpose,
                Path = model.FilePath
            };

            if (!String.IsNullOrEmpty(model.UserPinfl))
                addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _orgDocuments.Add(addModel);

            return addModel.Id;
        }

        public int Update(OrganizationDocsCommand model)
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
            orgDoc.DocumentDate = model.DocumentDate;
            orgDoc.DocumentType = model.DocumentType;
            orgDoc.DocumentName = model.DocumentName;
            orgDoc.MainPurpose = model.MainPurpose;
            orgDoc.Path = model.FilePath;

            if (!String.IsNullOrEmpty(model.UserPinfl))
                orgDoc.UserPinfl = model.UserPinfl;
            orgDoc.LastUpdate = DateTime.Now;

            _orgDocuments.Update(orgDoc);

            return orgDoc.Id;
        }

        public int Delete(OrganizationDocsCommand model)
        {
            var orgDoc = _orgDocuments.Find(d => d.Id == model.Id).FirstOrDefault();
            if (orgDoc == null)
                throw ErrorStates.Error(UIErrors.BasedDocNotFound);
            
            var org = _organizations.Find(o => o.Id == orgDoc.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            _orgDocuments.Remove(orgDoc);

            return orgDoc.Id;
        }
    }
}
