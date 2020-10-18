using System;
using System.Globalization;

namespace BlazorApp.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public string DateFormatFriendly
        {
            get
            {
                if (Date.Date == DateTime.UtcNow.Date)
                    return "Hoy";

                if (Date.Date == DateTime.UtcNow.Date.AddDays(1))
                    return "Mañana";

                var date = Date.ToString("dddd", new CultureInfo("es-MX"));
                return char.ToUpper(date[0]) + date.Substring(1);
            }
        }

        public int TemperatureMaxC { get; set; }

        public int TemperatureMaxF => 32 + (int)(TemperatureMaxC / 0.5556);

        public int TemperatureMinC { get; set; }

        public int TemperatureMinF => 32 + (int)(TemperatureMaxC / 0.5556);

        public string Summary { get; set; }
    }
}
