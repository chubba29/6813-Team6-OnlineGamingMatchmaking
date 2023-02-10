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
        public void Test1()
        {
            var testValue = "test pass";

            Assert.That(testValue, Is.EqualTo("test pass"));
        }

        [Test]
        public void WeatherForcastTest()
        {
            var obj = new WeatherForecast();

            var testString = "A summary of weather.";

            obj.Summary = testString;

            Assert.That(obj.Summary, Is.EqualTo(testString));


        }
    }
}