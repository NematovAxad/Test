using AdminHandler.Results.Region;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Region
{
    public class RegionQuery:IRequest<RegionQueryResult>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public bool IsParent { get; set; }
    }
}
