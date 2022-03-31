
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OpenDataModels
{
    public class OpenDataQuery:IRequest<OpenDataQueryResult>
    {
        public int OrgId { get; set; }
    }
}
