using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using CSharpSeleniumQA.Drivers;

namespace CSharpSeleniumQA.Pages
{
    public class AlertsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private By AlertButton => By.XPath("//button[text()='Click for JS Alert']");
        private By ConfirmButton => By.XPath("//button[text()='Click for JS Confirm']");
        private By PromptButton => By.XPath("//button[text()='Click for JS Prompt']");
        private By Result => By.Id("result");

        public AlertsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl($"{ConfigManager.BaseUrl}/javascript_alerts");
        }

        public void TriggerAndAcceptAlert()
        {
            _driver.FindElement(AlertButton).Click();
            _wait.Until(ExpectedConditions.AlertIsPresent());
            _driver.SwitchTo().Alert().Accept();
        }

        public void TriggerAndDismissConfirm()
        {
            _driver.FindElement(ConfirmButton).Click();
            _wait.Until(ExpectedConditions.AlertIsPresent());
            _driver.SwitchTo().Alert().Dismiss();
        }

        public void TriggerPromptAndType(string text)
        {
            _driver.FindElement(PromptButton).Click();
            _wait.Until(ExpectedConditions.AlertIsPresent());
            var alert = _driver.SwitchTo().Alert();
            alert.SendKeys(text);
            alert.Accept();
        }

        public string GetResult()
        {
            return _driver.FindElement(Result).Text;
        }
    }
}
