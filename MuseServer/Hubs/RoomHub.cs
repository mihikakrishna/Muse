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

        public async Task CreateRoom()
        {
            var random = new Random();
            var roomCode = _freeRooms.ElementAt(random.Next(_freeRooms.Count));

            _freeRooms.Remove(roomCode);
            _usedRooms.Add(roomCode);
            _roomSizes[roomCode] = 1;

            await Clients.Client(roomCode).SendAsync("RecievedCreatedRoom", roomCode);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task JoinRoom(RoomMessage roomMessage)
        {
            if (ValidateRoom(roomMessage))
            {
                _roomSizes[roomMessage.roomCode]++;
                await Clients.Client(roomMessage.roomCode).SendAsync("JoinedRoom");
                await Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.roomCode);
            }
            else
            {
                Console.WriteLine($"Key [{roomMessage.roomCode}] not found in _usedRooms");
            }
        }

        public async Task LeaveRoom(RoomMessage roomMessage)
        {
            if (ValidateRoom(roomMessage))
            {
                _roomSizes[roomMessage.roomCode]--;
                if (_roomSizes[roomMessage.roomCode] == 0)
                {
                    _usedRooms.Remove(roomMessage.roomCode);
                    _freeRooms.Add(roomMessage.roomCode);
                }
                await Clients.Client(roomMessage.roomCode).SendAsync("LeftRoom");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.roomCode);
            }
            else
            {
                Console.WriteLine($"Key [{roomMessage.roomCode}] not found in _usedRooms");
            }  
        }

        public bool ValidateRoom(RoomMessage roomMessage)
        {
            return _usedRooms.Contains(roomMessage.roomCode);
        }

    }
}
