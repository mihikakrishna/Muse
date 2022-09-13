namespace MuseDomain.Models
{
    public class RoomMessage
    {
        private readonly string _roomId;

        public RoomMessage(string roomId)
        {
            _roomId = roomId;
        }

        public string RoomId => _roomId;
    }
}
