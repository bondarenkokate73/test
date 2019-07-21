using NUnit.Framework;
using ConsoleApp1;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace Tests
{
    [AllureNUnit]
    public class Tests
    {
        const string name = "Екатерина Бондаренко";
        const string email = "bondarenkokate73@yandex.ru";
        WorkWithBrowser browser;
        IWebDriver driver;

        [SetUp]
        public void Init()
        {
            
        }

        [TearDown]
        public void KillDriver()
        {

        }

        [Test]
        [AllureSuite("Connect")]
        public void TestConnect()
        {
            browser = new WorkWithBrowser(name, email);
            driver = new ChromeDriver(Environment.CurrentDirectory);
            bool result = browser.Connect(driver, "test19bond@gmail.com", "test19bondtest19bond");
            Assert.True(result);
        }

        [Test]
        [AllureSuite("FindLetters")]
        public void TestFindLetters()
        {         
            int result = browser.FindLetters(name, email);
            Assert.AreEqual(result,3);
        }

        [Test]
        [AllureSuite("SendLetter")]
        public void TestSendLetter()
        {
            bool result = browser.SendLeter(email);
            driver.Quit();
            Assert.True(result);
        }
    }
}