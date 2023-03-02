using Domain;
using Domain.Models;
using Domain.Models.FirstSection;
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
using UserHandler.Commands.SecondSectionCommand;
using UserHandler.Results.SecondSectionCommandResult;
using UserHandler.Results.SecondSectionQueryResult;

namespace UserHandler.Handlers.SecondSectionHandler
{
    public class SiteFailCommentCommandHandler : IRequestHandler<SiteFailCommentCommand, SiteFailCommentCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<SiteFailComments, int> _fails;

        public SiteFailCommentCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<SiteFailComments, int> fails)
        {
            _organization = organization;
            _deadline = deadline;
            _fails = fails;
        }

        public async Task<SiteFailCommentCommandResult> Handle(SiteFailCommentCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new SiteFailCommentCommandResult() { IsSuccess = true };
        }
        public void Add(SiteFailCommentCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            var deadline = _deadline.Find(d => d.Id == model.DeadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            SiteFailComments addModel = new SiteFailComments();

            addModel.OrganizationId = org.Id;
            addModel.DeadlineId = deadline.Id;
            addModel.Website = model.Website;
            addModel.ExpertComment = model.ExpertComment;
            addModel.ScreenPath = model.ImagePath;
            

            _fails.Add(addModel);
        }


        public void Update(SiteFailCommentCommand model)
        {
            var fail = _fails.Find(f => f.Id == model.Id).FirstOrDefault();

            if(fail is null)
                throw ErrorStates.NotFound("comment not found!!!");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            fail.ScreenPath = model.ImagePath;
            fail.ExpertComment = model.ExpertComment;   

            _fails.Update(fail);
        }

        public void Delete(SiteFailCommentCommand model)
        {
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            var fail = _fails.Find(f => f.Id == model.Id).FirstOrDefault();

            if (fail is null)
                throw ErrorStates.NotFound("comment not found!!!");

            _fails.Remove(fail);
        }
    }
}
