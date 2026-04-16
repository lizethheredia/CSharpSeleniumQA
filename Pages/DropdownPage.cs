using CSharpSeleniumQA.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace CSharpSeleniumQA.Pages
{
    public class DropdownPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private By DropdownSelector => By.Id("dropdown");

        public DropdownPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl($"{ConfigManager.BaseUrl}/dropdown");
        }

        public void SelectByValue(string value)
        {
            var dropdown = new SelectElement(_driver.FindElement(DropdownSelector));
            dropdown.SelectByValue(value);
        }

        public void SelectByText(string text)
        {
            var dropdown = new SelectElement(_driver.FindElement(DropdownSelector));
            dropdown.SelectByText(text);
        }

        public string GetSelectedOption()
        {
            var dropdown = new SelectElement(_driver.FindElement(DropdownSelector));
            return dropdown.SelectedOption.Text;
        }
    }
}
