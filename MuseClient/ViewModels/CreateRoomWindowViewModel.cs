using System;
using MuseClient.Enums;
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
                _navigationStore.CurrentViewModel = new ListenTogetherWindowViewModel(_navigationStore);
                break;
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}