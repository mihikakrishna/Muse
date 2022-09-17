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
    private readonly SignalRChatroomService _chatroomService;

    public CreateRoomCommand(HomeWindowViewModel viewModel, NavigationStore navigationStore, SignalRChatroomService chatroomService)
    {
        _viewModel = viewModel;
        _navigationStore = navigationStore;
        _chatroomService = chatroomService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        // send roomCode to server to createRoom, save result in _roomCode and continue
        _viewModel.RoomCode = "1234"; // since server logic isnt there yet to generate our room code - hardcoding "1234" for now
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(
            navigationStore: _navigationStore,
            username: _viewModel.Username,
            roomCode: _viewModel.RoomCode);
    }

}