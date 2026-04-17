using Allure.NUnit;
using Allure.NUnit.Attributes;
using CSharpSeleniumQA.Drivers;
using CSharpSeleniumQA.Pages;
using OpenQA.Selenium;

namespace CSharpSeleniumQA.Tests
{
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]
    [AllureNUnit]
    [AllureSuite("Login")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class LoginTests
    {
        private readonly BrowserType _browser;
        private IWebDriver _driver;
        private LoginPage _loginPage;

        public LoginTests(BrowserType browser)
        {
            _browser = browser;
        }

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateDriver(
                browser: _browser,
                headless: ConfigManager.Headless
            );
            _loginPage = new LoginPage(_driver);
            _loginPage.GoTo();
        }

        [TearDown]
        public void TearDown()
        {
            if (
                TestContext.CurrentContext.Result.Outcome.Status
                == NUnit.Framework.Interfaces.TestStatus.Failed
            )
            {
                ScreenshotHelper.TakeScreenshot(_driver, TestContext.CurrentContext.Test.Name);

                var errorMessage = TestContext.CurrentContext.Result.Message ?? "Unknown error";
                var stackTrace = TestContext.CurrentContext.Result.StackTrace ?? "No stack trace";
                var testName = TestContext.CurrentContext.Test.Name;

                var analysis = AIFailureAnalyzer
                    .AnalyzeFailureAsync(testName, errorMessage, stackTrace)
                    .Result;

                Allure.Net.Commons.AllureApi.AddAttachment(
                    "AI Failure Analysis",
                    "text/plain",
                    System.Text.Encoding.UTF8.GetBytes(analysis)
                );
            }
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            _loginPage.Login(ConfigManager.ValidUsername, ConfigManager.ValidPassword);
            Assert.That(_loginPage.IsLoginSuccessful(), Is.True, "Valid login should succeed");
        }

        [Test]
        public void Login_WithInvalidCredentials_ShouldFail()
        {
            _loginPage.Login(ConfigManager.InvalidUsername, ConfigManager.InvalidPassword);
            Assert.That(_loginPage.IsLoginFailed(), Is.True, "Invalid login should show error");
        }
    }
}
