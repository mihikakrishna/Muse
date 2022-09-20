using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;


namespace MuseServer.Hubs
{
    public class RoomHub : Hub
    {
        private readonly HashSet<string> _freeRooms;
        private readonly HashSet<string> _usedRooms;
        private readonly Dictionary<string, int> _roomSizes;

        public RoomHub()
        {
            _freeRooms = new HashSet<string>();
            _usedRooms = new HashSet<string>();
            _roomSizes = new Dictionary<string, int>();

            for (var roomCode = 0; roomCode < 10000; roomCode++)
            {
                _freeRooms.Add(roomCode.ToString("D4"));
            }
        }

        public async Task CreateRoom(RoomMessage roomMessage)
        {
            var random = new Random();
            var roomCode = _freeRooms.ElementAt(random.Next(_freeRooms.Count));

            _freeRooms.Remove(roomCode);
            _usedRooms.Add(roomCode);
            _roomSizes[roomCode] = 1;

            roomMessage.RoomCode = roomCode;
            
            await Clients.Client(Context.ConnectionId).SendAsync("CreatedRoom", roomMessage);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task JoinRoom(RoomMessage roomMessage)
        {
            if (!IsRoomUsed(roomMessage))
            {
                return;
            }
            
            _roomSizes[roomMessage.RoomCode]++;
            await Clients.Client(Context.ConnectionId).SendAsync("JoinedRoom");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
        }

        public async Task LeaveRoom(RoomMessage roomMessage)
        {
            if (!IsRoomUsed(roomMessage))
            {
                return;
            }
            
            _roomSizes[roomMessage.RoomCode]--;
            if (_roomSizes[roomMessage.RoomCode] == 0)
            {
                _usedRooms.Remove(roomMessage.RoomCode);
                _freeRooms.Add(roomMessage.RoomCode);
            }
            
            await Clients.Client(roomMessage.RoomCode).SendAsync("LeftRoom");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
        }

        public async Task ValidateRoom(RoomMessage roomMessage)
        {
            var isRoomValid = _usedRooms.Contains(roomMessage.RoomCode) && _roomSizes.ContainsKey(roomMessage.RoomCode);
            await Clients.Client(Context.ConnectionId).SendAsync("ValidatedRoom", isRoomValid);
        }

        private bool IsRoomUsed(RoomMessage roomMessage)
        {
            if (!_usedRooms.Contains(roomMessage.RoomCode))
            {
                Console.WriteLine($"Item [{roomMessage.RoomCode}] not found in _usedRooms");
                return false;
            }
            
            if (!_roomSizes.ContainsKey(roomMessage.RoomCode))
            {
                Console.WriteLine($"Key [{roomMessage.RoomCode}] not found in _roomSizes");
                return false;
            }

            return true;
        }
        
    }
}
