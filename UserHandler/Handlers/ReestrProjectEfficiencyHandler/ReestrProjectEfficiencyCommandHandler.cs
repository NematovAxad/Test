using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Commands.ReestrProjectEfficiencyCommand;
using UserHandler.Results.ReestrProjectClassificationResult;
using UserHandler.Results.ReestrProjectEfficiencyResult;
using System.Linq;
using Domain.Permission;
using MainInfrastructures.Migrations;
using Microsoft.EntityFrameworkCore;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain;

namespace UserHandler.Handlers.ReestrProjectEfficiencyHandler
{
    public class ReestrProjectEfficiencyCommandHandler : IRequestHandler<ReestrProjectEfficiencyCommand, ReestrProjectEfficiencyCommandResult>
    {


        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectEfficiency, int> _projectEfficiency;
        private readonly IRepository<ProjectEfficiency, int> _efficiency;

        public ReestrProjectEfficiencyCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectEfficiency, int> projectEfficiency, IRepository<ProjectEfficiency, int> efficiency)
        {
            _organization = organization;
            _deadline = deadline;
            _projectEfficiency = projectEfficiency;
            _efficiency = efficiency;
        }


        public async Task<ReestrProjectEfficiencyCommandResult> Handle(ReestrProjectEfficiencyCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ReestrProjectEfficiencyCommandResult() { Id = id, Success = true };
        }

        public int Add(ReestrProjectEfficiencyCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            var projectEfficiency = _projectEfficiency.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectEfficiency != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            ReestrProjectEfficiency addModel = new ReestrProjectEfficiency();

            addModel.OrganizationId = model.OrganizationId;
            addModel.ReestrProjectId = model.ReestrProjectId;

            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if (!String.IsNullOrEmpty(model.OrgComment))
                    addModel.OrgComment = model.OrgComment;
                addModel.Exist = model.Exist;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    addModel.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    addModel.ExceptedItems = model.ExceptedItems;
            }


            _projectEfficiency.Add(addModel);

            return addModel.Id;
        }
        public int Update(ReestrProjectEfficiencyCommand model)
        {
            var projectEfficiency = _projectEfficiency.Find(p => p.Id == model.Id).Include(mbox => mbox.Efficiencies).FirstOrDefault();
            if (projectEfficiency == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            var org = _organization.Find(o => o.Id == projectEfficiency.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            
            if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);



            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                if (!String.IsNullOrEmpty(model.OrgComment))
                    projectEfficiency.OrgComment = model.OrgComment;
                projectEfficiency.Exist = model.Exist;

                if (model.Exist == false)
                {
                    _efficiency.RemoveRange(projectEfficiency.Efficiencies);
                }
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectEfficiency.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    projectEfficiency.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    projectEfficiency.ExceptedItems = model.ExceptedItems;
            }

            if (((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
               if (model.Exist == false)
                {
                    projectEfficiency.ExpertComment = String.Empty;
                    projectEfficiency.AllItems = 0;
                    projectEfficiency.ExceptedItems = 0;
                }
            }

            _projectEfficiency.Update(projectEfficiency);

            return projectEfficiency.Id;
        }
        public int Delete(ReestrProjectEfficiencyCommand model)
        {
            var projectIdentities = _projectEfficiency.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectIdentities == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            _projectEfficiency.Remove(projectIdentities);

            return projectIdentities.Id;
        }
    }
}
