using System.Collections.Generic;
using System.Linq;
using backEnd;
using backEnd.Controllers;
using NUnit.Framework;

namespace TestProject1
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
            WeatherForecastController controller = new WeatherForecastController(null);//无参，所以为null
            IEnumerable<WeatherForecast> result = controller.Get();
            //cookie
            Assert.AreEqual(result.ToList().Count, 5);//注意这里不能对比JSON格式的数据，返回5个
            Assert.Pass();
            Assert.Pass();
        }
    }
}