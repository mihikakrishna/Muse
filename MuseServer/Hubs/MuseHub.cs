using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;

namespace MuseServer.Hubs
{
    internal class MuseHub : Hub
    {


        private readonly static Lazy<HashSet<string>> _freeRooms
            = new Lazy<HashSet<string>>(() => new HashSet<string>(Enumerable.Range(0, 9999).ToList().Select(x => x.ToString("D4"))));

        private readonly static Lazy<HashSet<string>> _usedRooms
            = new Lazy<HashSet<string>>(() => new HashSet<string>());

        private readonly static Lazy<Dictionary<string, int>> _roomSizes
            = new Lazy<Dictionary<string, int>>(() => new Dictionary<string, int>());

        public async Task CreateRoom()
        {
            //ValidateLazyGenerated();

            var random = new Random();
            var roomCode = _freeRooms.Value.ElementAt(random.Next(_freeRooms.Value.Count));

            _freeRooms.Value.Remove(roomCode);
            _usedRooms.Value.Add(roomCode);
            _roomSizes.Value[roomCode] = 1;

            var createdRoomMessage = new RoomMessage(roomCode);

            await Clients.Client(Context.ConnectionId).SendAsync("CreatedRoom", createdRoomMessage);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task JoinRoom(RoomMessage roomMessage)
        {
            //ValidateLazyGenerated();

            if (!IsRoomUsed(roomMessage))
            {
                return;
            }

            _roomSizes.Value[roomMessage.RoomCode]++;
            await Clients.Client(Context.ConnectionId).SendAsync("JoinedRoom");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
        }

        public async Task LeaveRoom(RoomMessage roomMessage)
        {
            ValidateLazyGenerated();

            if (!IsRoomUsed(roomMessage))
            {
                return;
            }

            _roomSizes.Value[roomMessage.RoomCode]--;
            if (_roomSizes.Value[roomMessage.RoomCode] == 0)
            {
                _usedRooms.Value.Remove(roomMessage.RoomCode);
                _freeRooms.Value.Add(roomMessage.RoomCode);
            }

            await Clients.Client(roomMessage.RoomCode).SendAsync("LeftRoom");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
        }

        public async Task ValidateRoom(RoomMessage roomMessage)
        {
            ValidateLazyGenerated();
            var isRoomValid = _usedRooms.Value.Contains(roomMessage.RoomCode) && _roomSizes.Value.ContainsKey(roomMessage.RoomCode);
            await Clients.Client(Context.ConnectionId).SendAsync("ValidatedRoom", isRoomValid);
        }

        public async Task SendMessage(ChatMessage chatMessage)
        {
            await Clients.Group(chatMessage.RoomCode).SendAsync("ReceiveMessage", chatMessage);
        }

        private bool IsRoomUsed(RoomMessage roomMessage)
        {
            if (!_usedRooms.Value.Contains(roomMessage.RoomCode))
            {
                Console.WriteLine($"Item [{roomMessage.RoomCode}] not found in _usedRooms");
                return false;
            }

            if (!_roomSizes.Value.ContainsKey(roomMessage.RoomCode))
            {
                Console.WriteLine($"Key [{roomMessage.RoomCode}] not found in _roomSizes");
                return false;
            }

            return true;
        }

        private void ValidateLazyGenerated()
        {
            if (!_freeRooms.IsValueCreated || !_usedRooms.IsValueCreated || !_roomSizes.IsValueCreated)
            {
                throw new Exception("Lazy static data members for _freeRooms, _usedRooms, and _roomSizes not generated.");
            }
        }

    }
}
