namespace WebApplicationUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testValue1 = "test";
            var testValue2 = "another test";

            Assert.AreNotEqual(testValue1, testValue2);
        }
    }
}