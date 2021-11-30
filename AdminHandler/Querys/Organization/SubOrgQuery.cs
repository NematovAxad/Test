using AdminHandler.Results.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Organization
{
    public class SubOrgQuery:IRequest<SubOrgQueryResult>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
    }
}
