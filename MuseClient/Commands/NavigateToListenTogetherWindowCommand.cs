using System;
using System.Windows.Input;
using MuseClient.Stores;
using MuseClient.ViewModels;

namespace MuseClient.Commands;

public class NavigateToListenTogetherWindowCommand : ICommand
{
    private readonly NavigationStore _navigationStore;

    public NavigateToListenTogetherWindowCommand(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
    
    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(_navigationStore);
    }
}