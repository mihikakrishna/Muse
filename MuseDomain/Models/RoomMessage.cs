namespace MuseDomain.Models
{
    public class RoomMessage
    {
        private string _roomCode;

        public RoomMessage(string roomCode)
        {
            _roomCode = roomCode;
        }

        public string RoomCode
        {
            get => _roomCode;
            set => _roomCode = value;
        }
    }
}
