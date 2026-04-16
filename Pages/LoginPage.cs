using CSharpSeleniumQA.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CSharpSeleniumQA.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Selectores
        private By UsernameField => By.Id("username");
        private By PasswordField => By.Id("password");
        private By LoginButton => By.CssSelector("button[type='submit']");
        private By SuccessMessage => By.CssSelector(".flash.success");
        private By ErrorMessage => By.CssSelector(".flash.error");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl($"{ConfigManager.BaseUrl}/login");
        }

        public void Login(string username, string password)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(UsernameField));
            _driver.FindElement(UsernameField).Clear();
            _driver.FindElement(UsernameField).SendKeys(username);
            _driver.FindElement(PasswordField).Clear();
            _driver.FindElement(PasswordField).SendKeys(password);
            _driver.FindElement(LoginButton).Click();
        }

        public bool IsLoginSuccessful()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(SuccessMessage));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsLoginFailed()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(ErrorMessage));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}
