using System;
using MuseClient.Enums;
using MuseClient.Services;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private SignalRChatService _chatService;

    public HomeWindowViewModel(NavigationStore navigationStore, SignalRChatService chatService)
    {
        _navigationStore = navigationStore;
        _chatService = chatService;
    }

    public void SwitchPage(string page)
    {
        switch (page)
        {
            case Pages.JoinRoomPage:
                _navigationStore.CurrentViewModel = new JoinRoomWindowViewModel(_navigationStore, _chatService);
                break;
            case Pages.CreateRoomPage:
                _navigationStore.CurrentViewModel = new CreateRoomWindowViewModel(_navigationStore, _chatService);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}