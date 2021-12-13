using AdminHandler.Querys.SecondOptionQueries;
using AdminHandler.Results.SecondOptionResults;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
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

        public Task<ContentManagerQueryResult> Handle(ContentManagerQuery request, CancellationToken cancellationToken)
        {
            return null;



        }
    }
}
