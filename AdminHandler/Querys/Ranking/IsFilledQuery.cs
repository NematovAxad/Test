﻿using AdminHandler.Results.Ranking;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Querys.Ranking
{
    public class IsFilledQuery:IRequest<IsFilledQueryResult>
    {
        public int OrganizationId { get; set; }
        public int Year { get; set; }
        public Quarters Quarter { get; set; }
        public int SphereId { get; set; }
        public int FieldId { get; set; }
    }
}
