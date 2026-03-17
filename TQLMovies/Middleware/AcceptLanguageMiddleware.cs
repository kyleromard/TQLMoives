using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TQLMovies.Localization;

namespace TQLMovies.Middleware
{
    public class AcceptLanguageMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly string[] SupportedLanguages = { "en", "it", "ru" };

        public AcceptLanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, LanguageContext languageContext)
        {
            var header = context.Request.Headers["Accept-Language"].FirstOrDefault();

            var language = ResolveLanguage(header);

            languageContext.Language = language;

            await _next(context);
        }

        private static string ResolveLanguage(string? header)
        {
            if (string.IsNullOrWhiteSpace(header))
            {
                return "en";
            }

            var value = header.Trim().ToLowerInvariant();

            if (SupportedLanguages.Contains(value, StringComparer.OrdinalIgnoreCase))
            {
                return value;
            }

            return "en";
        }
    }
}

