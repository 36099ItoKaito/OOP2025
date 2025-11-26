using System;
using System.Collections.Generic;
using System.Text.Json;

namespace TenkiApp.Services {
    public class WeatherParser {

        public CurrentWeather ParseCurrent(JsonDocument weatherData) {
            var current = weatherData.RootElement.GetProperty("current");
            var daily = weatherData.RootElement.GetProperty("daily");

            return new CurrentWeather {
                Temperature = current.GetProperty("temperature_2m").GetDouble(),
                WeatherCode = current.GetProperty("weather_code").GetInt32(),
                Humidity = current.GetProperty("relative_humidity_2m").GetDouble(),
                WindSpeed = current.GetProperty("wind_speed_10m").GetDouble(),
                MaxTemp = daily.GetProperty("temperature_2m_max")[0].GetDouble(),
                MinTemp = daily.GetProperty("temperature_2m_min")[0].GetDouble()
            };
        }

        // ParseHourly は MainWindow 側のバインディングに合う形 (Time, Weather, Temperature, Precipitation の文字列) を返します
        public List<HourlyForecastDto> ParseHourly(JsonDocument weatherData) {
            var hourly = weatherData.RootElement.GetProperty("hourly");

            var times = hourly.GetProperty("time");
            var temps = hourly.GetProperty("temperature_2m");
            var codes = hourly.GetProperty("weather_code");
            var rain = hourly.GetProperty("precipitation_probability");

            var results = new List<HourlyForecastDto>();

            int count = Math.Min(24, times.GetArrayLength());
            for (int i = 0; i < count; i++) {
                string timeStr = times[i].GetString() ?? "";
                DateTime time;
                // 安全にパース（もし失敗したら元文字列を表示）
                if (!DateTime.TryParse(timeStr, out time)) {
                    time = DateTime.MinValue;
                }

                double t = temps[i].GetDouble();
                int weatherCode = codes[i].GetInt32();
                int precip = rain[i].GetInt32();

                results.Add(new HourlyForecastDto {
                    Time = time != DateTime.MinValue ? time.ToString("HH:mm") : timeStr,
                    Temperature = $"{t:F1}°C",
                    Weather = WeatherCodeToEmoji(weatherCode),
                    Precipitation = precip > 0 ? $"💧 {precip}%" : ""
                });
            }

            return results;
        }

        // 簡易絵文字マッピング（MainWindow のものと整合）
        private string WeatherCodeToEmoji(int code) {
            return code switch {
                0 => "☀️",
                1 or 2 => "🌤️",
                3 => "☁️",
                45 or 48 => "🌫️",
                51 or 53 or 55 => "🌦️",
                61 or 63 or 65 => "🌧️",
                71 or 73 or 75 => "❄️",
                77 => "🌨️",
                80 or 81 or 82 => "🌦️",
                85 or 86 => "🌨️",
                95 => "⛈️",
                96 or 99 => "⛈️",
                _ => "❓"
            };
        }
    }

    // ParseHourly の戻り値 DTO（UI バインド用）
    public class HourlyForecastDto {
        public string Time { get; set; } = "";
        public string Weather { get; set; } = "";
        public string Temperature { get; set; } = "";
        public string Precipitation { get; set; } = "";
    }

    public class CurrentWeather {
        public double Temperature { get; set; }
        public int WeatherCode { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
    }
}
