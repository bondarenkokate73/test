using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace ConsoleApp1
{
    class GRID
    {
        IWebDriver driver;

        DesiredCapabilities capability;
        //     capability = DesiredCapabilities.Chrome();       

        public IWebDriver getDriver()
        {
            capability = new DesiredCapabilities();
            capability.SetCapability(CapabilityType.BrowserName, "chrome");
            capability.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
            driver = new RemoteWebDriver(new Uri(@"http://192.168.56.1:5353/wd/hub"), capability);
            return driver;
        }
    }
}
