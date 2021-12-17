using AdminHandler.Results.Ranking;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Ranking
{
    public class DeadlineCommand:IRequest<DeadlineCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int Year { get; set; }
        public Quarters Quarter { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool IsActive { get; set; }
    }
}
