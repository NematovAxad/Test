using Domain;
using Domain.Enums;
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
using UserHandler.Results.ReestrPassportResult;
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
            return new OrgPublicServicesCommandResult() { Id = id, IsSuccess = false };
        }
        public int Add(OrgPublicServicesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var orgPublicServices = _orgPublicServices.Find(h => h.OrganizationId == model.OrganizationId && h.ServiceNameRu == model.ServiceNameRu).FirstOrDefault();
            if (orgPublicServices != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.OperatorDeadlineDate.ToString());

            
            OrganizationPublicServices addModel = new OrganizationPublicServices()
            {
                OrganizationId = model.OrganizationId,
                
            };
            addModel.ServiceNameUz = model.ServiceNameUz;

            addModel.ServiceNameRu = model.ServiceNameRu;


            addModel.ServiceBasedDocumentType = model.ServiceBasedDocumentType;

            addModel.ServiceBasedDocumentName = model.ServiceBasedDocumentName;

            addModel.ServiceBasedDocumentNumber = model.ServiceBasedDocumentNumber;

            addModel.ServiceBasedDocumentDate = model.ServiceBasedDocumentDate;



            addModel.PaidFor = model.PaidFor;

            addModel.ServicePrice = model.ServicePrice;

            addModel.ServicePriceComment = model.ServicePriceComment;



            addModel.ServiceCompletePeriodType = model.ServiceCompletePeriodType;

            addModel.ServiceCompletePeriod = model.ServiceCompletePeriod;


            addModel.ServiceType = model.ServiceType;

            addModel.ServiceLink = model.ServiceLink;

            addModel.ServiceScreenshotLink = model.ServiceScreenshotLink;


            addModel.ServiceSubjects = model.ServiceSubjects;


            addModel.ServiceHasReglament = model.ServiceHasReglament;

            addModel.ServiceReglamentPath = model.ServiceReglamentPath;

            addModel.ServiceReglamentComment = model.ServiceReglamentComment;

            addModel.ServiceHasUpdateReglament = model.ServiceHasUpdateReglament;

            addModel.ServiceUpdateReglamentPath = model.ServiceUpdateReglamentPath;

            addModel.ServiceUpdateReglamentComment = model.ServiceUpdateReglamentComment;



            addModel.MyGovService = model.MyGovService;

            addModel.MyGovLink = model.MyGovLink;

            addModel.MyGovScreenshotLink = model.MyGovScreenshotLink;



            addModel.OtherApps = model.OtherApps;

            addModel.AppName = model.AppName;
                
            addModel.AppLink = model.AppLink;

            addModel.AppScreenshot = model.AppScreenshot;


            addModel.ServiceHasReglamentExpert = model.ServiceHasReglamentExpert;

            addModel.ServiceHasReglamentExpertComment = model.ServiceHasReglamentExpertComment;

            addModel.ServiceHasUpdateReglamentExpert = model.ServiceHasUpdateReglamentExpert;

            addModel.ServiceHasUpdateReglamentExpertComment = model.ServiceHasUpdateReglamentExpertComment;

            addModel.MyGovServiceExpert = model.MyGovServiceExpert;

            addModel.MyGovServiceExpertComment = model.MyGovServiceExpertComment;

            addModel.OtherAppsExpert = model.OtherAppsExpert;

            addModel.OtherAppsExpertComment = model.OtherAppsExpertComment;

            addModel.ServiceTypeExpert = model.ServiceTypeExpert;

            addModel.ServiceTypeExpertComment = model.ServiceTypeExpertComment;

        _orgPublicServices.Add(addModel);

            return addModel.Id;
        }
        public int Update(OrgPublicServicesCommand model)
        {
            var service = _orgPublicServices.Find(h => h.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.NotFound("deadline");

            var org = _organization.Find(o => o.Id == service.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
            {
                if (deadline.OperatorDeadlineDate > DateTime.Now)
                {
                    service.ServiceNameUz = model.ServiceNameUz;

                    service.ServiceNameRu = model.ServiceNameRu;


                    service.ServiceBasedDocumentType = model.ServiceBasedDocumentType;

                    service.ServiceBasedDocumentName = model.ServiceBasedDocumentName;

                    service.ServiceBasedDocumentNumber = model.ServiceBasedDocumentNumber;

                    service.ServiceBasedDocumentDate = model.ServiceBasedDocumentDate;



                    service.PaidFor = model.PaidFor;

                    service.ServicePrice = model.ServicePrice;

                    service.ServicePriceComment = model.ServicePriceComment;



                    service.ServiceCompletePeriodType = model.ServiceCompletePeriodType;

                    service.ServiceCompletePeriod = model.ServiceCompletePeriod;


                    service.ServiceType = model.ServiceType;

                    service.ServiceLink = model.ServiceLink;

                    service.ServiceScreenshotLink = model.ServiceScreenshotLink;


                    service.ServiceSubjects = model.ServiceSubjects;


                    service.ServiceHasReglament = model.ServiceHasReglament;

                    service.ServiceReglamentPath = model.ServiceReglamentPath;

                    service.ServiceReglamentComment = model.ServiceReglamentComment;

                    service.ServiceHasUpdateReglament = model.ServiceHasUpdateReglament;

                    service.ServiceUpdateReglamentPath = model.ServiceUpdateReglamentPath;

                    service.ServiceUpdateReglamentComment = model.ServiceUpdateReglamentComment;



                    service.MyGovService = model.MyGovService;

                    service.MyGovLink = model.MyGovLink;

                    service.MyGovScreenshotLink = model.MyGovScreenshotLink;



                    service.OtherApps = model.OtherApps;

                    service.AppName = model.AppName;

                    service.AppLink = model.AppLink;

                    service.AppScreenshot = model.AppScreenshot;

                }
                else
                {
                    throw ErrorStates.NotAllowed("deadline");
                }
                
            }
            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate > DateTime.Now)
                {
                    service.ServiceHasReglamentExpert = model.ServiceHasReglamentExpert;

                    service.ServiceHasReglamentExpertComment = model.ServiceHasReglamentExpertComment;

                    service.ServiceHasUpdateReglamentExpert = model.ServiceHasUpdateReglamentExpert;

                    service.ServiceHasUpdateReglamentExpertComment = model.ServiceHasUpdateReglamentExpertComment;

                    service.MyGovServiceExpert = model.MyGovServiceExpert;

                    service.MyGovServiceExpertComment = model.MyGovServiceExpertComment;

                    service.OtherAppsExpert = model.OtherAppsExpert;

                    service.OtherAppsExpertComment = model.OtherAppsExpertComment;

                    service.ServiceTypeExpert = model.ServiceTypeExpert;

                    service.ServiceTypeExpertComment = model.ServiceTypeExpertComment;
                }
                else
                {
                    throw ErrorStates.NotAllowed("deadline");
                }
            }
            

                _orgPublicServices.Update(service);

            return service.Id;
        }
        public int Delete(OrgPublicServicesCommand model)
        {
            var service = _orgPublicServices.Find(h => h.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.NotFound(model.Id.ToString());
           
            var org = _organization.Find(o => o.Id == service.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(service.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            
            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
           
            
            _orgPublicServices.Remove(service);

            return service.Id;
        }
    }
}
