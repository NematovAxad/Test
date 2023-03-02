using Domain;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.Models.ThirdSection;
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
    public class OrgPublicServicesCommandHandler : IRequestHandler<OrgPublicServicesCommand, OrgPublicServicesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrganizationPublicServices, int> _orgPublicServices;

        public OrgPublicServicesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrganizationPublicServices, int> orgPublicServices)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgPublicServices = orgPublicServices;
        }
        public async Task<OrgPublicServicesCommandResult> Handle(OrgPublicServicesCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgPublicServicesCommandResult() { IsSuccess = true };
        }
        public void Add(OrgPublicServicesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var orgPublicServices = _orgPublicServices.Find(h => h.OrganizationId == model.OrganizationId && h.ServiceName == model.ServiceName).FirstOrDefault();
            if (orgPublicServices != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            
            OrganizationPublicServices addModel = new OrganizationPublicServices()
            {
                OrganizationId = model.OrganizationId,
                ServiceName = model.ServiceName,
                UserTypes = model.UserTypes,
                RenderingForm = model.RenderingForm,
                PortalLink = model.PortalLink,
                ServiceLink = model.ServiceLink,
                MobileApp = model.MobileApp,
                OtherResources = model.OtherResources,
                IsPaid = model.IsPaid,
                ServiceResult = model.ServiceResult,
                ServiceOtherResult = model.ServiceOtherResult,
                MechanizmForTrackingProgress = model.MechanizmForTrackingProgress,
                TrackingProgressBy = model.TrackingProgressBy,
                ReglamentUpdated = model.ReglamentUpdated,
                ReglamentPath = model.ReglamentFilePath,
            };
            
            
            _orgPublicServices.Add(addModel);
        }
        public void Update(OrgPublicServicesCommand model)
        {
            var service = _orgPublicServices.Find(h => h.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == service.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            if(!String.IsNullOrEmpty(model.ServiceName))
                service.ServiceName = model.ServiceName;

            if (!String.IsNullOrEmpty(model.UserTypes))
                service.UserTypes = model.UserTypes;

            if (!String.IsNullOrEmpty(model.RenderingForm))
                service.RenderingForm = model.RenderingForm;

            if (!String.IsNullOrEmpty(model.PortalLink))
                service.PortalLink = model.PortalLink;

            if (!String.IsNullOrEmpty(model.ServiceLink))
                service.ServiceLink = model.ServiceLink;

            if (!String.IsNullOrEmpty(model.MobileApp))
                service.MobileApp = model.MobileApp;

            if (!String.IsNullOrEmpty(model.OtherResources))
                service.OtherResources = model.OtherResources;

            service.IsPaid = model.IsPaid;

            if (!String.IsNullOrEmpty(model.ServiceResult))
                service.ServiceResult = model.ServiceResult;

            if (!String.IsNullOrEmpty(model.ServiceOtherResult))
                service.ServiceOtherResult = model.ServiceOtherResult;

            service.MechanizmForTrackingProgress = model.MechanizmForTrackingProgress;

            if (!String.IsNullOrEmpty(model.TrackingProgressBy))
                service.TrackingProgressBy = model.TrackingProgressBy;


            service.ReglamentPath = model.ReglamentFilePath;

            service.ReglamentUpdated = model.ReglamentUpdated;

            _orgPublicServices.Update(service);
        }
        public void Delete(OrgPublicServicesCommand model)
        {
            var service = _orgPublicServices.Find(h => h.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == service.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            _orgPublicServices.Remove(service);
        }
    }
}
