using System;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Services;
using MuseClient.ViewModels;
using ReactiveUI;

namespace MuseClient.Stores;

public class NavigationStore : StoreBase
{
    private ViewModelBase _currentViewModel;
    public event Action? CurrentViewModelIsChanged;
    public SignalRChatService _chatService;


    public NavigationStore()
    {
        _currentViewModel = new HomeWindowViewModel(this);
        _chatService = new SignalRChatService(new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/chatHub")
                    .Build());
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

     

    private void OnCurrentViewModelIsChanged()
    {
        CurrentViewModelIsChanged?.Invoke();
    }
}