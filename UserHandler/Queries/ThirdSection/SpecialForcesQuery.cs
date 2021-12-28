﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Queries.ThirdSection
{
    public class SpecialForcesQuery:IRequest<SpecialForcesQueryResult>
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
    }
}
