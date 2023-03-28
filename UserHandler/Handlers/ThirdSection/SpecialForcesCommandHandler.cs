﻿using Domain;
using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.FirstSection;
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
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new SpecialForcesCommandResult() { IsSuccess = true };
        }
        public void Add(SpecialForcesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            
            var specialForces = _specialForces.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (specialForces != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");



            OrganizationIctSpecialForces addModel = new OrganizationIctSpecialForces()
            {
                OrganizationId = model.OrganizationId,
                HasSpecialForces = model.HasSpecialForces,
                SpecialForcesName = model.SpecialForcesName,
                FormOfSpecialForces = model.FormOfSpecialForces,
                FullNameDirector = model.FullNameDirector,
                HeadPosition = model.HeadPosition,
                WorkPhone = model.WorkPhone,
                MobilePhone = model.MobilePhone,
                Email = model.Email,
                MinistryAgreedHead = model.MinistryAgreedHead,
                HasCharacterizingDocument = model.HasCharacterizingDocument,
                HasMinistryAgreedCharacterizingDocument = model.HasMinistryAgreedCharacterizingDocument,
                EmployeesSum = model.EmployeesSum,
                CentralofficeEmployees = model.CentralofficeEmployees,
                RegionalEmployees = model.RegionalEmployees,
                SubordinateEmployees = model.SubordinateEmployees,
                InformationSecurityEmployees = model.InformationSecurityEmployees,
                InformationSystemDatabaseEmployees = model.InformationSystemDatabaseEmployees,
                EmployeesResumesSentMinistry = model.EmployeesResumesSentMinistry,
                HasWorkPlanOfSpecialForces = model.HasWorkPlanOfSpecialForces,
                FinanceProvisionMaterial = model.FinanceProvisionMaterial,
                AmountOfFunds = model.AmountOfFunds,
                LastYearAmountOfFunds = model.LastYearAmountOfFunds,
                FundForKeepingForces = model.FundForKeepingForces,
                AmountOfSpentFund = model.AmountOfSpentFund,
                NextYearFundForKeepingForces = model.NextYearFundForKeepingForces,
                OutsourcingSpentFund = model.OutsourcingSpentFund,
                OutsourcingHasCertificates = model.OutsourcingHasCertificates,
                OutsourcingEmployees = model.OutsourcingEmployees,
                OutsourcingHasWorkPlan = model.OutsourcingHasWorkPlan,
                QuarterlyReportOutsourcing = model.QuarterlyReportOutsourcing
            };


            addModel.CharacterizingDocument = model.CharacterizingDocumentPath;
            addModel.MinistryAgreedCharacterizingDocument = model.MinistryAgreedCharacterizingDocumentPath;
            addModel.OrganizationalStructureFile = model.OrganizationalStructureFilePath;
            addModel.SpecialistsStuffingDocument = model.SpecialistsStuffingDocumentPath;
            addModel.EmployeesSertificates = model.EmployeesSertificatesPath;
            addModel.WorkPlanOfSpecialForces = model.WorkPlanOfSpecialForcesPath;
            
            _specialForces.Add(addModel);
        }
        public void Update(SpecialForcesCommand model)
        {
            var specialForces = _specialForces.Find(h => h.Id == model.Id).FirstOrDefault();
            if (specialForces == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == specialForces.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");

            specialForces.HasSpecialForces = model.HasSpecialForces;
            specialForces.SpecialForcesName = model.SpecialForcesName;
            specialForces.FormOfSpecialForces = model.FormOfSpecialForces;
            specialForces.FullNameDirector = model.FullNameDirector;
            specialForces.HeadPosition = model.HeadPosition;
            specialForces.WorkPhone = model.WorkPhone;
            specialForces.MobilePhone = model.MobilePhone;
            specialForces.Email = model.Email;
            specialForces.MinistryAgreedHead = model.MinistryAgreedHead;
            specialForces.HasCharacterizingDocument = model.HasCharacterizingDocument;
            specialForces.HasMinistryAgreedCharacterizingDocument = model.HasMinistryAgreedCharacterizingDocument;
            specialForces.EmployeesSum = model.EmployeesSum;
            specialForces.CentralofficeEmployees = model.CentralofficeEmployees;
            specialForces.RegionalEmployees = model.RegionalEmployees;
            specialForces.SubordinateEmployees = model.SubordinateEmployees;
            specialForces.InformationSecurityEmployees = model.InformationSecurityEmployees;
            specialForces.InformationSystemDatabaseEmployees = model.InformationSystemDatabaseEmployees;
            specialForces.EmployeesResumesSentMinistry = model.EmployeesResumesSentMinistry;
            specialForces.HasWorkPlanOfSpecialForces = model.HasWorkPlanOfSpecialForces;
            specialForces.FinanceProvisionMaterial = model.FinanceProvisionMaterial;
            specialForces.AmountOfFunds = model.AmountOfFunds;
            specialForces.LastYearAmountOfFunds = model.LastYearAmountOfFunds;
            specialForces.FundForKeepingForces = model.FundForKeepingForces;
            specialForces.AmountOfSpentFund = model.AmountOfSpentFund;
            specialForces.NextYearFundForKeepingForces = model.NextYearFundForKeepingForces;
            specialForces.OutsourcingSpentFund = model.OutsourcingSpentFund;
            specialForces.OutsourcingHasCertificates = model.OutsourcingHasCertificates;
            specialForces.OutsourcingEmployees = model.OutsourcingEmployees;
            specialForces.OutsourcingHasWorkPlan = model.OutsourcingHasWorkPlan;
            specialForces.QuarterlyReportOutsourcing = model.QuarterlyReportOutsourcing;

            specialForces.CharacterizingDocument = model.CharacterizingDocumentPath;
            specialForces.MinistryAgreedCharacterizingDocument = model.MinistryAgreedCharacterizingDocumentPath;
            specialForces.OrganizationalStructureFile = model.OrganizationalStructureFilePath;
            specialForces.SpecialistsStuffingDocument = model.SpecialistsStuffingDocumentPath;
            specialForces.EmployeesSertificates = model.EmployeesSertificatesPath;
            specialForces.WorkPlanOfSpecialForces = model.WorkPlanOfSpecialForcesPath;

            _specialForces.Update(specialForces);
        }
        public void Delete(SpecialForcesCommand model)
        {
            var specialForces = _specialForces.Find(h => h.Id == model.Id).FirstOrDefault();
            if (specialForces == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == specialForces.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            _specialForces.Remove(specialForces);
        }
    }
}
