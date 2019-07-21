using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    public class PageObject
    {
        private By textGoInLocator = By.ClassName("INl6Jd");
        private By inputReservEmail = By.ClassName("whsOnd");
        private By buttonGoInLocator = By.TagName("a");
        private By buttonVerifEmail = By.ClassName("vR13fe");
        //LoginLocators
        private By usernameLocator = By.Id("identifierId");
        private By passwordLocator = By.Name("password");
        //SearchLocators
        private By searchLocator = By.ClassName("gb_cf");
        private By findAllLettersLocator = By.ClassName("xS");
        private By findLettersFromEmailLocator = By.ClassName("gD");
        //SendLetterLocators
        private By buttonWriteLetterLocator = By.ClassName("z0");
        private By adressLocator = By.Name("to");
        private By themeAndtextOfLetterLocator = By.ClassName("aoT");
        private By letterWasSendLocator = By.ClassName("bAq");

        private IWebDriver driver;

        public PageObject(IWebDriver driver, string url)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
        }

        private void Wait(By locator)
        {
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch
            {
                Console.WriteLine("Время ожидания поиска элемента истокло. Проверьте подключение к интернету");
            }
        }

        //FindElements
        public string typeReConnectLink()
        {
            return driver.FindElements(buttonGoInLocator)
                .Where(a => a.Text.Trim().Equals("Войти")).First().GetAttribute("href");
        }

        public IWebElement typeVerifiEmail()
        {
            Wait(textGoInLocator);
            if (driver.FindElement(textGoInLocator).Text.Trim().Equals("Подтвердите резервный адрес электронной почты"))
            {
                driver.FindElement(buttonVerifEmail).Click();
                Wait(inputReservEmail);
                return driver.FindElement(inputReservEmail);
            }
            return null;
        }

        public IWebElement typeUsername()
        {
            Wait(usernameLocator);
            return driver.FindElement(usernameLocator);
        }

        public IWebElement typePassword()
        {
            Wait(passwordLocator);
            return driver.FindElement(passwordLocator);
        }

        public IWebElement typeButtonSend()
        {
            Wait(buttonWriteLetterLocator);
            return driver.FindElement(buttonWriteLetterLocator);
        }

        public IWebElement typeAdress()
        {
            Wait(adressLocator);
            return driver.FindElement(adressLocator);
        }

        public IWebElement typeText()
        {
            Wait(themeAndtextOfLetterLocator);
            return driver.FindElement(themeAndtextOfLetterLocator);
        }

        public IWebElement[] typeSearch(string email)
        {
            IWebElement[] resultArray;
            Wait(searchLocator);
            driver.FindElements(searchLocator).Where(a => a.TagName.Equals("input")).First().SendKeys(email + Keys.Enter);
            Thread.Sleep(2000); //по-другому работать не хочет, надо разобраться
         //   Wait(findAllLettersLocator);
            do
            {
                resultArray = driver.FindElements(findAllLettersLocator).ToArray();
            } while (resultArray.Count() == 0);
            return resultArray;
        }

        public IWebElement[] typeFindLettersOnPage()
        {
            Wait(findLettersFromEmailLocator);
            return driver.FindElements(findLettersFromEmailLocator).ToArray();
        }

        public bool typeLetterWasSend()
        {
            Thread.Sleep(2000);
            return driver.FindElement(letterWasSendLocator).Text.Trim().Equals("Письмо отправлено.");

        }   
    }
}
