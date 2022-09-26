using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseDomain.Models;

namespace MuseClient.Services;
public class SignalRMuseService
{
    private readonly HubConnection _connection;

    public event Action<RoomMessage>? CreatedRoom;
    public event Action<bool>? ValidatedRoom;
    public event Action? JoinedRoom;
    public event Action? LeftRoom;
    public event Action<ChatMessage>? MessageReceived;

    public SignalRMuseService(HubConnection connection)
    {
        _connection = connection;

        _connection.On<RoomMessage>("CreatedRoom", (RoomMessage)
            => CreatedRoom?.Invoke(RoomMessage));

        _connection.On<bool>("ValidatedRoom", (IsRoomValid)
            => ValidatedRoom?.Invoke(IsRoomValid));

        _connection.On("JoinedRoom", ()
            => JoinedRoom?.Invoke());

        _connection.On("LeftRoom", ()
            => LeftRoom?.Invoke());

        _connection.On<ChatMessage>("ReceiveMessage", (chatMessage)
            => MessageReceived?.Invoke(chatMessage));
    }

    public async Task Connect()
    {
        await _connection.StartAsync();
        Console.WriteLine("Connected");
    }

    public async Task CreateRoom()
    {
        await _connection.SendAsync("CreateRoom");
        Console.WriteLine("Created Room");
    }

    public async Task JoinRoom(RoomMessage roomMessage)
    {
        await _connection.SendAsync("JoinRoom", roomMessage);
        Console.WriteLine("Joined Room");
    }

    public async Task LeaveRoom(RoomMessage roomMessage)
    {
        await _connection.SendAsync("LeaveRoom", roomMessage);
        Console.WriteLine("Left Room");
    }

    public async Task ValidateRoom(RoomMessage roomMessage)
    {
        await _connection.SendAsync("ValidateRoom", roomMessage);
    }

    public async Task SendMessage(ChatMessage message)
    {
        await _connection.SendAsync("SendMessage", message);
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
}
