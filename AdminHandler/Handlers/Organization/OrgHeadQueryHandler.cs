using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
using Domain.Models.FirstSection;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Organization
{
    public class OrgHeadQueryHandler : IRequestHandler<OrgHeadQuery, OrgHeadQueryResult>
    {
        private readonly IRepository<ReplacerOrgHead, int> _replacerOrgHead;

        public OrgHeadQueryHandler(IRepository<ReplacerOrgHead, int> replacerOrgHead)
        {
            _replacerOrgHead = replacerOrgHead;
        }

        public async Task<OrgHeadQueryResult> Handle(OrgHeadQuery request, CancellationToken cancellationToken)
        {
            var head = _replacerOrgHead.GetAll();
            if (request.Id != 0)
            {
                head = head.Where(o => o.Id == request.Id);
            }
               
            if (request.OrgId != 0)
            {
                head = head.Where(o => o.OrganizationId == request.OrgId);
            }

            OrgHeadQueryResult result = new OrgHeadQueryResult();
            result.Count = head.Count();
            result.Data = head.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
