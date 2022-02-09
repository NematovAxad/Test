using Domain.MonitoringModels.Models;
using Domain.States;
using JohaRepository;
using MediatR;
using MonitoringHandler.Commands.StructureCommands;
using MonitoringHandler.Results.StructureResults.CommandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringHandler.Handlers.StructureHandlers
{
    public class CommentCommandHandler : IRequestHandler<CommentCommand, CommentCommandResult>
    {
        private readonly IRepository<Comment, int> _comment;
        
        public CommentCommandHandler(IRepository<Comment, int> comment)
        {
            _comment = comment;
        }

        public async Task<CommentCommandResult> Handle(CommentCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new CommentCommandResult() { IsSuccess = true };
        }
        public void Add(CommentCommand model)
        {
            Comment addModel = new Comment()
            {
                Text = model.Text,
                UserId = model.UserId,
                UserRole = model.UserPermissions[0],
                Action = "commenting",
                UserName = model.UserId.ToString(),
                DateComment = DateTime.Now,
                StageId = model.StageId
            };

            _comment.Add(addModel);
        }
        public void Delete(CommentCommand model)
        {
            var c = _comment.Find(c => c.Id == model.Id).FirstOrDefault();
            if (c == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _comment.Remove(c);
        }
    }
}
