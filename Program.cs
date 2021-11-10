using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherSolution
{
    class Program
    {
        public static void Main(string[] args)
        {
            TestWeather tw = new TestWeather();
            tw.GetWeatherInformation("2988507");

            Console.ReadLine();




        }

    }
       
}
