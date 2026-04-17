# Selenium Failure Analyzer — Reference

## Project context

- **Framework:** C# / .NET 9
- **Test runner:** NUnit 4
- **Browser automation:** Selenium WebDriver 4
- **Reporting:** Allure
- **Site under test:** the-internet.herokuapp.com

## File locations

| File | Purpose |
|------|---------|
| `Drivers/AIFailureAnalyzer.cs` | Calls Claude API with failure context |
| `Drivers/ScreenshotHelper.cs` | Captures screenshot on failure |
| `Drivers/ConfigManager.cs` | Reads AnthropicApiKey from config |
| `Tests/LoginTests.cs` | TearDown calls analyzer when test fails |

## How the analyzer is called

```csharp
// In TearDown when test fails:
var analysis = AIFailureAnalyzer.AnalyzeFailureAsync(
    testName,    // e.g. "Login_WithValidCredentials_ShouldSucceed"
    errorMessage, // NUnit assertion message
    stackTrace   // full stack trace
).Result;

AllureApi.AddAttachment("AI Failure Analysis", "text/plain", bytes);
```

## Page Objects in scope

- `LoginPage.cs` — login form, success/error flash messages
- `CheckboxesPage.cs` — checkbox state management
- `DropdownPage.cs` — HTML select element
- `AlertsPage.cs` — JS alerts, confirms, prompts
- `DynamicLoadingPage.cs` — async content with spinner

## Common selectors used

```csharp
By.Id("username")
By.Id("password")
By.CssSelector("button[type='submit']")
By.CssSelector(".flash.success")
By.CssSelector(".flash.error")
By.Id("dropdown")
By.XPath("//button[text()='Click for JS Alert']")
By.Id("result")
By.CssSelector("#start button")
By.Id("loading")
By.CssSelector("#finish h4")
```