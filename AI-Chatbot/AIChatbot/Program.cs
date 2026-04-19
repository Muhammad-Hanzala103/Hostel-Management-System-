using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace AIChatbot;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    private const string OpenAiApiKey = "YOUR_OPENAI_API_KEY"; // Replace with your API key
    private const string OpenAiUrl = "https://api.openai.com/v1/chat/completions";

    static async Task Main(string[] args)
    {
        Console.WriteLine("🤖 AI Chatbot - Powered by OpenAI");
        Console.WriteLine("Type 'exit' to quit\n");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OpenAiApiKey);

        var messages = new List<object>
        {
            new { role = "system", content = "You are a helpful AI assistant for a hostel management system. Help users with queries about hostel operations, student management, and general assistance." }
        };

        while (true)
        {
            Console.Write("You: ");
            string? userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
                continue;

            if (userInput.ToLower() == "exit")
                break;

            messages.Add(new { role = "user", content = userInput });

            try
            {
                var response = await GetChatResponse(messages);
                Console.WriteLine($"AI: {response}\n");
                messages.Add(new { role = "assistant", content = response });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }
    }

    private static async Task<string> GetChatResponse(List<object> messages)
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = messages,
            max_tokens = 150,
            temperature = 0.7
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(OpenAiUrl, content);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var responseJson = JsonDocument.Parse(responseString);

        return responseJson.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "Sorry, I couldn't generate a response.";
    }
}
