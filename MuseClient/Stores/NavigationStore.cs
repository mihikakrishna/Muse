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

    public NavigationStore(SignalRChatService chatService)
    {
        _currentViewModel = new HomeWindowViewModel(this, chatService);
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