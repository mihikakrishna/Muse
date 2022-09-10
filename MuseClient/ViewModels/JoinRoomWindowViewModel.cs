using System;
using MuseClient.Enums;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class JoinRoomWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public JoinRoomWindowViewModel(NavigationStore navigationStore)
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
                _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(_navigationStore);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}