using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MuseClient.Commands;
using MuseClient.Services;
using MuseClient.Stores;
using MuseDomain.Models;
using ReactiveUI;

namespace MuseClient.ViewModels;
public class ListenTogetherWindowViewModel : ViewModelBase
{
    private readonly string _username;
    private readonly string _roomCode;
    private string _errorMessage;
    private bool _isConnected;
    private string _chatInput;
    public ObservableCollection<ChatMessage> Messages { get; }
    public ObservableCollection<string> Songs { get; }
    public ICommand SendChatMessageCommand { get; }
    public ICommand NavigateToHomeWindowCommand { get; }

    public ListenTogetherWindowViewModel(
        NavigationStore navigationStore,
        SignalRMuseService signalRMuseService,
        string username,
        string roomCode)
    {
        _username = username;
        _roomCode = roomCode;
        _errorMessage = string.Empty;
        _chatInput = string.Empty;

        Songs = new ObservableCollection<string>();
        Messages = new ObservableCollection<ChatMessage>();

        // dummy data to test (delete once chat & song features work)
        Songs.Add("Song 1");
        Songs.Add("Song 2");
        Songs.Add("Song 3");

        SendChatMessageCommand = new SendChatMessageCommand(this, signalRMuseService);
        NavigateToHomeWindowCommand = new NavigateToHomeWindowCommand(this, signalRMuseService, navigationStore);

        signalRMuseService.MessageReceived += SignalRMuseService_MessageReceived;
    }

    public string Username => _username;

    public string RoomCode => _roomCode;

    public string ChatInput
    {
        get => _chatInput;
        set => this.RaiseAndSetIfChanged(ref _chatInput, value);
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

    private void SignalRMuseService_MessageReceived(ChatMessage chatMessage)
    {
        Messages.Add(chatMessage);
        Console.WriteLine(chatMessage.Message);
    }

}
