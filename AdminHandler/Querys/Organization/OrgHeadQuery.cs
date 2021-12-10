using AdminHandler.Results.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Organization
{
    public class OrgHeadQuery:IRequest<OrgHeadQueryResult>
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
    }
}
