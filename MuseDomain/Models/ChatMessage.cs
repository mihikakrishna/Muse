namespace MuseDomain.Models;

public class ChatMessage
{
    public string Message { get; }

    public ChatMessage(string message)
    {
        Message = message;
    }
}