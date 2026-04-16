using Allure.NUnit;
using Allure.NUnit.Attributes;
using CSharpSeleniumQA.Drivers;
using CSharpSeleniumQA.Pages;
using NUnit.Framework;
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
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            _loginPage.Login(ConfigManager.ValidUsername, ConfigManager.ValidPassword);
            Assert.That(
                _loginPage.IsLoginSuccessful(),
                Is.True,
                "El login válido debería ser exitoso"
            );
        }

        [Test]
        public void Login_WithInvalidCredentials_ShouldFail()
        {
            _loginPage.Login(ConfigManager.InvalidUsername, ConfigManager.InvalidPassword);
            Assert.That(
                _loginPage.IsLoginFailed(),
                Is.True,
                "El login inválido debería mostrar error"
            );
        }
    }
}
