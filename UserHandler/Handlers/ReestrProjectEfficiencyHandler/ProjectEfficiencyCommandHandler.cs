using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UserHandler.Commands.ReestrProjectClassificationCommand;
using UserHandler.Commands.ReestrProjectEfficiencyCommand;
using UserHandler.Results.ReestrProjectClassificationCommandResult;
using UserHandler.Results.ReestrProjectEfficiencyResult;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;

namespace UserHandler.Handlers.ReestrProjectEfficiencyHandler
{
    public class ProjectEfficiencyCommandHandler:IRequestHandler<ProjectEfficiencyCommand, ProjectEfficiencyCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ProjectEfficiency, int> _efficiency;
        private readonly IRepository<ReestrProjectEfficiency, int> _projectEfficiency;

        public ProjectEfficiencyCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ProjectEfficiency, int> efficiency, IRepository<ReestrProjectEfficiency, int> projectEfficiency)
        {
            _organization = organization;
            _deadline = deadline;
            _efficiency = efficiency;
            _projectEfficiency = projectEfficiency;
        }

        public async Task<ProjectEfficiencyCommandResult> Handle(ProjectEfficiencyCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ProjectEfficiencyCommandResult() { Id = id, Success = true };
        }
        public int Add(ProjectEfficiencyCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());



            var projectEfficiency = _projectEfficiency.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectEfficiency == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var efficincy = _efficiency.Find(p => p.ParentId == model.ParentId && p.EfficiencyType == model.EfficiencyType).FirstOrDefault();
            if (efficincy != null)
                throw ErrorStates.NotAllowed(model.EfficiencyType.ToString());


            ProjectEfficiency addModel = new ProjectEfficiency();


            if (((model.UserOrgId == projectEfficiency.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                addModel.ParentId = model.ParentId;
                addModel.EfficiencyType = model.EfficiencyType;
                addModel.OrgComment = model.OrgComment;
                addModel.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _efficiency.Add(addModel);

            return addModel.Id;
        }

        public int Update(ProjectEfficiencyCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            var projectEfficiency = _projectEfficiency.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectEfficiency == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var efficiency = _efficiency.Find(p => p.Id == model.Id).FirstOrDefault();
            if (efficiency == null)
                throw ErrorStates.NotAllowed(model.Id.ToString());


            if (((model.UserOrgId == projectEfficiency.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                efficiency.EfficiencyType = model.EfficiencyType;
                efficiency.OrgComment = model.OrgComment;
                if (!String.IsNullOrEmpty(model.FilePath))
                    efficiency.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _efficiency.Update(efficiency);

            return efficiency.Id;
        }
        public int Delete(ProjectEfficiencyCommand model)
        {
            var identity = _efficiency.Find(p => p.Id == model.Id).FirstOrDefault();
            if (identity == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _efficiency.Remove(identity);

            return identity.Id;
        }
    }
}
