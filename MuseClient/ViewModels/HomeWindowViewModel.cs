using System;
using MuseClient.Enums;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public HomeWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public void SwitchPage()
    {
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(_navigationStore);
    }
}