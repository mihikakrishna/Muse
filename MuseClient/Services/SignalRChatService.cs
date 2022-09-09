using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseDomain.Models;

namespace MuseClient.Services;
public class SignalRChatService
{
    private readonly HubConnection _connection;

    public event Action<ChatMessage>? MessageReceived;
    public SignalRChatService(HubConnection connection)
    {
        _connection = connection;
        _connection.On<ChatMessage>("RecieveMessage", (chatMessage) => MessageReceived?.Invoke(chatMessage));
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
