using CoreDomain.Helpers;
using Microsoft.AspNetCore.Http;
using OpenBudgetDomain.Consts;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace CoreDomain.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly Dictionary<string, string> _languages;
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;

            _languages = new Dictionary<string, string>();
            _languages.Add(LanguageConsts.Uz, CultureHelper.UzLanguageName);
            _languages.Add(LanguageConsts.Ru, CultureHelper.RuLanguageName);
            _languages.Add(LanguageConsts.En, CultureHelper.EnLanguageName);
            _languages.Add(LanguageConsts.Kr, CultureHelper.KrLanguageName);
        }
     
        public async Task InvokeAsync(HttpContext context)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(CultureHelper.RuLanguageName);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(CultureHelper.RuLanguageName);

            var language = context.Request.Headers["Language"];
            if (!string.IsNullOrEmpty(language) && _languages.TryGetValue(language, out var cultureName))
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
                CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
                LanguageConsts.CurrentLanguage = language;
            }

            await _next(context);
        }
    }
}
