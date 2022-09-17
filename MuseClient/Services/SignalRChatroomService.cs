using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseDomain.Models;

namespace MuseClient.Services
{
    public class SignalRChatroomService
    {
        private readonly HubConnection _connection;

        public event Action<RoomMessage>? ReceivedCreatedRoom;
        public event Action<RoomMessage>? JoinedRoom;
        public event Action<RoomMessage>? LeftRoom;
        public SignalRChatroomService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<RoomMessage>("ReceivedCreatedRoom", (RoomMessage) 
                => ReceivedCreatedRoom?.Invoke(RoomMessage));
            _connection.On<RoomMessage>("JoinedRoom", (RoomMessage) 
                => JoinedRoom?.Invoke(RoomMessage));
            _connection.On<RoomMessage>("LeftRoom", (RoomMessage) 
                => LeftRoom?.Invoke(RoomMessage));
        }
        public async Task Connect()
        {
            await _connection.StartAsync();
            Console.WriteLine("Connected");
        }

        public async Task CreateRoom(RoomMessage roomMessage)
        {
            await _connection.SendAsync("CreateRoom", roomMessage);
            Console.WriteLine("Created Room");
        }

        public async Task JoinRoom(RoomMessage roomMessage)
        {
            await _connection.SendAsync("JoinRoom", roomMessage.RoomCode);
            Console.WriteLine("Joined Room");
        }

        public async Task LeaveRoom(RoomMessage roomMessage)
        {
            await _connection.SendAsync("LeaveRoom", roomMessage.RoomCode);
            Console.WriteLine("Left Room");
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
}
