using System;
using MuseClient.Enums;
using MuseClient.Services;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class JoinRoomWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly SignalRChatService _chatService;

    public JoinRoomWindowViewModel(NavigationStore navigationStore, SignalRChatService chatService)
    {
        _navigationStore = navigationStore;
        _chatService = chatService;
    }

    public void SwitchPage(string page)
    {
        switch (page)
        {
            case Pages.HomePage:
                _navigationStore.CurrentViewModel = new HomeWindowViewModel(_navigationStore, _chatService);
                break;
            case Pages.ListenTogetherPage:
                _navigationStore.CurrentViewModel = new ListenTogetherWindowViewModel(_navigationStore, _chatService);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}