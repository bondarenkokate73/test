using OpenQA.Selenium;

namespace ConsoleApp1
{
    class WorkWithLocators
    {
        PageObject pg;

        public void loginAs(string username, string password)
        {
            pg.typeUsername().SendKeys(username + Keys.Enter);
            pg.typePassword().SendKeys(password + Keys.Enter);
        }

        public IWebElement[] findEmailAndAllLetters(string email)
        {
            return pg.typeSearch(email);
        }

        public IWebElement[] FindLettersOnPage()
        {
            return pg.typeFindLettersOnPage();
        }

        public bool SendResultLetter(string email, string theme, string text)
        {
            pg.typeButtonSend().Click();
            pg.typeAdress().SendKeys(email);
            pg.typeText().SendKeys(theme + Keys.Tab + text + (Keys.Control + Keys.Enter));
            return pg.typeLetterWasSend();
        }

        public void VerifiEmail()
        {
            try
            {
                pg.typeVerifiEmail().SendKeys("bondarenkokate73@yandex.ru" + Keys.Enter);
            }
            catch { }
        }
    }
}
