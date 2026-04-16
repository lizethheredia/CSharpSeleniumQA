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
    [AllureSuite("Checkboxes")]
    public class CheckboxesTests
    {
        private IWebDriver _driver;
        private CheckboxesPage _checkboxesPage;

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateDriver(
                browser: ConfigManager.Browser,
                headless: ConfigManager.Headless
            );
            _checkboxesPage = new CheckboxesPage(_driver);
            _checkboxesPage.GoTo();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Checkboxes_CheckAll_ShouldBeSelected()
        {
            _checkboxesPage.CheckAll();
            Assert.That(
                _checkboxesPage.AreAllChecked(),
                Is.True,
                "Todos los checkboxes deberían estar seleccionados"
            );
        }

        [Test]
        public void Checkboxes_UncheckAll_ShouldBeDeselected()
        {
            _checkboxesPage.CheckAll();
            _checkboxesPage.UncheckAll();
            Assert.That(
                _checkboxesPage.AreAllUnchecked(),
                Is.True,
                "Todos los checkboxes deberían estar deseleccionados"
            );
        }
    }
}
