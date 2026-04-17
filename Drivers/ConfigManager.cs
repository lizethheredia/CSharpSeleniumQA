using Microsoft.Extensions.Configuration;

namespace CSharpSeleniumQA.Drivers
{
    public class ConfigManager
    {
        private static IConfiguration _config;

        static ConfigManager()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.local.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string BaseUrl => _config["BaseUrl"]!;
        public static bool Headless => bool.Parse(_config["Headless"] ?? "false");
        public static BrowserType Browser =>
            Enum.Parse<BrowserType>(_config["Browser"] ?? "Chrome");

        public static string ValidUsername => _config["Credentials:ValidUsername"]!;
        public static string ValidPassword => _config["Credentials:ValidPassword"]!;
        public static string InvalidUsername => _config["Credentials:InvalidUsername"]!;
        public static string InvalidPassword => _config["Credentials:InvalidPassword"]!;
        public static string AnthropicApiKey => _config["AnthropicApiKey"] ?? "";
    }
}
