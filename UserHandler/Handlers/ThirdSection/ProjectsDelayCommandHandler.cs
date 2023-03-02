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
    public class ProjectsDelayCommandHandler : IRequestHandler<ProjectsDelayCommand, ProjectsDelayCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<DelaysOnProjects, int> _delaysOnProjects;

        public ProjectsDelayCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<DelaysOnProjects, int> delaysOnProjects)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _delaysOnProjects = delaysOnProjects;
        }
        public async Task<ProjectsDelayCommandResult> Handle(ProjectsDelayCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ProjectsDelayCommandResult() { IsSuccess = true };
        }
        public void Add(ProjectsDelayCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var projectDelays = _delaysOnProjects.Find(h => h.OrganizationId == model.OrganizationId && h.ProjectDocumentNumber == model.ProjectDocumentNumber).FirstOrDefault();
            if (projectDelays != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            DelaysOnProjects addModel = new DelaysOnProjects()
            {
                OrganizationId = model.OrganizationId,
                ProjectName = model.ProjectName,
                ProjectDocumentNumber = model.ProjectDocumentNumber,
                ProjectDocumentDate = model.ProjectDocumentDate,
                ProjectStatus = model.ProjectStatus,
                ProjectApplyingMechanism = model.ProjectApplyingMechanism,
                ProjectApplyingDate = model.ProjectApplyingDate,
                ProjectFinancingSource = model.ProjectFinancingSource,
                ProjectPrice = model.ProjectPrice,
                ProvidedFund = model.ProvidedFund
            };
            _delaysOnProjects.Add(addModel);
        }
        public void Update(ProjectsDelayCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            var projectDelays = _delaysOnProjects.Find(h => h.OrganizationId == model.OrganizationId && h.ProjectDocumentNumber == model.ProjectDocumentNumber).FirstOrDefault();
            if (projectDelays == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            projectDelays.OrganizationId = model.OrganizationId;
            projectDelays.ProjectName = model.ProjectName;
            projectDelays.ProjectDocumentNumber = model.ProjectDocumentNumber;
            projectDelays.ProjectDocumentDate = model.ProjectDocumentDate;
            projectDelays.ProjectStatus = model.ProjectStatus;
            projectDelays.ProjectApplyingMechanism = model.ProjectApplyingMechanism;
            projectDelays.ProjectApplyingDate = model.ProjectApplyingDate;
            projectDelays.ProjectFinancingSource = model.ProjectFinancingSource;
            projectDelays.ProjectPrice = model.ProjectPrice;
            projectDelays.ProvidedFund = model.ProvidedFund;

            _delaysOnProjects.Update(projectDelays);
        }
        public void Delete(ProjectsDelayCommand model)
        {
            var projectDelays = _delaysOnProjects.Find(h => h.Id == model.Id).FirstOrDefault();
            if (projectDelays == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            var org = _organization.Find(o => o.Id == projectDelays.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            _delaysOnProjects.Remove(projectDelays);
        }
    }
}
