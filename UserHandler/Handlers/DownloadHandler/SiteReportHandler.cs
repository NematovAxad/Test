using Domain.Models;
using Domain.Models.Models;
using Domain.Models.SecondSection;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Queries.DownloadQuery;
using UserHandler.Results.DownloadResult;

namespace UserHandler.Handlers.DownloadHandler
{
    public class SiteReportHandler : IRequestHandler<SiteReportQuery, SiteReportResult>
    {
        IRepository<WebSiteRequirements, int> _siteRequirements;
        IRepository<Organizations, int> _organizations;

        public SiteReportHandler(IRepository<WebSiteRequirements, int> siteRequirements, IRepository<Organizations, int> organizations)
        {
            _siteRequirements = siteRequirements;
            _organizations = organizations;
        }

        public async Task<SiteReportResult> Handle(SiteReportQuery request, CancellationToken cancellationToken)
        {
            var result = new SiteReportResult {Items = new List<SiteReportResultModel>() };

            var list = _siteRequirements.GetAll();
            var orgList = _organizations.GetAll().ToList();

            if(request.OrgId!=0)
            {
                orgList = orgList.Where(o => o.Id == request.OrgId).ToList();
            }

            foreach(var o in orgList)
            {
                var listForOrg = list.Where(m => m.OrganizationId == o.Id).OrderBy(m => m.Number).ToList();
                SiteReportResultModel item = new SiteReportResultModel();
                
                item.Count = result.Items.Count() + 1;
                item.OrgName = o.ShortName;
                item.Site = o.WebSite;
                if (listForOrg.Count()>0)
                {
                    item.Requirement1 = listForOrg[0].RequirementStatus.ToString();
                    item.Comment1 = listForOrg[0].Comment;
                    item.Requirement2 = listForOrg[1].RequirementStatus.ToString();
                    item.Comment2 = listForOrg[1].Comment;
                    item.Requirement3 = listForOrg[2].RequirementStatus.ToString();
                    item.Comment3 = listForOrg[2].Comment;
                    item.Requirement4 = listForOrg[3].RequirementStatus.ToString();
                    item.Comment4 = listForOrg[3].Comment;
                    item.Requirement5 = listForOrg[4].RequirementStatus.ToString();
                    item.Comment5 = listForOrg[4].Comment;
                    item.Requirement6 = listForOrg[5].RequirementStatus.ToString();
                    item.Comment6 = listForOrg[5].Comment;
                    item.Requirement7 = listForOrg[6].RequirementStatus.ToString();
                    item.Comment7 = listForOrg[6].Comment;
                    item.Requirement8 = listForOrg[7].RequirementStatus.ToString();
                    item.Comment8 = listForOrg[7].Comment;
                    item.Requirement9 = listForOrg[8].RequirementStatus.ToString();
                    item.Comment9 = listForOrg[8].Comment;
                    item.Requirement10 = listForOrg[9].RequirementStatus.ToString();
                    item.Comment10 = listForOrg[9].Comment;
                    item.Requirement11 = listForOrg[10].RequirementStatus.ToString();
                    item.Comment11 = listForOrg[10].Comment;
                    item.Requirement12 = listForOrg[11].RequirementStatus.ToString();
                    item.Comment12 = listForOrg[11].Comment;
                    item.Requirement13 = listForOrg[12].RequirementStatus.ToString();
                    item.Comment13 = listForOrg[12].Comment;
                    item.Requirement14 = listForOrg[13].RequirementStatus.ToString();
                    item.Comment14 = listForOrg[13].Comment;
                    item.Requirement15 = listForOrg[14].RequirementStatus.ToString();
                    item.Comment15 = listForOrg[14].Comment;
                    item.Requirement16 = listForOrg[15].RequirementStatus.ToString();
                    item.Comment16 = listForOrg[15].Comment;
                    item.Requirement17 = listForOrg[16].RequirementStatus.ToString();
                    item.Comment17 = listForOrg[16].Comment;
                    item.Requirement18 = listForOrg[17].RequirementStatus.ToString();
                    item.Comment18 = listForOrg[17].Comment;
                    item.Requirement19 = listForOrg[18].RequirementStatus.ToString();
                    item.Comment19 = listForOrg[18].Comment;
                    item.Requirement20 = listForOrg[19].RequirementStatus.ToString();
                    item.Comment20 = listForOrg[19].Comment;
                    item.Requirement21 = listForOrg[20].RequirementStatus.ToString();
                    item.Comment21 = listForOrg[20].Comment;
                    item.Requirement22 = listForOrg[21].RequirementStatus.ToString();
                    item.Comment22 = listForOrg[21].Comment;
                    item.Requirement23 = listForOrg[22].RequirementStatus.ToString();
                    item.Comment23 = listForOrg[22].Comment;
                    item.Requirement24 = listForOrg[23].RequirementStatus.ToString();
                    item.Comment24 = listForOrg[23].Comment;
                    item.Requirement25 = listForOrg[24].RequirementStatus.ToString();
                    item.Comment25 = listForOrg[24].Comment;
                    item.Requirement26 = listForOrg[25].RequirementStatus.ToString();
                    item.Comment26 = listForOrg[25].Comment;
                    item.Requirement27 = listForOrg[26].RequirementStatus.ToString();
                    item.Comment27 = listForOrg[26].Comment;
                    item.Requirement28 = listForOrg[27].RequirementStatus.ToString();
                    item.Comment28 = listForOrg[27].Comment;
                    item.Requirement29 = listForOrg[28].RequirementStatus.ToString();
                    item.Comment29 = listForOrg[28].Comment;
                    item.Requirement30 = listForOrg[29].RequirementStatus.ToString();
                    item.Comment30 = listForOrg[29].Comment;
                    item.Requirement31 = listForOrg[30].RequirementStatus.ToString();
                    item.Comment31 = listForOrg[30].Comment;
                    item.Requirement32 = listForOrg[31].RequirementStatus.ToString();
                    item.Comment32 = listForOrg[31].Comment;
                    item.Requirement33 = listForOrg[32].RequirementStatus.ToString();
                    item.Comment33 = listForOrg[32].Comment;
                    item.Requirement34 = listForOrg[33].RequirementStatus.ToString();
                    item.Comment34 = listForOrg[33].Comment;
                    item.Requirement35 = listForOrg[34].RequirementStatus.ToString();
                    item.Comment35 = listForOrg[34].Comment;
                    item.Requirement36 = listForOrg[35].RequirementStatus.ToString();
                    item.Comment36 = listForOrg[35].Comment;
                    item.Requirement37 = listForOrg[36].RequirementStatus.ToString();
                    item.Comment37 = listForOrg[36].Comment;
                    item.Requirement38 = listForOrg[37].RequirementStatus.ToString();
                    item.Comment38 = listForOrg[37].Comment;
                    item.Requirement39 = listForOrg[38].RequirementStatus.ToString();
                    item.Comment39 = listForOrg[38].Comment;
                    item.Requirement40 = listForOrg[39].RequirementStatus.ToString();
                    item.Comment40 = listForOrg[39].Comment;
                    item.Requirement41 = listForOrg[40].RequirementStatus.ToString();
                    item.Comment41 = listForOrg[40].Comment;
                    item.Requirement42 = listForOrg[41].RequirementStatus.ToString();
                    item.Comment42 = listForOrg[41].Comment;
                    item.Requirement43 = listForOrg[42].RequirementStatus.ToString();
                    item.Comment43 = listForOrg[42].Comment;
                    item.Requirement44 = listForOrg[43].RequirementStatus.ToString();
                    item.Comment44 = listForOrg[43].Comment;
                    item.Requirement45 = listForOrg[44].RequirementStatus.ToString();
                    item.Comment45 = listForOrg[44].Comment;
                }

                result.Items.Add(item);
            }
            return result;
        }
    }
}
