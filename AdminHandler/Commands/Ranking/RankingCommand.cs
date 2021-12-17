using AdminHandler.Results.Ranking;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Ranking
{
    public class RankingCommand:IRequest<RankingCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int Year { get; set; }
        public Quarters Quarter { get; set; }
        public int FieldId { get; set; }
        public double Rank { get; set; }
        public bool IsException { get; set; }
    }
}
