using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;
using MuseDomain.Models;

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

    public async void Execute(object? parameter)
    {
        // send RoomCode to server to createRoom, save result in _roomCode and continue
        var roomMessage = new RoomMessage("");
        await _chatroomService.CreateRoom(roomMessage);
        Console.WriteLine(roomMessage.RoomCode);
        _viewModel.RoomCode = roomMessage.RoomCode;
        _navigationStore.CurrentViewModel = ListenTogetherWindowViewModel.CreateConnectedViewModel(
            navigationStore: _navigationStore,
            username: _viewModel.Username,
            roomCode: _viewModel.RoomCode);
    }

}