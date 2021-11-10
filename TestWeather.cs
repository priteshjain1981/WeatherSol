using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Weather_Shared;

namespace WeatherSolution
{
    public class TestWeather
    {
        public async void GetWeatherInformation(string cityId)
        {
            var client = new HttpClient();

            var uri = new Uri(@"http://api.openweathermap.org/data/2.5/weather?id=" + cityId + "&appid=aa69195559bd4f88d79f9aadeb77a8f6");

            var response = await client.GetStringAsync(uri);

            WeatherCollection weatherCollection = JsonConvert.DeserializeObject<WeatherCollection>(response);
            Console.WriteLine(weatherCollection.WeatherData[0].name);
        }
    }
}
