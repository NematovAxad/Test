using Domain.Models;
using Domain.Models.SecondSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrPassportCommands;
using UserHandler.Results.ReestrPassportResult;

namespace UserHandler.Handlers.ReestrPassportHandler
{
    public class ConnectionCommandHandler : IRequestHandler<ConnectionCommand, ConnectionCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ProjectConnections, int> _projectConnection;
        private readonly IRepository<ReestrProjectConnection, int> _reestrProjectConnection;

        public ConnectionCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ProjectConnections, int> projectConnection, IRepository<ReestrProjectConnection, int> reestrProjectConnection)
        {
            _organization = organization;
            _deadline = deadline;
            _projectConnection = projectConnection;
            _reestrProjectConnection = reestrProjectConnection;
        }

        public async Task<ConnectionCommandResult> Handle(ConnectionCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new ConnectionCommandResult() { IsSuccess = true };
        }

        public void Add(ConnectionCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());



            var projectConnection = _reestrProjectConnection.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var connection = _projectConnection.Find(p => p.ParentId == model.ParentId && p.PlatformReestrId == model.PlatformReestrId).FirstOrDefault();
            if (connection != null)
                throw ErrorStates.NotAllowed(model.PlatformReestrId.ToString());


            ProjectConnections addModel = new ProjectConnections();


            if (((model.UserOrgId == projectConnection.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                addModel.ParentId = model.ParentId;
                addModel.ConnectionType = model.ConnectionType;
                addModel.PlatformReestrId = model.PlatformReestrId;
                addModel.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _projectConnection.Add(addModel);
        }

        public void Update(ConnectionCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            var projectConnection = _reestrProjectConnection.Find(p => p.Id == model.ParentId).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotAllowed(model.ParentId.ToString());

            var connection = _projectConnection.Find(p => p.Id == model.Id).FirstOrDefault();
            if (connection == null)
                throw ErrorStates.NotAllowed(model.ParentId.ToString());


            if (((model.UserOrgId == projectConnection.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                connection.ConnectionType = model.ConnectionType;
                connection.PlatformReestrId = model.PlatformReestrId;
                if(!String.IsNullOrEmpty(model.FilePath))
                    connection.FilePath = model.FilePath;

            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _projectConnection.Update(connection);
        }

        public void Delete(ConnectionCommand model)
        {
            var connection = _projectConnection.Find(p => p.PlatformReestrId == model.PlatformReestrId).FirstOrDefault();
            if (connection == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _projectConnection.Remove(connection);
        }
    }
}
