﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiConfigs.Options
{
    internal static class Extensions
    {
        internal static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);

            return model;
        }
    }
}
