using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace CSharpSeleniumQA.BDD.Drivers
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge,
    }

    public class DriverFactory
    {
        public static IWebDriver CreateDriver(
            BrowserType browser = BrowserType.Chrome,
            bool headless = false
        )
        {
            switch (browser)
            {
                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    if (headless)
                        firefoxOptions.AddArgument("--headless");
                    return new FirefoxDriver(firefoxOptions);

                case BrowserType.Edge:
                    var edgeOptions = new EdgeOptions();
                    if (headless)
                        edgeOptions.AddArgument("--headless");
                    return new EdgeDriver(edgeOptions);

                default:
                    var chromeOptions = new ChromeOptions();
                    if (headless)
                    {
                        chromeOptions.AddArgument("--headless");
                        chromeOptions.AddArgument("--no-sandbox");
                        chromeOptions.AddArgument("--disable-dev-shm-usage");
                    }
                    chromeOptions.AddArgument("--window-size=1920,1080");
                    return new ChromeDriver(chromeOptions);
            }
        }
    }
}
