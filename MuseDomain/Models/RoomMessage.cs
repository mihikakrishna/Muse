namespace MuseDomain.Models
{
    public class RoomMessage
    {
        private readonly string _roomCode;

        public RoomMessage(string roomCode)
        {
            _roomCode = roomCode;
        }

        public string roomCode => _roomCode;
    }
}
