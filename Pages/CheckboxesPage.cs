using CSharpSeleniumQA.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CSharpSeleniumQA.Pages
{
    public class CheckboxesPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private By Checkboxes => By.CssSelector("input[type='checkbox']");

        public CheckboxesPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl($"{ConfigManager.BaseUrl}/checkboxes");
        }

        public void CheckAll()
        {
            var checkboxes = _driver.FindElements(Checkboxes);
            foreach (var checkbox in checkboxes)
            {
                if (!checkbox.Selected)
                    checkbox.Click();
            }
        }

        public void UncheckAll()
        {
            var checkboxes = _driver.FindElements(Checkboxes);
            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Selected)
                    checkbox.Click();
            }
        }

        public bool AreAllChecked()
        {
            var checkboxes = _driver.FindElements(Checkboxes);
            return checkboxes.All(c => c.Selected);
        }

        public bool AreAllUnchecked()
        {
            var checkboxes = _driver.FindElements(Checkboxes);
            return checkboxes.All(c => !c.Selected);
        }
    }
}
