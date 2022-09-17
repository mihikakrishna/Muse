using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseDomain.Models;

namespace MuseClient.Services
{
    public class SignalRChatroomService
    {
        private readonly HubConnection _connection;

        public event Action<RoomMessage>? CreatedRoom;
        public event Action<RoomMessage>? JoinedRoom;
        public event Action<RoomMessage>? LeftRoom;
        public SignalRChatroomService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<RoomMessage>("CreatedRoom", (RoomMessage) 
                => CreatedRoom?.Invoke(RoomMessage));
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

        public async Task CreateRoom()
        {
            await _connection.SendAsync("CreateRoom");
            Console.WriteLine("Created Room");
        }

        public async Task JoinRoom(RoomMessage roomMessage)
        {
            await _connection.SendAsync("JoinRoom", roomMessage.roomCode);
            Console.WriteLine("Joined Room");
        }

        public async Task LeaveRoom(RoomMessage roomMessage)
        {
            await _connection.SendAsync("LeaveRoom", roomMessage.roomCode);
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
