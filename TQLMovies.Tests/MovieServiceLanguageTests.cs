using Microsoft.Extensions.Localization;
using TQLMovies.Localization;
using TQLMovies.Models;
using TQLMovies.Services;

namespace TQLMovies.Tests
{
    public class MovieServiceLanguageTests
    {
        private static MovieService CreateService(string language)
        {
            var repo = new MovieRepository();
            var context = new LanguageContext { Language = language };
            IStringLocalizer localizer = new LanguageLocalizer(context);

            return new MovieService(repo, localizer);
        }

        [Fact]
        public void GetById_NoLanguage_DefaultsToEnglish()
        {
            var service = CreateService(string.Empty);

            var movie = service.GetById(1);

            Assert.NotNull(movie);
            Assert.Equal("Horror", movie!.Category);
        }

        [Fact]
        public void GetById_EnglishLanguage_ReturnsEnglishCategory()
        {
            var service = CreateService("en");

            var movie = service.GetById(1);

            Assert.NotNull(movie);
            Assert.Equal("Horror", movie!.Category);
        }

        [Fact]
        public void GetById_ItalianLanguage_ReturnsItalianCategory()
        {
            var service = CreateService("it");

            var movie = service.GetById(1);

            Assert.NotNull(movie);
            Assert.Equal("Orrore", movie!.Category);
        }

        [Fact]
        public void GetById_RussianLanguage_ReturnsRussianCategory()
        {
            var service = CreateService("ru");

            var movie = service.GetById(1);

            Assert.NotNull(movie);
            Assert.Equal("Ужасы", movie!.Category);
        }
    }
}

