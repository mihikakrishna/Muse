using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MuseClient.Enums;
using MuseClient.Services;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class CreateRoomWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public CreateRoomWindowViewModel(NavigationStore navigationStore)
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
            case Pages.ListenTogetherPage:
                var hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/chatHub")
                    .Build();
                var chatService = new SignalRChatService(hubConnection);
                _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(_navigationStore, chatService);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}