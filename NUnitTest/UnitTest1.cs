using WeatherMap_api;

namespace NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Able_to_create_weather_object()
        {
            WeatherForecast obj = new WeatherForecast()
            { 
                Date = DateTime.Now,
                Summary = "test"
            };
            Assert.IsTrue(obj != null);
        }
    }
}