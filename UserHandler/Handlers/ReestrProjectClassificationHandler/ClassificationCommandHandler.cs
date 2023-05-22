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
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain;

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
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ClassificationCommandResult() {Id = id, Success = true };
        }
        public int Add(ClassificationCommand model)
        {
            int id = 0;

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            



            var projectClassification = _projectClassification.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectClassification == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _classifications.Find(p => p.ParentId == model.ParentId && p.ClassificationUri == model.ClassificationUri).FirstOrDefault();
            if (identity != null)
                throw ErrorStates.NotAllowed(model.ClassificationUri.ToString());


            


            if ((model.UserOrgId == projectClassification.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);


                ProjectClassifications addModel = new ProjectClassifications();
                addModel.ParentId = model.ParentId;
                addModel.ClassificationType = model.ClassificationType;
                addModel.ClassificationUri = model.ClassificationUri;
                addModel.FilePath = model.FilePath;

                _classifications.Add(addModel);
                id = addModel.Id;
            }
            



            

            return id;
        }

        public int Update(ClassificationCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

          


            var projectClassification = _projectClassification.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectClassification == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var identity = _classifications.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotAllowed(model.Id.ToString());


            if ((model.UserOrgId == projectClassification.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                identity.ClassificationType = model.ClassificationType;
                identity.ClassificationUri = model.ClassificationUri;
                if (!String.IsNullOrEmpty(model.FilePath))
                    identity.FilePath = model.FilePath;
            }
            



            _classifications.Update(identity);

            return identity.Id;
        }
        public int Delete(ClassificationCommand model)
        {
            var identity = _classifications.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _classifications.Remove(identity);

            return identity.Id;
        }
    }
}
