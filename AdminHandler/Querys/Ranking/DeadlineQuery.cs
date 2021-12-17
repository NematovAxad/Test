using AdminHandler.Results.Ranking;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Ranking
{
    public class DeadlineQuery:IRequest<DeadlineQueryResult>
    {
        public int Year { get; set; }
        public Quarters Quarter { get; set; }
        public bool IsActive { get; set; }
    }
}
