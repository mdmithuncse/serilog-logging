
using Serilog;

namespace SerilogLogging.WebAPI.Endpoints.Weather
{
    internal sealed class Get : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            string requestName = "Get weather forecast request";

            Log.Information("Request started at {0} {1}", DateTime.Now, requestName);

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            Log.Information("Request finished at {0} {1}", DateTime.Now, requestName);
        }
    }
}
