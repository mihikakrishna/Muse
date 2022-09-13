using Microsoft.AspNetCore.SignalR;
using MuseDomain.Models;


namespace MuseServer.Hubs
{
    public class RoomHub : Hub
    {
        private HashSet<string> _roomCodes = new HashSet<string>();

        public Task CreateRoom()
        {
            Random _rdm = new Random();
            var roomCode = _rdm.Next(0, 9999).ToString();
            return Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public Task JoinRoom(RoomMessage roomMessage)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomMessage.RoomId);
            //await Clients.Group(roomCode).SendAsync("ReceiveMessage", user, " joined to " + roomCode).ConfigureAwait(true);
        }

        public Task LeaveRoom(RoomMessage roomMessage)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomMessage.RoomId);
        }

        public bool ValidateRoom(RoomMessage roomMessage)
        {
            return _roomCodes.Contains(roomMessage.RoomId);
        }
    }
}
