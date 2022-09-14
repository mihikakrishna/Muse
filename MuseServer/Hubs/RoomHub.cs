using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;


namespace MuseServer.Hubs
{
    public class RoomHub : Hub
    {
        private HashSet<string> _roomCodes;

        public RoomHub()
        {
            _roomCodes = new HashSet<string>();
        }

        public Task CreateRoom()
        {
            var random = new Random();
            var roomCode = random.Next(0, 10000).ToString("D4");
            while (_roomCodes.Contains(roomCode))
            {
                roomCode = random.Next(0, 10000).ToString("D4");
            }
            _roomCodes.Add(roomCode);
            return Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public Task JoinRoom(RoomMessage roomMessage)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.RoomId);
        }

        public Task LeaveRoom(RoomMessage roomMessage)
        {
            _roomCodes.Remove(roomMessage.RoomId);
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.RoomId);
        }

        public bool ValidateRoom(RoomMessage roomMessage)
        {
            return _roomCodes.Contains(roomMessage.RoomId);
        }
    }
}
