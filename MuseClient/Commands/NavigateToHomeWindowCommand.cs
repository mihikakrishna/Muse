using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;

namespace MuseClient.Commands;

public class NavigateToHomeWindowCommand : ICommand
{
    private readonly SignalRMuseService _signalRMuseService;
    private readonly NavigationStore _navigationStore;

    public NavigateToHomeWindowCommand(SignalRMuseService museService, NavigationStore navigationStore)
    {
        _signalRMuseService = museService;
        _navigationStore = navigationStore;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = new HomeWindowViewModel(_navigationStore, _signalRMuseService);
    }
}