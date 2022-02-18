using Domain;
using Domain.Models;
using Domain.Models.FifthSection;
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
using UserHandler.Commands.ThirdSection;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class OrgProcessesCommandHandler:IRequestHandler<OrgProcessessCommand, OrgProcessesCommandResult>
    {
        private readonly IRepository<OrgProcesses, int> _orgProcesses;
        private readonly IRepository<Organizations, int> _organization;
        public OrgProcessesCommandHandler(IRepository<OrgProcesses, int> orgProcesses, IRepository<Organizations, int> organization)
        {
            _orgProcesses = orgProcesses;
            _organization = organization;
        }
        public async Task<OrgProcessesCommandResult> Handle(OrgProcessessCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgProcessesCommandResult() { IsSuccess = true };
        }
        public void Add(OrgProcessessCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            
            var orgProcesses = _orgProcesses.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (orgProcesses != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            
            OrgProcesses addModel = new OrgProcesses()
            {
                OrganizationId = model.OrganizationId,
                ProcessNumber = model.ProcessNumber,
                ItProcessNumber = model.ItProcessNumber
            };
            if (model.File != null)
            {
                var filePath = FileState.AddFile("commonDocs", model.File);
                addModel.FilePath = filePath;
            }
            _orgProcesses.Add(addModel);
        }
        public void Update(OrgProcessessCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            var orgProcesses = _orgProcesses.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgProcesses == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");



            orgProcesses.OrganizationId = model.OrganizationId;
            orgProcesses.ProcessNumber = model.ProcessNumber;
            orgProcesses.ItProcessNumber = model.ItProcessNumber;
           
            if (model.File != null)
            {
                var filePath = FileState.AddFile("commonDocs", model.File);
                orgProcesses.FilePath = filePath;
            }
            _orgProcesses.Update(orgProcesses);
        }
        public void Delete(OrgProcessessCommand model)
        {
            var orgProcesses = _orgProcesses.Find(h => h.Id == model.Id).FirstOrDefault();
            if (orgProcesses == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == orgProcesses.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _orgProcesses.Remove(orgProcesses);
        }
    }
}
