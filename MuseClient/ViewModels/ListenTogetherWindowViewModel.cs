using System;
using MuseClient.Enums;
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
            default:
                throw new ArgumentException($"Invalid page name received: {page}");
        }
    }
}