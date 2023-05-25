using Domain;
using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.Models.SixthSection;
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
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Commands.ThirdSection;
using UserHandler.Results.SixthSectionResults;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class SpecialForcesCommandHandler : IRequestHandler<SpecialForcesCommand, SpecialForcesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationIctSpecialForces, int> _specialForces;

        public SpecialForcesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationIctSpecialForces, int> specialForces)
        {
            _organization = organization;
            _deadline = deadline;
            _specialForces = specialForces;
        }
        public async Task<SpecialForcesCommandResult> Handle(SpecialForcesCommand request, CancellationToken cancellationToken)
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
            return new SpecialForcesCommandResult() { Id = id, IsSuccess = false };
        }
        public int Add(SpecialForcesCommand model)
        {
            var specialForces = _specialForces.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (specialForces != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());


            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.SixthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);
            

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                throw ErrorStates.NotAllowed("permission");



            OrganizationIctSpecialForces addModel = new OrganizationIctSpecialForces();


            addModel.OrganizationId = model.OrganizationId;

            addModel.HasSpecialForces = model.HasSpecialForces;

            addModel.SpecialForcesName = model.SpecialForcesName;

            addModel.FormOfSpecialForces = model.FormOfSpecialForces;

            addModel.FullNameDirector = model.FullNameDirector;

            addModel.HeadPosition = model.HeadPosition;

            addModel.WorkPhone = model.WorkPhone;

            addModel.MobilePhone = model.MobilePhone;

            addModel.Email = model.Email;

            addModel.MinistryAgreedHead = model.MinistryAgreedHead;

            addModel.MinistryAgreedHeadDocument = model.MinistryAgreedHeadDocument;

            addModel.HasCharacterizingDocument = model.HasCharacterizingDocument;

            addModel.CharacterizingDocument = model.CharacterizingDocument;

            addModel.HasMinistryAgreedCharacterizingDocument = model.HasMinistryAgreedCharacterizingDocument;

            addModel.MinistryAgreedCharacterizingDocument = model.MinistryAgreedCharacterizingDocument;

            addModel.EmployeesSum = model.EmployeesSum;

            addModel.CentralofficeEmployees = model.CentralofficeEmployees;

            addModel.RegionalEmployees = model.RegionalEmployees;

            addModel.SubordinateEmployees = model.SubordinateEmployees;

            addModel.InformationSecurityEmployees = model.InformationSecurityEmployees;

            addModel.InformationSystemDatabaseEmployees = model.InformationSystemDatabaseEmployees;

            addModel.OrganizationalStructureFile = model.OrganizationalStructureFile;

            addModel.SpecialistsStuffingDocument = model.SpecialistsStuffingDocument;

            addModel.EmployeesSertificates = model.EmployeesSertificates;

            addModel.EmployeesResumesSentMinistry = model.EmployeesResumesSentMinistry;

            addModel.HasWorkPlanOfSpecialForces = model.HasWorkPlanOfSpecialForces;

            addModel.WorkPlanOfSpecialForces = model.WorkPlanOfSpecialForces;

            addModel.FinanceProvisionMaterial = model.FinanceProvisionMaterial;

            addModel.FinanceProvisionMaterialDocument = model.FinanceProvisionMaterialDocument;

            addModel.AmountOfFunds = model.AmountOfFunds;

            addModel.LastYearAmountOfFunds = model.LastYearAmountOfFunds;

            addModel.FundForKeepingForces = model.FundForKeepingForces;

            addModel.AmountOfSpentFund = model.AmountOfSpentFund;

            addModel.NextYearFundForKeepingForces = model.NextYearFundForKeepingForces;

            addModel.OutsourcingSpentFund = model.OutsourcingSpentFund;

            addModel.OutsourcingHasCertificates = model.OutsourcingHasCertificates;

            addModel.OutsourcingCompanySertificate = model.OutsourcingCompanySertificate;

            addModel.OutsourcingEmployees = model.OutsourcingEmployees;

            addModel.OutsourcingHasWorkPlan = model.OutsourcingHasWorkPlan;

            addModel.OutsourcingWorkPlanFile = model.OutsourcingWorkPlanFile;

            addModel.QuarterlyReportOutsourcing = model.QuarterlyReportOutsourcing;

            addModel.QuarterlyReportOutsourcingFile = model.QuarterlyReportOutsourcingFile;


            _specialForces.Add(addModel);

            return addModel.Id;
        }
        public int Update(SpecialForcesCommand model)
        {
            var specialForces = _specialForces.Find(h => h.Id == model.Id).FirstOrDefault();
            if (specialForces == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());


            var org = _organization.Find(o => o.Id == specialForces.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            


            if (model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))
            {
                if (deadline.SixthSectionDeadlineDate > DateTime.Now)
                {
                    specialForces.HasSpecialForces = model.HasSpecialForces;

                    specialForces.SpecialForcesName = model.SpecialForcesName;

                    specialForces.FormOfSpecialForces = model.FormOfSpecialForces;

                    specialForces.FullNameDirector = model.FullNameDirector;

                    specialForces.HeadPosition = model.HeadPosition;

                    specialForces.WorkPhone = model.WorkPhone;

                    specialForces.MobilePhone = model.MobilePhone;

                    specialForces.Email = model.Email;

                    specialForces.MinistryAgreedHead = model.MinistryAgreedHead;

                    specialForces.MinistryAgreedHeadDocument = model.MinistryAgreedHeadDocument;

                    specialForces.HasCharacterizingDocument = model.HasCharacterizingDocument;

                    specialForces.CharacterizingDocument = model.CharacterizingDocument;

                    specialForces.HasMinistryAgreedCharacterizingDocument = model.HasMinistryAgreedCharacterizingDocument;

                    specialForces.MinistryAgreedCharacterizingDocument = model.MinistryAgreedCharacterizingDocument;

                    specialForces.EmployeesSum = model.EmployeesSum;

                    specialForces.CentralofficeEmployees = model.CentralofficeEmployees;

                    specialForces.RegionalEmployees = model.RegionalEmployees;

                    specialForces.SubordinateEmployees = model.SubordinateEmployees;

                    specialForces.InformationSecurityEmployees = model.InformationSecurityEmployees;

                    specialForces.InformationSystemDatabaseEmployees = model.InformationSystemDatabaseEmployees;

                    specialForces.OrganizationalStructureFile = model.OrganizationalStructureFile;

                    specialForces.SpecialistsStuffingDocument = model.SpecialistsStuffingDocument;

                    specialForces.EmployeesSertificates = model.EmployeesSertificates;

                    specialForces.EmployeesResumesSentMinistry = model.EmployeesResumesSentMinistry;

                    specialForces.HasWorkPlanOfSpecialForces = model.HasWorkPlanOfSpecialForces;

                    specialForces.WorkPlanOfSpecialForces = model.WorkPlanOfSpecialForces;

                    specialForces.FinanceProvisionMaterial = model.FinanceProvisionMaterial;

                    specialForces.FinanceProvisionMaterialDocument = model.FinanceProvisionMaterialDocument;

                    specialForces.AmountOfFunds = model.AmountOfFunds;

                    specialForces.LastYearAmountOfFunds = model.LastYearAmountOfFunds;

                    specialForces.FundForKeepingForces = model.FundForKeepingForces;

                    specialForces.AmountOfSpentFund = model.AmountOfSpentFund;

                    specialForces.NextYearFundForKeepingForces = model.NextYearFundForKeepingForces;

                    specialForces.OutsourcingSpentFund = model.OutsourcingSpentFund;

                    specialForces.OutsourcingHasCertificates = model.OutsourcingHasCertificates;

                    specialForces.OutsourcingCompanySertificate = model.OutsourcingCompanySertificate;

                    specialForces.OutsourcingEmployees = model.OutsourcingEmployees;

                    specialForces.OutsourcingHasWorkPlan = model.OutsourcingHasWorkPlan;

                    specialForces.OutsourcingWorkPlanFile = model.OutsourcingWorkPlanFile;

                    specialForces.QuarterlyReportOutsourcing = model.QuarterlyReportOutsourcing;

                    specialForces.QuarterlyReportOutsourcingFile = model.QuarterlyReportOutsourcingFile;
                }
                else { throw ErrorStates.Error(UIErrors.DeadlineExpired); }
            }

            if ((model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
            {
                if (deadline.OperatorDeadlineDate > DateTime.Now)
                {
                    specialForces.ExpertComment = model.ExpertComment;
                    specialForces.ExpertExept = model.ExpertExept;
                }
                else { throw ErrorStates.Error(UIErrors.DeadlineExpired); }
            }


            _specialForces.Update(specialForces);

            return specialForces.Id;
        }
        public int Delete(SpecialForcesCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.SixthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            var specialForces = _specialForces.Find(h => h.Id == model.Id).FirstOrDefault();
            if (specialForces == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            
            var org = _organization.Find(o => o.Id == specialForces.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                throw ErrorStates.NotAllowed("permission");

            _specialForces.Remove(specialForces);

            return specialForces.Id;
        }
    }
}
