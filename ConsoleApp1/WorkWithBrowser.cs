using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    public class WorkWithBrowser
    {
        
        int countLetterFromName;
        PageObject pg;
        string name;
        string email;

        public WorkWithBrowser(string name, string email)
        {
            this.name = name;
            this.email = email;
        }

        public bool Connect(IWebDriver driver, string email, string password)
        {
            pg = new PageObject(driver, "https://gmail.com");          
            if (driver.Title.Equals("Gmail"))
            {
                pg.loginAs(email, password);
                pg.VerifiEmail();
                return true;
            }
            else if (driver.Title.Equals("Gmail – электронная почта и бесплатное хранилище от Google"))
            {
                pg.ReConnect();
                pg.loginAs(email, password);
                pg.VerifiEmail();
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

            IWebElement[] webs = pg.findEmailAndAllLetters(email);
            foreach (IWebElement element in webs)
            {
                try
                {
                    Thread.Sleep(1000);
                    element.Click();
                    IWebElement[] emailInWeb = pg.FindLettersOnPage();
                    foreach (IWebElement web in emailInWeb)
                    {
                        if (web.GetAttribute("email").Equals(email))
                        {
                            countLetterFromName++;
                        }
                    }
                    pg.Back();
                }
                catch { }
            }
            Console.WriteLine("Найдено " + countLetterFromName + " писем от " + name);
            return countLetterFromName;
        }

        public bool SendLeter(string email)
        {
            Console.WriteLine("Письмо отправлено.");
            return pg.SendResultLetter(email, "Тестовое задание. Бондаренко.", "Мы нашли " + countLetterFromName + " писем от Вас.");
            
        }
    }
}
