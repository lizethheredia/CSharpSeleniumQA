# CSharpSeleniumQA 🧪

Automation test suite built with **C#**, **Selenium WebDriver**, and **NUnit** following the **Page Object Model** pattern. Integrated with **Allure** for reporting and **GitHub Actions** for CI/CD.

---

## 🛠️ Tech Stack

| Tool | Purpose |
|------|---------|
| C# / .NET 9 | Programming language |
| Selenium WebDriver 4 | Browser automation |
| NUnit | Test framework |
| Allure | Test reporting |
| GitHub Actions | CI/CD pipeline |
| Chrome / Firefox | Browsers |

---

## 📁 Project Structure
CSharpSeleniumQA/
├── Drivers/
│   ├── DriverFactory.cs      # Multi-browser setup
│   └── ConfigManager.cs      # Configuration management
├── Pages/
│   ├── LoginPage.cs
│   ├── CheckboxesPage.cs
│   ├── DropdownPage.cs
│   ├── AlertsPage.cs
│   └── DynamicLoadingPage.cs
├── Tests/
│   ├── LoginTests.cs
│   ├── CheckboxesTests.cs
│   ├── DropdownTests.cs
│   ├── AlertsTests.cs
│   └── DynamicLoadingTests.cs
├── .github/workflows/
│   └── selenium-tests.yml
├── appsettings.json
├── allureConfig.json
└── run-tests.sh

---

## ⚙️ Configuration

Configuration is managed via `appsettings.json` with no hardcoded values. Local overrides go in `appsettings.local.json` (gitignored). CI/CD uses GitHub Actions Secrets mapped to environment variables.

---

## 🚀 Run Locally

**Prerequisites:** .NET 9 SDK, Chrome/Firefox, Allure CLI (`brew install allure`)

Run tests + open Allure report:
```bash
./run-tests.sh
```

Run tests only:
```bash
dotnet test
```

---

## 🌐 Test Coverage

| Page | Tests |
|------|-------|
| Login | Valid login, Invalid login |
| Checkboxes | Check all, Uncheck all |
| Dropdown | Select by value, Select by text |
| Alerts | Accept alert, Dismiss confirm, Type in prompt |
| Dynamic Loading | Wait for element to appear |

---

## 📝 Site Under Test

[The Internet - Herokuapp](https://the-internet.herokuapp.com/)
