using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ReesterModels
{
    public class SecondRequestQuery:IRequest<SecondRequestQueryResult>
    {
        public int Id { get; set; }
    }
}
