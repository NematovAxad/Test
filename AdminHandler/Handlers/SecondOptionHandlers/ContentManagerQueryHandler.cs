using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
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
    public class ContentManagerQueryHandler : IRequestHandler<ContentManagerQuery, ContentManagerQueryResult>
    {
        private readonly IRepository<ContentManager, int> _contentManager;

        public ContentManagerQueryHandler(IRepository<ContentManager, int> contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<ContentManagerQueryResult> Handle(ContentManagerQuery request, CancellationToken cancellationToken)
        {
            var manager = new ContentManager();
            if (request.Id != 0)
                manager = _contentManager.Find(m => m.Id == request.Id).FirstOrDefault();
            if (request.OrganizationId != 0)
                manager = _contentManager.Find(m => m.OrganizationId == request.OrganizationId).FirstOrDefault();

            return new ContentManagerQueryResult() {ContentManager = manager };


        }
    }
}
