using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Commands;
using MuseClient.Enums;
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

    public async Task SwitchPage(string page)
    {
        switch (page)
        {
            case Pages.HomePage:
                await _chatService.Disconnect();
                _navigationStore.CurrentViewModel = new HomeWindowViewModel(_navigationStore);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }

}
