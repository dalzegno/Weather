using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json; //Requires nuget package System.Net.Http.Json
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Text.Json;

using Weather.Models;

namespace Weather.Services
{
    //You replace this class witth your own Service from Project Part A
    public class OpenWeatherService
    {
        readonly string apiKey = "8012a9abcb7e03e2b21180ce04cb1259";

        public readonly ConcurrentDictionary<(string, string), Forecast> _cachedCity = new ConcurrentDictionary<(string, string), Forecast>();
        public readonly ConcurrentDictionary<((double, double), string), Forecast> _cachedGeoLoc = new ConcurrentDictionary<((double, double), string), Forecast>();

        public event EventHandler<string> WeatherForecastAvailable;
        protected virtual void OnWeatherForeCastAvailable(object sender, string f)
        {
            WeatherForecastAvailable?.Invoke(this, f);
        }
        public async Task<Forecast> GetForecastAsync(string City)
        {
            string d = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            if (_cachedCity.ContainsKey((City, d)))
            {
                Forecast f;
                _cachedCity.TryGetValue((City, d), out f);
                OnWeatherForeCastAvailable(this, $"Cached forecast for {City} available");
                return f;
            }
            else
            {
                //https://openweathermap.org/current
                var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                var uri = $"https://api.openweathermap.org/data/2.5/forecast?q={City}&units=metric&lang={language}&appid={apiKey}";
                Forecast forecast = await ReadWebApiAsync(uri);
                OnWeatherForeCastAvailable(this, $"New weather forecast for {City} available.");
                string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                _cachedCity.GetOrAdd((City, dt), forecast);
                return forecast;
            }
        }
        
        public async Task<Forecast> GetForecastAsync(double latitude, double longitude)
        {
            string d = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //gets cached forecast if available
            if (_cachedGeoLoc.ContainsKey(((latitude, longitude), d)))
            {
                Forecast f;
                _cachedGeoLoc.TryGetValue(((latitude, longitude), d), out f);
                OnWeatherForeCastAvailable(this, $"Cached forecast for {latitude},{longitude} available");
                return f;
            }
            //adds new forecast from ReadWebApi
            else
            {
                //https://openweathermap.org/current
                var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                var uri = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&lang={language}&appid={apiKey}";

                Forecast forecast = await ReadWebApiAsync(uri);
                OnWeatherForeCastAvailable(this, $"New weather forecast for {latitude}, {longitude} available");
                string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                _cachedGeoLoc.GetOrAdd(((latitude, longitude), dt), forecast);
                return forecast;
            }
        }
        private async Task<Forecast> ReadWebApiAsync(string uri)
        {
            // Reads WeatherApiData and returns new Forecast object
            WeatherApiData w;
            using (var httpClient = new HttpClient())
            using (HttpResponseMessage response = await httpClient.GetAsync(uri))
            {
                response.EnsureSuccessStatusCode();
                w = await response.Content.ReadFromJsonAsync<WeatherApiData>();
            }
            Forecast forecast = new Forecast()
                {
                    City = w.city.name,
                    Items = w.list.Select(x => new ForecastItem()
                    {
                        Description = x.weather[0].description,
                        Temperature = x.main.temp,
                        dateTime = UnixTimeStampToDateTime(x.dt),
                        WindSpeed = x.wind.speed
                    }
                ).ToList()
                };
                return forecast;
        }
        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
