﻿using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Results.ReestrPassportResult;
using Domain.States;
using System.Linq;
using Domain.Permission;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ProjectCyberSecurityExpertDecisionCommandHandler : IRequestHandler<ProjectCyberSecurityExpertDecisionCommand, ProjectCyberSecurityExpertDecisionCommandResult>
    {

        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectCyberSecurityExpertDecision, int> _projectCyberSecurityExpertDecision;

        public ProjectCyberSecurityExpertDecisionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectCyberSecurityExpertDecision, int> projectCyberSecurityExpertDecision)
        {
            _organization = organization;
            _deadline = deadline;
            _projectCyberSecurityExpertDecision = projectCyberSecurityExpertDecision;
        }

        public async Task<ProjectCyberSecurityExpertDecisionCommandResult> Handle(ProjectCyberSecurityExpertDecisionCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ProjectCyberSecurityExpertDecisionCommandResult() {Id = id, IsSuccess = true };
        }
        public int Add(ProjectCyberSecurityExpertDecisionCommand model)
        {
            int id = 0;

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            
            

            var projectExpertDecision = _projectCyberSecurityExpertDecision.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectExpertDecision != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {

                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);


                ReestrProjectCyberSecurityExpertDecision addModel = new ReestrProjectCyberSecurityExpertDecision();
                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                addModel.Exist = model.Exist;
                if (!String.IsNullOrEmpty(model.FilePath))
                    addModel.FilePath = model.FilePath;

                _projectCyberSecurityExpertDecision.Add(addModel);

                id = addModel.Id;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                ReestrProjectCyberSecurityExpertDecision addModel = new ReestrProjectCyberSecurityExpertDecision();
                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                addModel.Exist = model.Exist;

                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;
                addModel.ExpertExcept = model.ExpertExcept;

                _projectCyberSecurityExpertDecision.Add(addModel);

                id = addModel.Id;
            }

           

            return id;
        }
        public int Update(ProjectCyberSecurityExpertDecisionCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            

            

            var projectExpertDecision = _projectCyberSecurityExpertDecision.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectExpertDecision == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                projectExpertDecision.Exist = model.Exist;
                if (!String.IsNullOrEmpty(model.FilePath))
                    projectExpertDecision.FilePath = model.FilePath;
                projectExpertDecision.OrgComment = model.OrgComment;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectExpertDecision.ExpertComment = model.ExpertComment;
                projectExpertDecision.ExpertExcept = model.ExpertExcept;
            }

            _projectCyberSecurityExpertDecision.Update(projectExpertDecision);

            return projectExpertDecision.Id;
        }

        public int Delete(ProjectCyberSecurityExpertDecisionCommand model)
        {
            var projectExpertDecision = _projectCyberSecurityExpertDecision.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectExpertDecision == null)
                throw ErrorStates.NotFound(model.ReestrProjectId.ToString());
            _projectCyberSecurityExpertDecision.Remove(projectExpertDecision);

            return projectExpertDecision.Id;
        }
    }
}
