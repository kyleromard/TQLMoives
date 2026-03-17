using Microsoft.Extensions.Localization;
using TQLMovies.Localization;
using TQLMovies.Services;
using TQLMovies.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<LanguageContext>();
builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IStringLocalizer, LanguageLocalizer>();
builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseMiddleware<AcceptLanguageMiddleware>();

app.MapControllers();

app.Run();
