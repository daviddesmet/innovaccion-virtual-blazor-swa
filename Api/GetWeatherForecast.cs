using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using BlazorApp.Shared;
using BlazorApp.Api.Models;

namespace BlazorApp.Api
{
    public static class GetWeatherForecast
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private static void AddHeaderIfMissing(HttpClient httpClient, string name, string value)
        {
            if (!httpClient.DefaultRequestHeaders.Contains(name))
                httpClient.DefaultRequestHeaders.Add(name, value);
        }

        [FunctionName("ForecastConagua")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ILogger log)
        {
            // Kudos to https://smn.conagua.gob.mx/es/pronostico-del-tiempo-por-municipios

            AddHeaderIfMissing(_httpClient, "Host", "smn.conagua.gob.mx");
            AddHeaderIfMissing(_httpClient, "Referer", "https://smn.conagua.gob.mx/tools/PHP/pronostico_municipios_grafico/index.php");
            AddHeaderIfMissing(_httpClient, "Sec-Fetch-Dest", "empty");
            AddHeaderIfMissing(_httpClient, "Sec-Fetch-Mode", "cors");
            AddHeaderIfMissing(_httpClient, "Sec-Fetch-Site", "same-origin");
            AddHeaderIfMissing(_httpClient, "X-Requested-With", "XMLHttpRequest");

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Blazor Forecast");

            var response = await _httpClient.GetStringAsync("https://smn.conagua.gob.mx/tools/PHP/pronostico_municipios_grafico/controlador/getDataJson2String.php?edo=11&mun=20");
            var forecasts = JsonConvert.DeserializeObject<ConaguaWeatherResponse[]>(response);
            var result = forecasts.Select((f, i) => new WeatherForecast
            {
                Date = DateTime.UtcNow.Date.AddDays(i),
                TemperatureMaxC = Convert.ToInt32(f.TempMax),
                TemperatureMinC = Convert.ToInt32(f.TempMin),
                Summary = f.SkyDesc
            });

            return new OkObjectResult(result);
        }
    }
}
