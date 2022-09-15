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

            for (int roomCode = 0; roomCode < 10000; roomCode++)
            {
                _freeRooms.Add(roomCode.ToString("D4"));
            }
        }

        public Task CreateRoom()
        {
            var random = new Random();
            var roomCode = _freeRooms.ElementAt(random.Next(_freeRooms.Count));

            _freeRooms.Remove(roomCode);
            _usedRooms.Add(roomCode);
            _roomSizes[roomCode] = 1;

            return Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public Task JoinRoom(RoomMessage roomMessage)
        {
            if (!ValidateRoom(roomMessage))
            {
                Console.WriteLine($"Key [{roomMessage.roomCode}] not found in _usedRooms");
                return Task.CompletedTask;
            }

            _roomSizes[roomMessage.roomCode]++;
            return Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.roomCode);
        }

        public Task LeaveRoom(RoomMessage roomMessage)
        {
            if (!ValidateRoom(roomMessage))
            {
                Console.WriteLine($"Key [{roomMessage.roomCode}] not found in _usedRooms");
                return Task.CompletedTask;
            }

            _roomSizes[roomMessage.roomCode]--;
            if (_roomSizes[roomMessage.roomCode] == 0)
            {
                _usedRooms.Remove(roomMessage.roomCode);
                _freeRooms.Add(roomMessage.roomCode);
            }
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.roomCode);
        }

        public bool ValidateRoom(RoomMessage roomMessage)
        {
            return _usedRooms.Contains(roomMessage.roomCode);
        }

    }
}
