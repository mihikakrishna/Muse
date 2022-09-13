using System;
using System.Reactive;
using System.Windows.Input;
using MuseClient.Commands;
using MuseClient.Stores;
using ReactiveUI;

namespace MuseClient.ViewModels;

public class HomeWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    private string _username;

    public HomeWindowViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _username = string.Empty;
    }

    public string Username
    {
        get => _username;
        set
        {
            this.RaiseAndSetIfChanged(ref _username, value);
            Console.WriteLine(_username);
        }
    }

    public void NavigateToListenTogetherWindowCommand()
    {
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(_navigationStore, _username);
    }
}