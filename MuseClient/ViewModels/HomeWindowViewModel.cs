using System;
using System.Windows.Input;
using MuseClient.Commands;
using MuseClient.Enums;
using MuseClient.Stores;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    public ICommand NavigateToListenTogetherWindowCommand { get; }

    public HomeWindowViewModel(NavigationStore navigationStore)
    {
        NavigateToListenTogetherWindowCommand = new NavigateToListenTogetherWindowCommand(navigationStore);
    }

    
}