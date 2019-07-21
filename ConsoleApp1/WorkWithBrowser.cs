using OpenQA.Selenium;
using System;
using System.Threading;

namespace ConsoleApp1
{
    public class WorkWithBrowser
    {
        IWebDriver driver;
        int countLetterFromName;
        PageObject pg;
        WorkWithLocators workWithLocators;
        string name;
        string email;

        public WorkWithBrowser(IWebDriver driver, string name, string email)
        {
            this.driver = driver;
            this.name = name;
            this.email = email;
        }

        public bool Connect(string email, string password)
        {
            pg = new PageObject(driver, "https://gmail.com");          
            if (driver.Title.Equals("Gmail"))
            {
                workWithLocators.loginAs(email, password);
                workWithLocators.VerifiEmail();
                return true;
            }
            else if (driver.Title.Equals("Gmail – электронная почта и бесплатное хранилище от Google"))
            {
                ReConnect();
                workWithLocators.loginAs(email, password);
                workWithLocators.VerifiEmail();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int FindLetters(string name, string email)
        {
            countLetterFromName = 0;

            IWebElement[] webs = workWithLocators.findEmailAndAllLetters(email);
            foreach (IWebElement element in webs)
            {
                try
                {
                    Thread.Sleep(1000);
                    element.Click();
                    IWebElement[] emailInWeb = workWithLocators.FindLettersOnPage();
                    foreach (IWebElement web in emailInWeb)
                    {
                        if (web.GetAttribute("email").Equals(email))
                        {
                            countLetterFromName++;
                        }
                    }
                    Back();
                }
                catch { }
            }
            Console.WriteLine("Найдено " + countLetterFromName + " писем от " + name);
            return countLetterFromName;
        }

        public bool SendLeter(string email)
        {
            Console.WriteLine("Письмо отправлено.");
            return workWithLocators.SendResultLetter(email, "Тестовое задание. Бондаренко.", "Мы нашли " + countLetterFromName + " писем от Вас.");
            
        }

        public void ReConnect()
        {
            driver.Navigate().GoToUrl(pg.typeReConnectLink());
        }

        public void Back()
        {
            driver.Navigate().Back();
        }

        public void Quit()
        {
            driver.Quit();
        }
    }
}
