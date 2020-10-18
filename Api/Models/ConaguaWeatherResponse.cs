using Newtonsoft.Json;

namespace BlazorApp.Api.Models
{
    public class ConaguaWeatherResponse
    {
        [JsonProperty("nes")]
        public string StateName { get; set; }

        [JsonProperty("nmun")]
        public string CityName { get; set; }

        [JsonProperty("tmax")]
        public double TempMax { get; set; }

        [JsonProperty("tmin")]
        public double TempMin { get; set; }

        [JsonProperty("desciel")]
        public string SkyDesc { get; set; }

        [JsonProperty("probprec")]
        public double RainProbability { get; set; }

        [JsonProperty("prec")]
        public double Precipitation { get; set; }

        [JsonProperty("velvien")]
        public double WindVelocity { get; set; }

        [JsonProperty("dirvienc")]
        public string WindDirection { get; set; }

        [JsonProperty("raf")]
        public double WindGust { get; set; }
    }
}
