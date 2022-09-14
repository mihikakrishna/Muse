namespace MuseDomain.Models;

public class ChatMessage
{
    private readonly string _message;
    private readonly string _username;
    private readonly string _roomCode;
    private readonly DateTime _timestamp;

    public ChatMessage(
        string message,
        string username,
        string roomCode,
        DateTime timestamp)
    {
        _message = message;
        _username = username;
        _roomCode = roomCode;
        _timestamp = timestamp;
    }

    public string Message => _message;
    public string Username => _username;
    public string RoomCode => _roomCode;
    public DateTime Timestamp => _timestamp;
}