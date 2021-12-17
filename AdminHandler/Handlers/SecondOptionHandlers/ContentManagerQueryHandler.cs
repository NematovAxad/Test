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
            var manager = _contentManager.GetAll();
            if (request.Id != 0)
                manager = manager.Where(m => m.Id == request.Id);
            if (request.OrganizationId != 0)
                manager = manager.Where(m => m.OrganizationId == request.OrganizationId);

            ContentManagerQueryResult result = new ContentManagerQueryResult();
            result.Count = manager.Count();
            result.Data = manager.OrderBy(u => u.Id).ToList<object>();
            return result;

        }
    }
}
