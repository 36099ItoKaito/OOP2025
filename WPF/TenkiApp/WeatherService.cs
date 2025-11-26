using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TenkiApp.Services {
    public class WeatherService {
        private readonly HttpClient _httpClient;

        public WeatherService() {
            _httpClient = new HttpClient();
        }

        public async Task<JsonDocument> GetWeatherAsync(double lat, double lon) {
            string url = $"https://api.open-meteo.com/v1/forecast?" +
                $"latitude={lat}&longitude={lon}" +
                $"&current=temperature_2m,relative_humidity_2m,weather_code,wind_speed_10m" +
                $"&hourly=temperature_2m,precipitation_probability,weather_code" +
                $"&daily=temperature_2m_max,temperature_2m_min" +
                $"&timezone=Asia/Tokyo";

            string json = await _httpClient.GetStringAsync(url);
            return JsonDocument.Parse(json);
        }

        public async Task<JsonDocument> GetCityCardWeatherAsync(double lat, double lon) {
            string url = $"https://api.open-meteo.com/v1/forecast?" +
                $"latitude={lat}&longitude={lon}" +
                $"&current=temperature_2m,weather_code" +
                $"&daily=temperature_2m_max,temperature_2m_min,precipitation_probability_max" +
                $"&timezone=Asia/Tokyo";

            string json = await _httpClient.GetStringAsync(url);
            return JsonDocument.Parse(json);
        }
    }
}
