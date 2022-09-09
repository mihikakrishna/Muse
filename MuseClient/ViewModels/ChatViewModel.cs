using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MuseClient.Commands;
using MuseClient.Enums;
using MuseClient.Services;
using MuseClient.Stores;
using MuseDomain.Models;
using ReactiveUI;

namespace MuseClient.ViewModels;
public class ChatViewModel : ViewModelBase
{
    private string _errorMessage;
    private bool _isConnected;
    private readonly NavigationStore _navigationStore;
    private readonly SignalRChatService _chatService;
    public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();
    public ICommand SendChatMessageCommand { get; }

    public ChatViewModel(NavigationStore navigationStore, SignalRChatService chatService)
    {
        _errorMessage = string.Empty;
        _navigationStore = navigationStore;
        _chatService = chatService;
        SendChatMessageCommand = new SendChatMessageCommand(this, chatService);

        chatService.MessageReceived += ChatService_MessageReceived;
    }
    private void ChatService_MessageReceived(ChatMessage chatMessage)
    {
        Messages.Add(chatMessage.Message);
        Console.WriteLine(chatMessage.Message);
    }
    public static ChatViewModel CreateConnectedViewModel(NavigationStore navigationStore, SignalRChatService chatService)
    {
        var viewModel = new ChatViewModel(navigationStore, chatService);

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
            this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }
    }
    public bool IsConnected
    {
        get
        {
            return _isConnected;
        }
        set
        {
            this.RaiseAndSetIfChanged(ref _isConnected, value);
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
