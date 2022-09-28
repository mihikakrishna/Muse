using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;

namespace MuseServer.Hubs
{
    internal class MuseHub : Hub
    {


        private readonly static Lazy<HashSet<string>> FreeRooms
            = new(() => new HashSet<string>(Enumerable.Range(0, 9999).ToList().Select(x => x.ToString("D4"))));

        private readonly static Lazy<HashSet<string>> UsedRooms
            = new(() => new HashSet<string>());

        private readonly static Lazy<Dictionary<string, int>> RoomSizes
            = new(() => new Dictionary<string, int>());

        public async Task CreateRoom()
        {
            var random = new Random();
            var roomCode = FreeRooms.Value.ElementAt(random.Next(FreeRooms.Value.Count));

            FreeRooms.Value.Remove(roomCode);
            UsedRooms.Value.Add(roomCode);
            RoomSizes.Value[roomCode] = 1;

            var createdRoomMessage = new RoomMessage(roomCode);

            await Clients.Client(Context.ConnectionId).SendAsync("CreatedRoom", createdRoomMessage);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task JoinRoom(RoomMessage roomMessage)
        {
            if (!IsRoomUsed(roomMessage))
            {
                return;
            }

            RoomSizes.Value[roomMessage.RoomCode]++;
            await Clients.Client(Context.ConnectionId).SendAsync("JoinedRoom");
            await Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
        }

        public async Task LeaveRoom(RoomMessage roomMessage)
        {
            if (!IsRoomUsed(roomMessage))
            {
                return;
            }

            RoomSizes.Value[roomMessage.RoomCode]--;
            if (RoomSizes.Value[roomMessage.RoomCode] == 0)
            {
                UsedRooms.Value.Remove(roomMessage.RoomCode);
                FreeRooms.Value.Add(roomMessage.RoomCode);
            }

            await Clients.Client(Context.ConnectionId).SendAsync("LeftRoom");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.RoomCode);
            
            Console.WriteLine("Client has left room " + roomMessage.RoomCode);
        }

        public async Task ValidateRoom(RoomMessage roomMessage)
        {
            var isRoomValid = UsedRooms.Value.Contains(roomMessage.RoomCode) && RoomSizes.Value.ContainsKey(roomMessage.RoomCode);
            await Clients.Client(Context.ConnectionId).SendAsync("ValidatedRoom", isRoomValid);
        }

        public async Task SendMessage(ChatMessage chatMessage)
        {
            await Clients.Group(chatMessage.RoomCode).SendAsync("ReceiveMessage", chatMessage);
        }

        private bool IsRoomUsed(RoomMessage roomMessage)
        {
            if (!UsedRooms.Value.Contains(roomMessage.RoomCode))
            {
                Console.WriteLine($"Item [{roomMessage.RoomCode}] not found in UsedRooms");
                return false;
            }

            if (!RoomSizes.Value.ContainsKey(roomMessage.RoomCode))
            {
                Console.WriteLine($"Key [{roomMessage.RoomCode}] not found in RoomSizes");
                return false;
            }

            return true;
        }

    }
}
