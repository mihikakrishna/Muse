using System;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Enums;
using MuseClient.Services;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class ListenTogetherWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public ListenTogetherWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void SwitchPage(string page)
    {
        switch (page)
        {
            case Pages.HomePage:
                _navigationStore.CurrentViewModel = new HomeWindowViewModel(_navigationStore);
                break;
            case Pages.ChatPage:
                _navigationStore.CurrentViewModel = ChatViewModel.CreateConnectedViewModel(_navigationStore, _navigationStore._chatService);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}