using System;
using MuseClient.Enums;
using MuseClient.Services;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public HomeWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void SwitchPage(string page)
    {
        switch (page)
        {
            case Pages.JoinRoomPage:
                _navigationStore.CurrentViewModel = new JoinRoomWindowViewModel(_navigationStore);
                break;
            case Pages.CreateRoomPage:
                _navigationStore.CurrentViewModel = new CreateRoomWindowViewModel(_navigationStore);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}