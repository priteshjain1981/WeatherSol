using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherNews;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;
using System.Configuration;

namespace WeatherNews.Tests
{
    [TestClass()]
    public class GetWeatherTests
    {
        [TestMethod()]
        public void GetWeatherInformationTest()
        {
            //Arrange
            var getWeatherValidator = new GetWeather();
            const string cityId = "2643741"; //2643741=City of London

            //Act
            var cityWeather = getWeatherValidator.GetWeatherInformation(cityId, "aa69195559bd4f88d79f9aadeb77a8f6");

            //Assert    
            string cityName = cityWeather.Result;
          
            Assert.IsTrue(cityName.Contains("City of London"), $"Weather Data for {cityName} is valid");
        }
    }
}