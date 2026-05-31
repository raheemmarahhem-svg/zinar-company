using System.Text;
using System.Text.Json;

namespace ZinarCompany.Services
{
    public class GeminiService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public GeminiService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<string> AskAsync(string userMessage)
        {
            var apiKey = _config["Gemini:ApiKey"];
            var model = _config["Gemini:Model"] ?? "gemini-2.5-flash";

            if (string.IsNullOrWhiteSpace(apiKey))
                return "AIzaSyC5WpIslAhs8eg8_o5zuFql27bMD3W302c";

            var systemPrompt =
@"You are Zinar Company Support Assistant (Duhok, Iraq).
Only answer about: Zinar Company, Aluminum/PVC/Wood systems, catalog navigation, installation tips, maintenance.
If outside scope say: I can only help with Zinar Company products and support questions.
Keep answers short.";

            var prompt = systemPrompt + "\n\nUser: " + userMessage + "\nAssistant:";

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

            var payload = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _http.PostAsync(url, content);
            var body = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                return $"AI service error: {res.StatusCode} - {body}";
            }

            using var doc = JsonDocument.Parse(body);

            var text =
                doc.RootElement
                   .GetProperty("candidates")[0]
                   .GetProperty("content")
                   .GetProperty("parts")[0]
                   .GetProperty("text")
                   .GetString();

            return string.IsNullOrWhiteSpace(text) ? "Please try again." : text!;
        }
    }
}