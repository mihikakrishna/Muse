using System;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Services;
using MuseClient.ViewModels;
using ReactiveUI;

namespace MuseClient.Stores;

public class NavigationStore : StoreBase
{
    private ViewModelBase _currentViewModel;
    private SignalRChatService _chatService;
    private ChatViewModel _chatViewModel;

    public event Action? CurrentViewModelIsChanged;


    public NavigationStore()
    {
        _currentViewModel = new HomeWindowViewModel(this);
        _chatService = new SignalRChatService(new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/chatHub")
                    .Build());
        _chatViewModel = ChatViewModel.CreateConnectedViewModel(this, _chatService);
    }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentViewModel, value);
            OnCurrentViewModelIsChanged();
        }
    }

    public ChatViewModel ChatViewModel
    {
        get => _chatViewModel;
    }

    private void OnCurrentViewModelIsChanged()
    {
        CurrentViewModelIsChanged?.Invoke();
    }
}