using Domain.Models.ThirdSection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Queries.ThirdSection
{
    public class OrganizationServiceRateQuery:IRequest<OrganizationServiceRateQueryResult>
    {
        public int ServiceId { get; set; }
        public int RateId { get; set; }
    }
}
