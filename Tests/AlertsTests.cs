using Allure.NUnit;
using Allure.NUnit.Attributes;
using CSharpSeleniumQA.Drivers;
using CSharpSeleniumQA.Pages;
using OpenQA.Selenium;

namespace CSharpSeleniumQA.Tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Alerts")]
    public class AlertsTests
    {
        private IWebDriver _driver;
        private AlertsPage _alertsPage;

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateDriver(
                browser: ConfigManager.Browser,
                headless: ConfigManager.Headless
            );
            _alertsPage = new AlertsPage(_driver);
            _alertsPage.GoTo();
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
            }
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Alert_Accept_ShouldShowSuccessMessage()
        {
            _alertsPage.TriggerAndAcceptAlert();
            Assert.That(
                _alertsPage.GetResult(),
                Is.EqualTo("You successfully clicked an alert"),
                "Alert should be accepted"
            );
        }

        [Test]
        public void Confirm_Dismiss_ShouldShowCancelledMessage()
        {
            _alertsPage.TriggerAndDismissConfirm();
            Assert.That(
                _alertsPage.GetResult(),
                Is.EqualTo("You clicked: Cancel"),
                "Confirm should be dismissed"
            );
        }

        [Test]
        public void Prompt_TypeText_ShouldShowTypedText()
        {
            _alertsPage.TriggerPromptAndType("Liz QA");
            Assert.That(
                _alertsPage.GetResult(),
                Is.EqualTo("You entered: Liz QA"),
                "Prompt should show typed text"
            );
        }
    }
}
