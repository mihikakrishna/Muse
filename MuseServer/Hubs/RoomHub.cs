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

        public async Task CreateRoom(RoomMessage roomMessage)
        {
            
            var random = new Random();
            var roomCode = _freeRooms.ElementAt(random.Next(_freeRooms.Count));

            _freeRooms.Remove(roomCode);
            _usedRooms.Add(roomCode);
            _roomSizes[roomCode] = 1;

            Console.WriteLine("Created room: " + roomCode);
            roomMessage.RoomCode = roomCode;

            await Clients.Client(Context.ConnectionId).SendAsync("ReceivedCreatedRoom", roomMessage);
            
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task JoinRoom(RoomMessage roomMessage)
        {
            if (ValidateRoom(roomMessage))
            {
                _roomSizes[roomMessage.RoomCode]++;
                await Clients.Client(roomMessage.RoomCode).SendAsync("JoinedRoom");
                await Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
            }
            else
            {
                Console.WriteLine($"Key [{roomMessage.RoomCode}] not found in _usedRooms");
            }
        }

        public async Task LeaveRoom(RoomMessage roomMessage)
        {
            if (ValidateRoom(roomMessage))
            {
                _roomSizes[roomMessage.RoomCode]--;
                if (_roomSizes[roomMessage.RoomCode] == 0)
                {
                    _usedRooms.Remove(roomMessage.RoomCode);
                    _freeRooms.Add(roomMessage.RoomCode);
                }
                await Clients.Client(roomMessage.RoomCode).SendAsync("LeftRoom");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
            }
            else
            {
                Console.WriteLine($"Key [{roomMessage.RoomCode}] not found in _usedRooms");
            }  
        }

        public bool ValidateRoom(RoomMessage roomMessage)
        {
            return _usedRooms.Contains(roomMessage.RoomCode);
        }

    }
}
