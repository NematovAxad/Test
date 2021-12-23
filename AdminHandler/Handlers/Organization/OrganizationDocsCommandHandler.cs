﻿using AdminHandler.Commands.Organization;
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
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.Id) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var filePath = FileState.AddFile("orgDocs", model.File);
            OrganizationDocuments addModel = new OrganizationDocuments()
            {
                OrganizationId = model.OrganizationId,
                DocumentNo = model.DocumentNo,
                DocumentDate = DateTime.Now,
                DocumentType = model.DocumentType,
                DocumentName = model.DocumentName,
                MainPurpose = model.MainPurpose,
                Path = filePath
            };
            _orgDocuments.Add(addModel);
        }

        public void Update(OrganizationDocsCommand model)
        {
            var orgDoc = _orgDocuments.Find(d => d.Id == model.Id).FirstOrDefault();
            if (orgDoc == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == orgDoc.Id) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            orgDoc.DocumentNo = model.DocumentNo;
            orgDoc.DocumentDate = DateTime.Now;
            orgDoc.DocumentType = model.DocumentType;
            orgDoc.DocumentName = model.DocumentName;
            if(model.File!=null)
            {
                var filePath = FileState.AddFile("orgDocs", model.File);
                orgDoc.Path = filePath;
            }

            _orgDocuments.Update(orgDoc);
        }

        public void Delete(OrganizationDocsCommand model)
        {
            var orgDoc = _orgDocuments.Find(d => d.Id == model.Id).FirstOrDefault();
            if (orgDoc == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == orgDoc.OrganizationId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _orgDocuments.Remove(model.Id);
        }
    }
}