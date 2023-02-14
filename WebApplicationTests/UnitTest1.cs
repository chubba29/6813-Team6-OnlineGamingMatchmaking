using NUnit.Framework;

namespace WebApplicationTests
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
            var testValue = 1;

            Assert.That(testValue, Is.EqualTo(1));
        }
    }
}