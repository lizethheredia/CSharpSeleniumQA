using System.Net.Http;
using System.Text;
using System.Text.Json;
using NUnit.Framework;

namespace CSharpSeleniumQA.Drivers
{
    public static class AIFailureAnalyzer
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<string> AnalyzeFailureAsync(
            string testName,
            string errorMessage,
            string stackTrace
        )
        {
            var apiKey = ConfigManager.AnthropicApiKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                return "AI analysis skipped — no API key configured.";
            }

            var prompt = $"""
                You are a QA automation expert analyzing a Selenium test failure in C#.

                Test name: {testName}
                Error message: {errorMessage}
                Stack trace: {stackTrace}

                Provide a concise analysis with:
                1. What failed and why
                2. The most likely root cause
                3. Suggested fix in C#

                Keep it under 200 words. Be direct and technical.
                """;

            var requestBody = new
            {
                model = "claude-opus-4-6",
                max_tokens = 500,
                messages = new[] { new { role = "user", content = prompt } },
            };

            var json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.anthropic.com/v1/messages"
            );
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Add("anthropic-version", "2023-06-01");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseBody);
            return doc.RootElement.GetProperty("content")[0].GetProperty("text").GetString()
                ?? "No analysis returned.";
        }
    }
}
