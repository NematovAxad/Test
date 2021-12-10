using AdminHandler.Results.Region;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Commands.Region
{
    public class RegionCommand:IRequest<RegionCommandResult>
    {
        public EventType EventType { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public bool IsParent { get; set; }
    }
}
