using Allure.NUnit;
using Allure.NUnit.Attributes;
using CSharpSeleniumQA.Drivers;
using CSharpSeleniumQA.Pages;
using OpenQA.Selenium;

namespace CSharpSeleniumQA.Tests
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Dropdown")]
    public class DropdownTests
    {
        private IWebDriver _driver;
        private DropdownPage _dropdownPage;

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateDriver(
                browser: ConfigManager.Browser,
                headless: ConfigManager.Headless
            );
            _dropdownPage = new DropdownPage(_driver);
            _dropdownPage.GoTo();
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
        public void Dropdown_SelectOption1_ShouldBeSelected()
        {
            _dropdownPage.SelectByValue("1");
            Assert.That(
                _dropdownPage.GetSelectedOption(),
                Is.EqualTo("Option 1"),
                "Option 1 should be selected"
            );
        }

        [Test]
        public void Dropdown_SelectOption2_ShouldBeSelected()
        {
            _dropdownPage.SelectByText("Option 2");
            Assert.That(
                _dropdownPage.GetSelectedOption(),
                Is.EqualTo("Option 2"),
                "Option 2 should be selected"
            );
        }
    }
}
