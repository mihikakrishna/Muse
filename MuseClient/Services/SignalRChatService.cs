using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseDomain.Models;
using ReactiveUI;

namespace MuseClient.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<ChatMessage> MessageRecieved;
        public SignalRChatService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<ChatMessage>("RecieveMessage", (chatMessage) => MessageRecieved?.Invoke(chatMessage));
        }
        public async Task Connect()
        {
            await _connection.StartAsync();
        }
        public async Task SendMessage(ChatMessage message)
        {
            await _connection.SendAsync("SendMessage", message);
        }
    }
}
