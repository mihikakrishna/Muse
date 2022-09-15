using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;


namespace MuseServer.Hubs
{
    public class RoomHub : Hub
    {
        private HashSet<string> _freeRooms;
        private HashSet<string> _usedRooms;
        private Dictionary<string, int> _roomSizes;

        public RoomHub()
        {
            _freeRooms = new HashSet<string>();
            _usedRooms = new HashSet<string>();
            _roomSizes = new Dictionary<string, int>();

            for (int i = 0; i < 10000; i++)
            {
                _freeRooms.Add(i.ToString("D4"));
            }
        }

        public Task CreateRoom()
        {
            var random = new Random();
            var roomId = _freeRooms.ElementAt(random.Next(_freeRooms.Count));

            _freeRooms.Remove(roomId);
            _usedRooms.Add(roomId);
            _roomSizes[roomId] = 1;

            return Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public Task JoinRoom(RoomMessage roomMessage)
        {
            _roomSizes[roomMessage.RoomId]++;
            return Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.RoomId);
        }

        public Task LeaveRoom(RoomMessage roomMessage)
        {
            _roomSizes[roomMessage.RoomId]--;
            if (_roomSizes[roomMessage.RoomId] == 0)
            {
                _usedRooms.Remove(roomMessage.RoomId);
                _freeRooms.Add(roomMessage.RoomId);
            }
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.RoomId);
        }

        public bool ValidateRoom(RoomMessage roomMessage)
        {
            return _usedRooms.Contains(roomMessage.RoomId);
        }
    }
}
