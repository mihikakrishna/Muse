using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;

namespace MuseClient.Commands;

public class NavigateToHomeWindowCommand : ICommand
{
    private readonly SignalRChatService _chatService;
    private readonly NavigationStore _navigationStore;

    public NavigateToHomeWindowCommand(SignalRChatService chatService, NavigationStore navigationStore)
    {
        _chatService = chatService;
        _navigationStore = navigationStore;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        await _chatService.Disconnect();
        _navigationStore.CurrentViewModel = new HomeWindowViewModel(_navigationStore);
    }
}