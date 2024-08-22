using AIChatDemo.API.Models;
using System.Net;
using System.Net.Http.Headers;

namespace AIChatDemo.API.Services
{
    public class AIChatService
    {
        private readonly HttpClient _httpClient;
        public AIChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetChatGPTResponse(string message)
        {

            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { new { role = "user", content = message } }
            };

            var response = await _httpClient.PostAsJsonAsync("",request);
            if (response.StatusCode != HttpStatusCode.TooManyRequests)
            {
                var content = await response.Content.ReadFromJsonAsync<ChatGPTResponse>();
                return content.Choices.FirstOrDefault()?.Message.Content;
            }
            else 
            {
                return $"Çok fazla istek gönderdiniz. Lütfen bir süre sonra tekrar deneyiniz.";
            }

        }

    }
}
