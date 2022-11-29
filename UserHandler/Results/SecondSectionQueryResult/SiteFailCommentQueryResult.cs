using Domain.Models.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.SecondSectionQueryResult
{
    public class SiteFailCommentQueryResult
    {
        public int Count { get; set; }
        public List<SiteFailComments> Data { get; set; }
    }
}
