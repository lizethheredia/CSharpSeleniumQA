using OpenQA.Selenium;

namespace CSharpSeleniumQA.BDD.Drivers
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string scenarioName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                Directory.CreateDirectory(folder);
                var fileName =
                    $"{scenarioName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var filePath = Path.Combine(folder, fileName);
                screenshot.SaveAsFile(filePath);
                Console.WriteLine($"Screenshot saved: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot failed: {ex.Message}");
            }
        }
    }
}
