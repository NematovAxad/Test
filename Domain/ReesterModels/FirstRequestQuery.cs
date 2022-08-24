using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ReesterModels
{
    public class FirstRequestQuery:IRequest<FirstRequestQueryResult>
    {
        public int OrgId { get; set; }
        public long Page { get; set; }
        public int Limit { get; set; }
    }
}
