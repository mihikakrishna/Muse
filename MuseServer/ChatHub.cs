using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;

namespace MuseServer
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(ChatMessage chatMessage)
        {
            await Clients.All.SendAsync("RecieveMessage", chatMessage);
        }
    }
}
