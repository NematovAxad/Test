using AdminHandler.Results.Ranking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Ranking
{
    public class FieldQuery:IRequest<FieldQueryResult>
    {
        public int Id { get; set; }
    }
}
