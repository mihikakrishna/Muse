using System;
using MuseClient.Services;
using System.Windows.Input;
using MuseClient.Commands;
using MuseClient.Enums;
using MuseClient.Stores;
using MuseDomain.Models;

namespace MuseClient.ViewModels;
public class ChatViewModel : ViewModelBase
{
    public string Messages { get; set; }
    public ICommand SendChatMessageCommand { get; }

    private string _errorMessage = string.Empty;
    private bool _isConnected;
    private readonly NavigationStore _navigationStore;
    private readonly SignalRChatService _chatService;

    public ChatViewModel(NavigationStore navigationStore, SignalRChatService chatService)
    {
        _navigationStore = navigationStore;
        _chatService = chatService;
        SendChatMessageCommand = new SendChatMessageCommand(this, chatService);

        Messages = new string(Array.Empty<char>());

        chatService.MessageRecieved += ChatService_MessageReceived;
    }
    private void ChatService_MessageReceived(ChatMessage chatMessage)
    {
        Messages = chatMessage.Message;
        Console.WriteLine(chatMessage.Message);
    }
    public static ChatViewModel CreateConnectedViewModel(NavigationStore navigationStore, SignalRChatService chatService)
    {
        ChatViewModel viewModel = new ChatViewModel(navigationStore, chatService);

        chatService.Connect().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Console.WriteLine("Unable to connect to chat hub");
            }
        });

        return viewModel;
    }
    public string ErrorMessage
    {
        get
        {
            return _errorMessage;
        }
        set
        {
            _errorMessage = value;
        }
    }
    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
    public bool IsConnected
    {
        get
        {
            return _isConnected;
        }
        set
        {
            _isConnected = value;
            Console.WriteLine("Connected to Server");
        }
    }

    public void SwitchPage(string page)
    {
        switch (page)
        {
            case Pages.ListenTogetherPage:
                _navigationStore.CurrentViewModel = new ListenTogetherWindowViewModel(_navigationStore, _chatService);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }

}
