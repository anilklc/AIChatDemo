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

        public async Task SendMessage(string userName, string message)
        {
            var userId = Context.GetHttpContext().Request.Cookies["UserId"] ?? "Unknown User";

            var response = await _chatService.GetChatGPTResponse(message);

            await Clients.Caller.SendAsync("ReceiveMessage", userName, message);
            await Clients.Caller.SendAsync("ReceiveMessage", "ChatGPT", response);
        }
    }
}






