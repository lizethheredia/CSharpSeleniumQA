using CSharpSeleniumQA.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CSharpSeleniumQA.Pages
{
    public class DynamicLoadingPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private By StartButton => By.CssSelector("#start button");
        private By LoadingSpinner => By.Id("loading");
        private By FinishText => By.CssSelector("#finish h4");

        public DynamicLoadingPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl($"{ConfigManager.BaseUrl}/dynamic_loading/1");
        }

        public void ClickStart()
        {
            _driver.FindElement(StartButton).Click();
        }

        public string WaitForResult()
        {
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(LoadingSpinner));
            return _driver.FindElement(FinishText).Text;
        }
    }
}
