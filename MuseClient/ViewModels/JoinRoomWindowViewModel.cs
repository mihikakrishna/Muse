using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
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
                var chatService = new SignalRChatService(new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/chatHub")
                    .Build());
                _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(_navigationStore, chatService);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}