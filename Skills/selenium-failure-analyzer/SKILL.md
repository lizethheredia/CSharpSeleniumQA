---
name: selenium-failure-analyzer
description: Analyzes Selenium WebDriver test failures in C# NUnit projects. Use when a Selenium test fails — reads the error message and stack trace, identifies the root cause, and suggests a C# fix. Triggered automatically from TearDown when TestStatus is Failed.
license: MIT
metadata:
  author: lizethheredia
  version: "1.0"
  framework: Selenium WebDriver + NUnit + C#
---

# Selenium Failure Analyzer

Analyzes failed Selenium tests and generates a human-readable report with root cause and suggested fix.

## When this skill activates

This skill is triggered automatically when a NUnit test fails. It receives:
- Test name
- Error message from NUnit assertion
- Stack trace

## Analysis steps

1. Read the error message — identify if it's an assertion failure, timeout, or element not found
2. Read the stack trace — pinpoint the exact line and method that failed
3. Identify the root cause category:
   - **Selector issue** — element not found, NoSuchElementException
   - **Timing issue** — element not visible yet, StaleElementReferenceException
   - **Assertion failure** — wrong value returned, wrong page state
   - **Browser issue** — driver crash, session expired
4. Suggest a concrete C# fix

## Output format

Return a structured analysis with three sections:
- What failed and why
- Most likely root cause
- Suggested fix in C# (code snippet)

Keep under 200 words. Be direct and technical.

## Common Selenium failure patterns

| Error | Root cause | Fix |
|-------|-----------|-----|
| `NoSuchElementException` | Selector wrong or element not loaded | Use WebDriverWait before FindElement |
| `StaleElementReferenceException` | DOM changed after element was found | Re-find element after page change |
| `WebDriverTimeoutException` | Element never appeared in N seconds | Increase timeout or check selector |
| `AssertionException` | Wrong page state after action | Add explicit wait before assertion |
| `InvalidOperationException` | Alert present blocking interaction | Handle alert with SwitchTo().Alert() |

## Integration

This skill is called from `Drivers/AIFailureAnalyzer.cs` in the CSharpSeleniumQA project.
Results are attached to the Allure report as "AI Failure Analysis" text attachment.