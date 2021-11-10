using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Weather_Shared;

namespace WeatherNews
{
    public class GetWeather
    {

        public async Task<string> GetWeatherInformation(string cityId,string appId)
        {
            var client = new HttpClient();
            string AppId = string.IsNullOrEmpty(appId) ? ConfigurationManager.AppSettings["AppId"]: appId;

            var uri = new Uri(@"http://api.openweathermap.org/data/2.5/weather?id=" + cityId + "&units=metric&appid=" + AppId);

            var response = await client.GetStringAsync(uri);

            WeatherCollection weatherCollection = JsonConvert.DeserializeObject<WeatherCollection>(response);
            string weatherData = string.Concat(DateTime.Now.ToLongTimeString(), "-",
                weatherCollection.name, 
                "-LAT:", weatherCollection.coord.lat, 
                "-LON:",weatherCollection.coord.lon,
                "-Weather:",weatherCollection.main.temp);
            Console.WriteLine(weatherData); 
            return weatherData; 
        }
    }

}
