using Domain;
using Domain.Models;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add:
                    id = Add(request);
                    break;
                case Domain.Enums.EventType.Update: 
                    id = Update(request);
                    break;
                case Domain.Enums.EventType.Delete: 
                    id = Delete(request); 
                    break;
            }
            return new ConnectionCommandResult() {Id = id, IsSuccess = false };
        }

        public int Add(ConnectionCommand model)
        {
            int id = 0;

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            

            var projectConnection = _reestrProjectConnection.Find(p => p.Id == model.ParentId && p.Exist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var connection = _projectConnection.Find(p => p.ParentId == model.ParentId && p.PlatformReestrId == model.PlatformReestrId).FirstOrDefault();
            if (connection != null)
                throw ErrorStates.NotAllowed(model.PlatformReestrId.ToString());




            if ((model.UserOrgId == projectConnection.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                ProjectConnections addModel = new ProjectConnections();
                addModel.ParentId = model.ParentId;
                addModel.ConnectionType = model.ConnectionType;
                addModel.PlatformReestrId = model.PlatformReestrId;
                addModel.FilePath = model.FilePath;

                _projectConnection.Add(addModel);

                id = addModel.Id;
            }
           

            return id;
        }

        public int Update(ConnectionCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            


            var projectConnection = _reestrProjectConnection.Find(p => p.Id == model.ParentId).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectConnection == null)
                throw ErrorStates.NotAllowed(model.ParentId.ToString());

            var connection = _projectConnection.Find(p => p.Id == model.Id).FirstOrDefault();
            if (connection == null)
                throw ErrorStates.NotAllowed(model.ParentId.ToString());


            if ((model.UserOrgId == projectConnection.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                connection.ConnectionType = model.ConnectionType;
                connection.PlatformReestrId = model.PlatformReestrId;
                if(!String.IsNullOrEmpty(model.FilePath))
                    connection.FilePath = model.FilePath;

            }



            _projectConnection.Update(connection);

            return connection.Id;
        }

        public int Delete(ConnectionCommand model)
        {
            var connection = _projectConnection.Find(p => p.Id == model.Id).FirstOrDefault();
            if (connection == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _projectConnection.Remove(connection);
            return connection.Id;
        }
    }
}
