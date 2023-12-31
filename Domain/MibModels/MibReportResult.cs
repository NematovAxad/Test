﻿using Domain.Models.MibModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain.MibModels
{
    public class MibReportResult
    {
        public List<MibReport> Data { get; set; }

        public DateTime LastUpdate { get; set; } 

        public double SuccessRate { get; set; }
    }
}
