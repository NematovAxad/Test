using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.Ranking;
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
    public class OrgInformationSystemsCommandHandler : IRequestHandler<OrgInformationSystemsCommand, OrgInformationSystemsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<OrgInformationSystems, int> _orgInfoSystem;

        public OrgInformationSystemsCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<OrgInformationSystems, int> orgInfoSystem)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _orgInfoSystem = orgInfoSystem;
        }
        public async Task<OrgInformationSystemsCommandResult> Handle(OrgInformationSystemsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new OrgInformationSystemsCommandResult() { IsSuccess = true };
        }
        public void Add(OrgInformationSystemsCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var orgInfoSystems = _orgInfoSystem.Find(h => h.OrganizationId == model.OrganizationId && h.SystemName == model.SystemName).FirstOrDefault();
            if (orgInfoSystems != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            
            OrgInformationSystems addModel = new OrgInformationSystems()
            {
                OrganizationId = model.OrganizationId,
                SystemName = model.SystemName,
                SystemAppointment = model.SystemAppointment,
                SystemReestrNumber = model.SystemReestrNumber,
                SystemCondition = model.SystemCondition,
                CommissioningDate = model.CommissioningDate,
                ExpertOpinionDate = model.ExpertOpinionDate,
                ExpertOpinionNumber = model.ExpertOpinionNumber,
                ListOfServices = model.ListOfServices,
                UsersCount = model.UsersCount,
                UsesClassifiers = model.UsesClassifiers,
                UsedClassifiers = model.UsedClassifiers,
                OtherClassifiers = model.OtherClassifiers,
                HasIntegrationWithEgovernment = model.HasIntegrationWithEgovernment,
                IntegrationInterdepartmentalPlatform = model.IntegrationInterdepartmentalPlatform,
                TransmittingInformationsFirst = model.TransmittingInformationsFirst,
                IntegratedRegisterClassifiers = model.IntegratedRegisterClassifiers,
                TransmittingInformationsSecond = model.TransmittingInformationsSecond,
                IntegratedCentralDatabase = model.IntegratedCentralDatabase,
                TransmittingInformationsThird = model.TransmittingInformationsThird,
                IntegratedComplexesOfSystems = model.IntegratedComplexesOfSystems,
                TransmittingInformationsFourth = model.TransmittingInformationsFourth,
                IntegratedPaymentSystem = model.IntegratedPaymentSystem,
                PaymentSystemName = model.PaymentSystemName,
                AvailabilityAutomationOfService = model.AvailabilityAutomationOfService,
                AutomatedServices = model.AutomatedServices
            };
            _orgInfoSystem.Add(addModel);
        }
        public void Update(OrgInformationSystemsCommand model)
        {
            var system = _orgInfoSystem.Find(h => h.Id == model.Id).FirstOrDefault();
            if (system == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == system.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            system.SystemName = model.SystemName;
            system.SystemAppointment = model.SystemAppointment;
            system.SystemReestrNumber = model.SystemReestrNumber;
            system.SystemCondition = model.SystemCondition;
            system.CommissioningDate = model.CommissioningDate;
            system.ExpertOpinionDate = model.ExpertOpinionDate;
            system.ExpertOpinionNumber = model.ExpertOpinionNumber;
            system.ListOfServices = model.ListOfServices;
            system.UsersCount = model.UsersCount;
            system.UsesClassifiers = model.UsesClassifiers;
            system.UsedClassifiers = model.UsedClassifiers;
            system.OtherClassifiers = model.OtherClassifiers;
            system.HasIntegrationWithEgovernment = model.HasIntegrationWithEgovernment;
            system.IntegrationInterdepartmentalPlatform = model.IntegrationInterdepartmentalPlatform;
            system.TransmittingInformationsFirst = model.TransmittingInformationsFirst;
            system.IntegratedRegisterClassifiers = model.IntegratedRegisterClassifiers;
            system.TransmittingInformationsSecond = model.TransmittingInformationsSecond;
            system.IntegratedCentralDatabase = model.IntegratedCentralDatabase;
            system.TransmittingInformationsThird = model.TransmittingInformationsThird;
            system.IntegratedComplexesOfSystems = model.IntegratedComplexesOfSystems;
            system.TransmittingInformationsFourth = model.TransmittingInformationsFourth;
            system.IntegratedPaymentSystem = model.IntegratedPaymentSystem;
            system.PaymentSystemName = model.PaymentSystemName;
            system.AvailabilityAutomationOfService = model.AvailabilityAutomationOfService;
            system.AutomatedServices = model.AutomatedServices;

            _orgInfoSystem.Update(system);
        }
        public void Delete(OrgInformationSystemsCommand model)
        {
            var service = _orgInfoSystem.Find(h => h.Id == model.Id).FirstOrDefault();
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
            _orgInfoSystem.Remove(service);
        }
    }
}
