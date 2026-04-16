using CSharpSeleniumQA.BDD.Drivers;
using CSharpSeleniumQA.BDD.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace CSharpSeleniumQA.BDD.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;
        private readonly ScenarioContext _scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = DriverFactory.CreateDriver(
                browser: Enum.Parse<BrowserType>(ConfigManager.Browser),
                headless: ConfigManager.Headless
            );
            _loginPage = new LoginPage(_driver);
        }

        [Given("I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            _loginPage.GoTo();
        }

        [When("I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _loginPage.Login(ConfigManager.ValidUsername, ConfigManager.ValidPassword);
        }

        [When("I enter invalid credentials")]
        public void WhenIEnterInvalidCredentials()
        {
            _loginPage.Login(ConfigManager.InvalidUsername, ConfigManager.InvalidPassword);
        }

        [Then("I should see the success message")]
        public void ThenIShouldSeeTheSuccessMessage()
        {
            Assert.That(_loginPage.IsLoginSuccessful(), Is.True, "Login should succeed");
        }

        [Then("I should see the error message")]
        public void ThenIShouldSeeTheErrorMessage()
        {
            Assert.That(_loginPage.IsLoginFailed(), Is.True, "Login should fail");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                ScreenshotHelper.TakeScreenshot(_driver, _scenarioContext.ScenarioInfo.Title);
            }
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
