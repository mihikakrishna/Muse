using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;

namespace MuseClient.Commands;

public class CreateRoomCommand : ICommand
{
    private readonly HomeWindowViewModel _viewModel;
    private readonly NavigationStore _navigationStore;
    private readonly SignalRMuseService _signalRMuseService;

    public CreateRoomCommand(HomeWindowViewModel viewModel, NavigationStore navigationStore, SignalRMuseService signalRMuseService)
    {
        _viewModel = viewModel;
        _navigationStore = navigationStore;
        _signalRMuseService = signalRMuseService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        // send roomCode to server to createRoom, save result in _roomCode and continue
        _viewModel.RoomCode = "1234"; // since server logic isnt there yet to generate our room code - hardcoding "1234" for now
        _navigationStore.CurrentViewModel = new ListenTogetherWindowViewModel(
            navigationStore: _navigationStore,
            signalRMuseService: _signalRMuseService,
            username: _viewModel.Username,
            roomCode: _viewModel.RoomCode);
    }

}