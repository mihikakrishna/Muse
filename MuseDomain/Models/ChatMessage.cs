namespace MuseDomain.Models;

public class ChatMessage
{
    private readonly string _message;

    public ChatMessage(string message)
    {
        _message = message;
    }

    public string Message => _message;
}