using System;
using System.Windows.Input;
using MuseClient.Services;
using MuseClient.Stores;
using MuseClient.ViewModels;
using MuseDomain.Models;

namespace MuseClient.Commands;

public class JoinRoomCommand : ICommand
{
    private readonly HomeWindowViewModel _viewModel;
    private readonly SignalRChatroomService _chatroomService;

    public JoinRoomCommand(HomeWindowViewModel viewModel, SignalRChatroomService chatroomService)
    {
        _viewModel = viewModel;
        _chatroomService = chatroomService;
    }

    public event EventHandler? CanExecuteChanged = delegate { };

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        var roomMessage = new RoomMessage(roomCode: _viewModel.RoomCode);
        await _chatroomService.ValidateRoom(roomMessage);
    }

}