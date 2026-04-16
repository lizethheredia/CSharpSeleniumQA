using Allure.Net.Commons;
using OpenQA.Selenium;

namespace CSharpSeleniumQA.Drivers
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var bytes = screenshot.AsByteArray;
                AllureApi.AddAttachment(fileName, "image/png", bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot failed: {ex.Message}");
            }
        }
    }
}
