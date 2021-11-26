using AdminHandler.Results.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Organization
{
    public class OrgQuery:IRequest<OrgQueryResult>
    {
        public int OrgId { get; set; }
    }
}
