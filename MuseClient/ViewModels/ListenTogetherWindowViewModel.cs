using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Commands;
using MuseClient.Services;
using MuseClient.Stores;
using MuseDomain.Models;
using ReactiveUI;

namespace MuseClient.ViewModels;
public class ListenTogetherWindowViewModel : ViewModelBase
{
    private string _errorMessage;
    private bool _isConnected;
    private readonly NavigationStore _navigationStore;

    private SignalRChatService _chatService;
    public ObservableCollection<string> Messages { get; }
    public ICommand SendChatMessageCommand { get; }
    public ICommand NavigateToHomeWindowCommand { get; }

    public static ListenTogetherWindowViewModel CreateConnectedViewModel(NavigationStore navigationStore)
    {
        var hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/chatHub")
                    .Build();
        var chatService = new SignalRChatService(hubConnection);
        var viewModel = new ListenTogetherWindowViewModel(navigationStore, chatService);

        chatService.Connect().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Console.WriteLine("An exception has occured");
            }
        });

        return viewModel;
    }

    private ListenTogetherWindowViewModel(NavigationStore navigationStore, SignalRChatService chatService)
    {
        _errorMessage = string.Empty;
        _navigationStore = navigationStore;
        _chatService = chatService;

        Messages = new ObservableCollection<string>();
        SendChatMessageCommand = new SendChatMessageCommand(this, chatService);
        NavigateToHomeWindowCommand = new NavigateToHomeWindowCommand(chatService, navigationStore);
        
        chatService.MessageReceived += ChatService_MessageReceived;
    }
    private void ChatService_MessageReceived(ChatMessage chatMessage)
    {
        Messages.Add(chatMessage.Message);
        Console.WriteLine(chatMessage.Message);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool IsConnected
    {
        get => _isConnected;
        set => this.RaiseAndSetIfChanged(ref _isConnected, value);
    }

}
