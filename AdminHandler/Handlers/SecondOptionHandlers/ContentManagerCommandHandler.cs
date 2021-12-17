using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain;
using Domain.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class ContentManagerCommandHandler : IRequestHandler<ContentManagerCommand, ContentManagerCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<ContentManager, int> _contentManager;

        public ContentManagerCommandHandler(IRepository<Organizations, int> organizations, IRepository<ContentManager, int> contentManager)
        {
            _organizations = organizations;
            _contentManager = contentManager;
        }

        public async Task<ContentManagerCommandResult> Handle(ContentManagerCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ContentManagerCommandResult() { IsSuccess = true };
        }
        public void Add(ContentManagerCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var manager = _contentManager.Find(m => m.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (manager != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            
            var filePath = FileState.AddFile("headDocs", model.File);
            ContentManager addModel = new ContentManager()
            {
                OrganizationId = model.OrganizationId,
                FullName = model.FullName,
                Position = model.Position,
                Phone = model.Phone,
                FilePath = filePath
            };
            _contentManager.Add(addModel);
        }
        public void Update(ContentManagerCommand model)
        {
            var manager = _contentManager.Find(m => m.Id == model.Id).FirstOrDefault();
            if (manager == null)
                throw ErrorStates.NotFound(model.Id.ToString());

            if(model.File!=null)
            {
                var filePath = FileState.AddFile("headDocs", model.File);
                manager.FilePath = filePath;
            }

            manager.FullName = model.FullName;
            manager.Position = model.Position;
            manager.Phone = model.Phone;
   
            _contentManager.Update(manager);
        }
        public void Delete(ContentManagerCommand model)
        {
            var manager = _contentManager.Find(m => m.Id == model.Id).FirstOrDefault();
            if (manager == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _contentManager.Remove(model.Id);
        }
    }
}
