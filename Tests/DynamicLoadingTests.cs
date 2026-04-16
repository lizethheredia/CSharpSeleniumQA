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
    [AllureSuite("DynamicLoading")]
    public class DynamicLoadingTests
    {
        private IWebDriver _driver;
        private DynamicLoadingPage _dynamicLoadingPage;

        [SetUp]
        public void SetUp()
        {
            _driver = DriverFactory.CreateDriver(
                browser: ConfigManager.Browser,
                headless: ConfigManager.Headless
            );
            _dynamicLoadingPage = new DynamicLoadingPage(_driver);
            _dynamicLoadingPage.GoTo();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void DynamicLoading_WaitForElement_ShouldAppear()
        {
            _dynamicLoadingPage.ClickStart();
            var result = _dynamicLoadingPage.WaitForResult();
            Assert.That(result, Is.EqualTo("Hello World!"));
        }
    }
}
