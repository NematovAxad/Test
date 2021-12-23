﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Report;
using UserHandler.Queries.DownloadQuery;
using ClosedXML.Excel;
using CoreResult.ResponseCores;
using AdminHandler.Results.Ranking;
using AdminHandler.Querys.Ranking;

namespace UserApi.Controllers
{
    [Route("apiUser/[controller]/[action]")]
    public class Statistics : Controller
    {
        IMediator _mediator;
        public Statistics(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> DownloadExportReport([FromQuery] ExportReportQuery query)
        {
            try
            {
                var data = await _mediator.Send(query);
                

                var path = Directory.GetCurrentDirectory();
                path = Path.Combine(path, "Templates", "templateExport.xlsx");

                var template = new XLTemplate(path);

                var variable = new
                {
                    Items = data.Item
                };


                template.AddVariable(variable);
                template.Generate();
           
                Stream stream = new MemoryStream();

                template.SaveAs(stream);

                stream.Flush();
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orgreport.xlsx");
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
        [HttpGet]
        public async Task<ActionResult<ReportBySpheresResult>> ReportBySphere([FromQuery] int deadlineId, int organizationId)
        {
            try
            {
                ReportBySpheresQuery model = new ReportBySpheresQuery()
                {
                    DeadlineId = deadlineId,
                    OrganizationId = organizationId
                };

                var result = await _mediator.Send<ReportBySpheresResult>(model);
                return result;
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}