using NUnit.Framework;
using ConsoleApp1;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConnectChrome()
        {
            Browser app = new Browser();
            bool result = app.Connect(new ChromeDriver());
            Assert.True(result);
        }

        [Test]
        public void TestConnectFirefox()
        {
            Browser app = new Browser();
            bool result = app.Connect(new FirefoxDriver());
            Assert.True(result);
        }

        [Test]
        public void TestConnectIE()
        {
            Browser app = new Browser();
            bool result = app.Connect(new InternetExplorerDriver());
            Assert.True(result);
        }
    }
}