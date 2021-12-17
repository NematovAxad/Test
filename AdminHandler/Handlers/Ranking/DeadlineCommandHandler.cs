using AdminHandler.Commands.Ranking;
using AdminHandler.Results.Ranking;
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

namespace AdminHandler.Handlers.Ranking
{
    public class DeadlineCommandHandler : IRequestHandler<DeadlineCommand, DeadlineCommandResult>
    {
        private readonly IRepository<Deadline, int> _deadline;

        public DeadlineCommandHandler(IRepository<Deadline, int> deadline)
        {
            _deadline = deadline;
        }

        public async Task<DeadlineCommandResult> Handle(DeadlineCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new DeadlineCommandResult() { IsSuccess = true };
        }
        public void Add(DeadlineCommand model)
        {
            var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline != null)
                throw ErrorStates.NotAllowed(model.Year.ToString());
            if(model.IsActive == true)
            {
                var list = _deadline.GetAll();
                foreach(var d in list)
                {
                    d.IsActive = false;
                    _deadline.Update(d);
                }
            }
            Deadline addModel = new Deadline()
            {
                Year = model.Year,
                Quarter = model.Quarter,
                DeadlineDate = model.DeadlineDate,
                IsActive = model.IsActive
            };
            _deadline.Add(addModel);
        }
        public void Update(DeadlineCommand model)
        {
            var deadline = _deadline.Find(d => d.Id == model.Id && d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            
            if (model.IsActive == true)
            {
                var list = _deadline.GetAll();
                foreach (var d in list)
                {
                    d.IsActive = false;
                    _deadline.Update(d);
                }
            }
            deadline.DeadlineDate = model.DeadlineDate;
            _deadline.Update(deadline);
        }
        public void Delete(DeadlineCommand model)
        {
            var deadline = _deadline.Find(d => d.Id == model.Id).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _deadline.Remove(deadline);
        }
    }
}
