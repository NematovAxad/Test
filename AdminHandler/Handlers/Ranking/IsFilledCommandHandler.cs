using AdminHandler.Commands.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.Permission;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Ranking
{
    public class IsFilledCommandHandler : IRequestHandler<IsFilledCommand, IsFilledCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<IsFilledTable, int> _isFilled;
        private readonly IRepository<Sphere, int> _sphere;
        private readonly IRepository<Field, int> _field;
        private readonly IDataContext _db;

        public IsFilledCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<IsFilledTable, int> isFilled, IRepository<Sphere, int> sphere, IRepository<Field, int> field, IDataContext db)
        {
            _organization = organization;
            _deadline = deadline;
            _isFilled = isFilled;
            _sphere = sphere;
            _field = field;
            _db = db;
        }
        public async Task<IsFilledCommandResult> Handle(IsFilledCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new IsFilledCommandResult() { IsSuccess = true };
        }
        public void Add(IsFilledCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline == null || deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(model.Quarter.ToString());

            var field = _field.Find(r => r.Id == model.FieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

            var isFilled = _isFilled.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).FirstOrDefault();
            if (isFilled != null)
                throw ErrorStates.NotAllowed("ranking as filled " + model.OrganizationId.ToString() + " for " + model.Quarter + " quartetr!");

            if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");
            IsFilledTable addModel = new IsFilledTable()
            {
                OrganizationId = model.OrganizationId,
                Year = model.Year,
                Quarter = model.Quarter,
                IsFilled = model.IsFilled,
                SphereId = model.SphereId,
                FieldId = model.FieldId,
                Comment = model.Comment
            };
            _isFilled.Add(addModel);
        }
        public void Update(IsFilledCommand model)
        {
            var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline == null || deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(model.Quarter.ToString());

            var field = _field.Find(r => r.Id == model.FieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

            var isFilled = _isFilled.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).FirstOrDefault();
            if (isFilled == null)
                throw ErrorStates.NotAllowed("ranking as filled " + model.OrganizationId.ToString() + " for " + model.Quarter + " quartetr!");

            isFilled.IsFilled = model.IsFilled;
            isFilled.Comment = model.Comment;
            _isFilled.Update(isFilled);
        }
        public void Delete(IsFilledCommand model)
        {
            var isFilled = _isFilled.Find(r => r.Id == model.Id).FirstOrDefault();
            if (isFilled == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _isFilled.Remove(isFilled);
        }
    }
}
