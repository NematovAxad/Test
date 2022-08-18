﻿using AdminHandler.Commands.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Models;
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
    public class DeadlineCommandHandler : IRequestHandler<DeadlineCommand, DeadlineCommandResult>
    {
        private readonly IRepository<Deadline, int> _deadline;
        IDataContext _db;
        public DeadlineCommandHandler(IRepository<Deadline, int> deadline, IDataContext db)
        {
            _deadline = deadline;
            _db = db;
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
            if (!model.UserPermissions.Any(p => p == Permissions.DEADLINE_CONTROL))
                throw ErrorStates.NotAllowed("permission");
            if (model.IsActive == true)
            {
                var list = _deadline.GetAll().ToList();
                for(int i = 0; i<list.Count; i++)
                {
                    list[i].IsActive = false;
                }

                _db.Context.Set<Deadline>().UpdateRange(list);
                _db.Context.SaveChanges();
            }
            Deadline addModel = new Deadline()
            {
                Year = model.Year,
                Quarter = model.Quarter,
                DeadlineDate = model.DeadlineDate,
                OperatorDeadlineDate = model.OperatorDeadlineDate,
                IsActive = model.IsActive,
                PingService = false
            };
            _deadline.Add(addModel);
        }
        public void Update(DeadlineCommand model)
        {
            var deadline = _deadline.Find(d => d.Id == model.Id).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.DEADLINE_CONTROL))
                throw ErrorStates.NotAllowed("permission");
            if (model.IsActive == true)
            {
                var list = _deadline.GetAll().ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].IsActive = false;
                }

                _db.Context.Set<Deadline>().UpdateRange(list);
                _db.Context.SaveChanges();
            }
            deadline.IsActive = model.IsActive;
            deadline.DeadlineDate = model.DeadlineDate;
            deadline.OperatorDeadlineDate = model.OperatorDeadlineDate;
            _deadline.Update(deadline);
        }
        public void Delete(DeadlineCommand model)
        {
            var deadline = _deadline.Find(d => d.Id == model.Id).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.DEADLINE_CONTROL))
                throw ErrorStates.NotAllowed("permission");
            _deadline.Remove(deadline);
        }
    }
}
