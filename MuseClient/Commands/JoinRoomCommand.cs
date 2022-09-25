using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;

namespace MuseClient.Commands;

public class JoinRoomCommand : ICommand
{
    private readonly HomeWindowViewModel _viewModel;
    private readonly NavigationStore _navigationStore;
    private readonly SignalRMuseService _signalRMuseService;

    public JoinRoomCommand(HomeWindowViewModel viewModel, NavigationStore navigationStore, SignalRMuseService signalRMuseService)
    {
        _viewModel = viewModel;
        _navigationStore = navigationStore;
        _signalRMuseService = signalRMuseService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        // send roomCode to server to ValidateRoom, if valid then continue, else throw an error window
        _navigationStore.CurrentViewModel = new ListenTogetherWindowViewModel(
            navigationStore: _navigationStore,
            signalRMuseService: _signalRMuseService,
            username: _viewModel.Username,
            roomCode: _viewModel.RoomCode);
    }

}