using Allure.NUnit;
using Allure.NUnit.Attributes;
using CSharpSeleniumQA.Drivers;
using CSharpSeleniumQA.Pages;
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
        public void DynamicLoading_WaitForElement_ShouldAppear()
        {
            _dynamicLoadingPage.ClickStart();
            var result = _dynamicLoadingPage.WaitForResult();
            Assert.That(result, Is.EqualTo("Hello World!"), "Element should appear after loading");
        }
    }
}
