using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class PageObject
    {
        By textGoInLocator = By.ClassName("INl6Jd");
        By inputReservEmail = By.ClassName("whsOnd");
        By buttonGoInLocator = By.TagName("a");
        By buttonVerifEmail = By.ClassName("vR13fe");
        //LoginLocators
        By usernameLocator = By.Id("identifierId");
        By passwordLocator = By.Name("password");
        //SearchLocators
        By searchLocator = By.ClassName("gb_cf");
        By findAllLettersLocator = By.ClassName("xS");
        By findLettersFromEmailLocator = By.ClassName("gD");
        //SendLetterLocators
        By buttonWriteLetterLocator = By.ClassName("z0");
        By adressLocator = By.Name("to");
        By themeAndtextOfLetterLocator = By.ClassName("aoT");
        By letterWasSendLocator = By.ClassName("bAq");

        private IWebDriver driver;

        public PageObject(IWebDriver driver, string url)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl(url);
        }

        public void Wait(By locator)
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

        public void Back()
        {
            driver.Navigate().Back();
        }

        public void Quit()
        {
            driver.Quit();
        }

        //FindElements
        private string typeReConnectLink()
        {
            return driver.FindElements(buttonGoInLocator)
                .Where(a => a.Text.Trim().Equals("Войти")).First().GetAttribute("href");
        }

        private void typeVerifiEmail()
        {
            Wait(textGoInLocator);
            if (driver.FindElement(textGoInLocator).Text.Trim().Equals("Подтвердите резервный адрес электронной почты"))
            {
                driver.FindElement(buttonVerifEmail).Click();
                Wait(inputReservEmail);
                driver.FindElement(inputReservEmail).SendKeys("bondarenkokate73@yandex.ru" + Keys.Enter);
            }
        }

        private void typeUsername(string username)
        {
            Wait(usernameLocator);
            driver.FindElement(usernameLocator).SendKeys(username + Keys.Enter);
        }

        private void typePassword(string password)
        {
            Wait(passwordLocator);
            driver.FindElement(passwordLocator).SendKeys(password + Keys.Enter);
        }

        private void typeButtonSend()
        {
            Wait(buttonWriteLetterLocator);
            driver.FindElement(buttonWriteLetterLocator).Click();
        }

        private void typeAdress(string email)
        {
            Wait(adressLocator);
            driver.FindElement(adressLocator).SendKeys(email);
        }

        private void typeText(string theme, string text)
        {
            Wait(themeAndtextOfLetterLocator);
            driver.FindElement(themeAndtextOfLetterLocator).SendKeys(theme + Keys.Tab + text + (Keys.Control + Keys.Enter));
        }

        private IWebElement[] typeSearch(string email)
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

        private IWebElement[] typeFindLettersOnPage()
        {
            Wait(findLettersFromEmailLocator);
            return driver.FindElements(findLettersFromEmailLocator).ToArray();
        }

        private bool typeLetterWasSend()
        {
            Thread.Sleep(2000);
            return driver.FindElement(letterWasSendLocator).Text.Trim().Equals("Письмо отправлено.");

        }

        //WorkWithElements
        public void loginAs(string username, string password)
        {
            typeUsername(username);
            typePassword(password);
        }

        public IWebElement[] findEmailAndAllLetters(string email)
        {
            return typeSearch(email);
        }

        public IWebElement[] FindLettersOnPage()
        {
            return typeFindLettersOnPage();
        }

        public bool SendResultLetter(string email, string theme, string text)
        {
            typeButtonSend();
            typeAdress(email);
            typeText(theme, text);
            return typeLetterWasSend();
        }

        public void ReConnect()
        {
            driver.Navigate().GoToUrl(typeReConnectLink());
        }

        public void VerifiEmail()
        {
            try
            {
                typeVerifiEmail();
            }
            catch { }
        }
    }
}
