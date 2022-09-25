using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseDomain.Models;

namespace MuseClient.Services;
public class SignalRMuseService
{
    private readonly HubConnection _connection;

    public event Action<ChatMessage>? MessageReceived;
    public SignalRMuseService(HubConnection connection)
    {
        _connection = connection;
        _connection.On<ChatMessage>("ReceiveMessage", (chatMessage) => MessageReceived?.Invoke(chatMessage));
    }
    public async Task Connect()
    {
        await _connection.StartAsync();
        Console.WriteLine("Connected");
    }
    public async Task Disconnect()
    {
        try
        {
            await _connection.StopAsync();
            Console.WriteLine("Disconnected");
        }
        finally
        {
            await _connection.DisposeAsync();
            Console.WriteLine("Disposed");
        }

    }
    public async Task SendMessage(ChatMessage message)
    {
        await _connection.SendAsync("SendMessage", message);
    }
}
