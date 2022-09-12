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
    private string _chatInput;
    public ObservableCollection<string> Messages { get; }
    public ObservableCollection<string> Songs { get; }
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
        _chatInput = string.Empty;

        Songs = new ObservableCollection<string>();
        Messages = new ObservableCollection<string>();

        // dummy data to test (delete once chat & song features work)
        Songs.Add("Song 1");
        Songs.Add("Song 2");
        Songs.Add("Song 3");

        SendChatMessageCommand = new SendChatMessageCommand(this, chatService);
        NavigateToHomeWindowCommand = new NavigateToHomeWindowCommand(chatService, navigationStore);

        chatService.MessageReceived += ChatService_MessageReceived;
    }
    public string ChatInput
    {
        get => _chatInput;
        set => this.RaiseAndSetIfChanged(ref _chatInput, value);
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
