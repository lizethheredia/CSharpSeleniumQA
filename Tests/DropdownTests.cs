using Allure.NUnit;
using Allure.NUnit.Attributes;
using CSharpSeleniumQA.Drivers;
using CSharpSeleniumQA.Pages;
using NUnit.Framework;
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
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Dropdown_SelectOption1_ShouldBeSelected()
        {
            _dropdownPage.SelectByValue("1");
            Assert.That(_dropdownPage.GetSelectedOption(), Is.EqualTo("Option 1"));
        }

        [Test]
        public void Dropdown_SelectOption2_ShouldBeSelected()
        {
            _dropdownPage.SelectByText("Option 2");
            Assert.That(_dropdownPage.GetSelectedOption(), Is.EqualTo("Option 2"));
        }
    }
}
