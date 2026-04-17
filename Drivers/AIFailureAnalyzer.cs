using System.Text;
using System.Text.Json;

namespace CSharpSeleniumQA.Drivers
{
    public static class AIFailureAnalyzer
    {
        private static readonly HttpClient _client = new HttpClient();

        private static string LoadSkill()
        {
            var skillPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Skills",
                "selenium-failure-analyzer",
                "SKILL.md"
            );

            if (File.Exists(skillPath))
                return File.ReadAllText(skillPath);

            return "You are a QA automation expert analyzing Selenium test failures in C#.";
        }

        public static async Task<string> AnalyzeFailureAsync(
            string testName,
            string errorMessage,
            string stackTrace
        )
        {
            var apiKey = ConfigManager.AnthropicApiKey;
            if (string.IsNullOrEmpty(apiKey))
                return "AI analysis skipped — no API key configured.";

            var systemPrompt = LoadSkill();

            var userMessage = $"""
                A Selenium test failed. Analyze it and provide:
                1. What failed and why
                2. Most likely root cause
                3. Suggested fix in C#

                Test name: {testName}
                Error message: {errorMessage}
                Stack trace: {stackTrace}

                Keep it under 200 words. Be direct and technical.
                """;

            var requestBody = new
            {
                model = "claude-opus-4-6",
                max_tokens = 500,
                system = systemPrompt,
                messages = new[] { new { role = "user", content = userMessage } },
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
