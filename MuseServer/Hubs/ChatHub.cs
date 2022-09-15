using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;

namespace MuseServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage chatMessage)
        {
            await Clients.Group(chatMessage.RoomCode).SendAsync("ReceiveMessage", chatMessage);
        }
    }
}
