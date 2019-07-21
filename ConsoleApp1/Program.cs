using System;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ConsoleApp1
{
    [AllureNUnit]
    class Program
    {
        const string name = "Екатерина Бондаренко"; //Введите свое имя
        const string email = "bondarenkokate73@yandex.ru"; //Введите почту, письма с которой надо найти
        static bool success = false;
        static WorkWithBrowser browser;
        static GRID grid;
        static IWebDriver driver;
        static PageObject pg;

        static void Main(string[] args)
        {
            grid = new GRID();
            driver = grid.getDriver();     // = new ChromeDriver(Environment.CurrentDirectory);
            browser = new WorkWithBrowser(driver, name, email);
            pg = new PageObject(driver, "https://gmail.com");
            Test();
        }

        [Test]
        [AllureSuite("Connect")]
        static void Test()
        {
            if (browser.Connect("test19bond@gmail.com", "test19bondtest19bond"))
            {
                browser.FindLetters(name, email);
                if (browser.SendLeter(email))
                {
                    success = true;
                }
                browser.Quit();
            }
            else
            {
                Console.WriteLine("Подлючение не удалось");
            }

            Assert.True(success);
        }
    }
}
