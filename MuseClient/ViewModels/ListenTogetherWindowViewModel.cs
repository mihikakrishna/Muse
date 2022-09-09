using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Enums;
using MuseClient.Services;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class ListenTogetherWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private readonly SignalRChatService _chatService;


    public ListenTogetherWindowViewModel(NavigationStore navigationStore, SignalRChatService chatService)
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
            case Pages.ChatPage:
                _navigationStore.CurrentViewModel = ChatViewModel.CreateConnectedViewModel(_navigationStore, _chatService);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}