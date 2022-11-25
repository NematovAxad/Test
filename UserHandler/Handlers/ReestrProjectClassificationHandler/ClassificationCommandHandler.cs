using Domain.Models.SecondSection;
using Domain.Models;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Results.ReestrProjectClassificationCommandResult;
using System.Threading.Tasks;
using System.Threading;
using UserHandler.Results.ReestrProjectIdentityResult;
using Domain.States;
using UserHandler.Commands.ReestrProjectIdentityCommand;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;

namespace UserHandler.Handlers.ReestrProjectClassificationHandler
{
    public class ClassificationCommandHandler:IRequestHandler<ClassificationCommand, ClassificationCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ProjectClassifications, int> _classifications;
        private readonly IRepository<ReestrProjectClassifications, int> _projectClassification;

        public ClassificationCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ProjectClassifications, int> classifications, IRepository<ReestrProjectClassifications, int> projectClassification)
        {
            _organization = organization;
            _deadline = deadline;
            _classifications = classifications;
            _projectClassification = projectClassification;
        }

        public async Task<ClassificationCommandResult> Handle(ClassificationCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ClassificationCommandResult() { Success = true };
        }
        public void Add(ClassificationCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());



            var projectClassification = _projectClassification.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectClassification == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _classifications.Find(p => p.ParentId == model.ParentId && p.ClassificationUri == model.ClassificationUri).FirstOrDefault();
            if (identity != null)
                throw ErrorStates.NotAllowed(model.ClassificationUri.ToString());


            ProjectClassifications addModel = new ProjectClassifications();


            if (((model.UserOrgId == projectClassification.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                addModel.ParentId = model.ParentId;
                addModel.ClassificationType = model.ClassificationType;
                addModel.ClassificationUri = model.ClassificationUri;
                addModel.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _classifications.Add(addModel);
        }

        public void Update(ClassificationCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            var projectClassification = _projectClassification.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectClassification == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _classifications.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotAllowed(model.Id.ToString());


            if (((model.UserOrgId == projectClassification.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                identity.ClassificationType = model.ClassificationType;
                identity.ClassificationUri = model.ClassificationUri;
                if (!String.IsNullOrEmpty(model.FilePath))
                    identity.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _classifications.Update(identity);
        }
        public void Delete(ClassificationCommand model)
        {
            var identity = _classifications.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _classifications.Remove(identity);
        }
    }
}
