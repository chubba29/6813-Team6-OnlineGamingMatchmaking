using System;
using NUnit.Framework;
using WebServiceGame6813Team6;
using WebServiceGame6813Team6.Controllers;

namespace WebServiceTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ObjectFilledTest()
        {
            var obj = new WeatherForecast();

            var date = DateTime.Now;
            var tempC = 24;
            var tempF = 75;
            var summary = "A summary of weather.";

            obj.Date = date;
            obj.TemperatureC = tempC;
            obj.Summary = summary;

            Assert.That(obj.Date, Is.EqualTo(date));
            Assert.That(obj.TemperatureC, Is.EqualTo(tempC));
            Assert.That(obj.TemperatureF, Is.EqualTo(tempF));
            Assert.That(obj.Summary, Is.EqualTo(summary));


        }

        [Test]
        public void ObjectFilledFailTest()
        {
            var obj = new WeatherForecast();

            var date = DateTime.Now;
            var tempC = 24;
            var tempF = 75;
            var summary = "A summary of weather.";

            obj.Date = DateTime.MaxValue;
            obj.TemperatureC = 0;
            obj.Summary = "Wrong Summary";

            Assert.That(obj.Date, Is.Not.EqualTo(date));
            Assert.That(obj.TemperatureC, Is.Not.EqualTo(tempC));
            Assert.That(obj.TemperatureF, Is.Not.EqualTo(tempF));
            Assert.That(obj.Summary, Is.Not.EqualTo(summary));

        }

        [Test]
        public void DateTest()
        {
            var obj = new WeatherForecast();

            var date = DateTime.Now;

            obj.Date = date;

            Assert.That(obj.Date, Is.EqualTo(date));


        }

        [Test]
        public void DateFailTest()
        {
            var obj = new WeatherForecast();

            var date = DateTime.MaxValue;

            obj.Date = DateTime.Now;

            Assert.That(obj.Date, Is.Not.EqualTo(date));


        }

        [Test]
        public void TemperatureCTest()
        {
            var obj = new WeatherForecast();

            var tempC = 24;

            obj.TemperatureC = 24;

            Assert.That(obj.TemperatureC, Is.EqualTo(tempC));


        }

        [Test]
        public void TemperatureCFailTest()
        {
            var obj = new WeatherForecast();

            var tempC = 24;

            obj.TemperatureC = 100;

            Assert.That(obj.TemperatureC, Is.Not.EqualTo(tempC));


        }

        [Test]
        public void TemperatureFTest()
        {
            var obj = new WeatherForecast();

            var tempC = 24;
            var tempF = 75;

            obj.TemperatureC = tempC;

            Assert.That(obj.TemperatureF, Is.EqualTo(tempF));


        }

        [Test]
        public void TemperatureFFailTest()
        {
            var obj = new WeatherForecast();

            var tempC = 24;
            var tempF = 0;

            obj.TemperatureC = tempC;

            Assert.That(obj.TemperatureF, Is.Not.EqualTo(tempF));


        }

        [Test]
        public void SummaryTest()
        {
            var obj = new WeatherForecast();

            var testString = "A summary of weather.";

            obj.Summary = testString;

            Assert.That(obj.Summary, Is.EqualTo(testString));


        }

        [Test]
        public void SummaryFailTest()
        {
            var obj = new WeatherForecast();

            var testString = "A summary of weather.";

            obj.Summary = "A bad summary";

            Assert.That(obj.Summary, Is.Not.EqualTo(testString));


        }

    }
}