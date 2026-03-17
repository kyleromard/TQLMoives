using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;
using TQLMovies.Localization;

namespace TQLMovies.Services
{
    public class LanguageLocalizer : IStringLocalizer
    {
        private readonly LanguageContext _languageContext;

        private readonly Dictionary<string, Dictionary<string, string>> _translations;

        public const string HorrorKey = "horror";
        public const string ComedyKey = "comedy";
        public const string ActionKey = "action";

        public LanguageLocalizer(LanguageContext languageContext)
        {
            _languageContext = languageContext;

            _translations = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "en", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { HorrorKey, "Horror" },
                        { ComedyKey, "Comedy" },
                        { ActionKey, "Action" }
                    }
                },
                {
                    "it", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { HorrorKey, "Orrore" },
                        { ComedyKey, "Commedia" },
                        { ActionKey, "Azione" }
                    }
                },
                {
                    "ru", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { HorrorKey, "Ужасы" },
                        { ComedyKey, "Комедия" },
                        { ActionKey, "Боевик" }
                    }
                }
            };
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            yield break;
        }
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return this;
        }

        public LocalizedString this[string name] => new LocalizedString(name, GetTranslation(name));

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        private string GetTranslation(string key)
        {
            var lang = string.IsNullOrWhiteSpace(_languageContext.Language)
                ? "en"
                : _languageContext.Language;

            if (_translations.TryGetValue(lang, out var dict) &&
                dict.TryGetValue(key, out var translated))
            {
                return translated;
            }
            
            return key;
        }
    }
}

