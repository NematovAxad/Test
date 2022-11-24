﻿using DocumentFormat.OpenXml.Office2013.Excel;
using Domain;
using Domain.Models;
using Domain.Models.SecondSection;
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
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Results.ReestrPassportResult;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ProjectPositionCommandHandler : IRequestHandler<ProjectPositionCommand, ProjectPositionCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectPosition, int> _projectPosition;

        public ProjectPositionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectPosition, int> projectPosition)
        {
            _organization = organization;
            _deadline = deadline;
            _projectPosition = projectPosition;
        }

        public async Task<ProjectPositionCommandResult> Handle(ProjectPositionCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ProjectPositionCommandResult() { IsSuccess = true };
        }
        public void Add(ProjectPositionCommand model)
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

            var projectPosition = _projectPosition.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectPosition != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            ReestrProjectPosition addModel = new ReestrProjectPosition();
            addModel.OrganizationId = model.OrganizationId;
            addModel.ReestrProjectId = model.ReestrProjectId;

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                addModel.ExpertExcept = model.ExpertExcept;
                if(!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;
            }
            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if(!String.IsNullOrEmpty(model.FilePath))
                    addModel.FilePath = model.FilePath;
            }
                

                _projectPosition.Add(addModel);
        }
        public void Update(ProjectPositionCommand model)
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

            var projectPosition = _projectPosition.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectPosition == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                projectPosition.ExpertExcept = model.ExpertExcept;
                if(!String.IsNullOrEmpty(model.ExpertComment))
                    projectPosition.ExpertComment = model.ExpertComment;

            }
            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if(!String.IsNullOrEmpty(model.FilePath))
                    projectPosition.FilePath = model.FilePath;
            }
               
            _projectPosition.Update(projectPosition);
        }
        public void Delete(ProjectPositionCommand model)
        {
            var projectPosition = _projectPosition.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectPosition == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            _projectPosition.Remove(projectPosition);
        }
    }
}
