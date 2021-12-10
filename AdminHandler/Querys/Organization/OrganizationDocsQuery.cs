using AdminHandler.Results.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Organization
{
    public class OrganizationDocsQuery:IRequest<OrganizationDocsQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
