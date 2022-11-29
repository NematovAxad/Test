﻿using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Results.ReestrPassportResult;
using Domain.Models.SecondSection;
using System.Linq;
using Domain.Permission;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ProjectExpertDecisionCommandHandler : IRequestHandler<ProjectExpertDecisionCommand, ProjectExpertDecisionCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectExpertDecision, int> _projectExpertDecision;

        public ProjectExpertDecisionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectExpertDecision, int> projectExpertDecision)
        {
            _organization = organization;
            _deadline = deadline;
            _projectExpertDecision = projectExpertDecision;
        }
        public async Task<ProjectExpertDecisionCommandResult> Handle(ProjectExpertDecisionCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ProjectExpertDecisionCommandResult() { IsSuccess = true };
        }
        public void Add(ProjectExpertDecisionCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            var projectExpertDecision = _projectExpertDecision.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectExpertDecision != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            ReestrProjectExpertDecision addModel = new ReestrProjectExpertDecision();
            addModel.OrganizationId = model.OrganizationId;
            addModel.ReestrProjectId = model.ReestrProjectId;

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;
                addModel.ExpertExcept = model.ExpertExcept;
            }

            _projectExpertDecision.Add(addModel);
        }
        public void Update(ProjectExpertDecisionCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            var projectExpertDecision = _projectExpertDecision.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectExpertDecision == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());


            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if(!String.IsNullOrEmpty(model.ExpertComment))
                    projectExpertDecision.ExpertComment = model.ExpertComment;
                projectExpertDecision.ExpertExcept = model.ExpertExcept;
            }

            _projectExpertDecision.Update(projectExpertDecision);
        }

        public void Delete(ProjectExpertDecisionCommand model)
        {
            var projectExpertDecision = _projectExpertDecision.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectExpertDecision == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());
            _projectExpertDecision.Remove(projectExpertDecision);
        }
    }
}