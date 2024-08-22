using AIChatDemo.API.Services;
using Microsoft.AspNetCore.SignalR;

namespace AIChatDemo.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AIChatService _chatService;
        public ChatHub(AIChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string user, string message)
        {    
            await Clients.All.SendAsync("ReceiveMessage", user, message);

            var response = await _chatService.GetChatGPTResponse(message);
            await Clients.All.SendAsync("ReceiveMessage", "ChatGPT", response);
        }

    }
}
